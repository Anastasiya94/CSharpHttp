using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using System.Data.SQLite;

namespace HttpServerApp
{
    class HttpServer
    {
        int port; //Порт по которому будем получать запросы
        HttpListener listener; //HttpListener для получения запросов
        HashSet<HttpListenerContext> current_requests; //Хэш таблица полученных и еще не обработанных запросы
        volatile bool stop_requested; //Флаг завершения работы сервера
        string database_path; //Путь к файлу базы данных SQLite

        //Конструктор сервера, инициализирует свойства класса и устанавливает макс. число потоков в пуле
        public HttpServer(string _database_path, int _port, int _max_thread_count)
        {
            port = _port;  
            stop_requested = false;
            database_path = _database_path;
            current_requests = new HashSet<HttpListenerContext>();
            //Устанавливаем заданное максимальное число одновременных потоков,
            //а минимальное число по умолчанию станет равно числу процессоров
            ThreadPool.SetMaxThreads(_max_thread_count, _max_thread_count);
            
        }

        //Запуск сервера
        public void Start()
        {
            //Создаем listener запросов и задаем ему шаблоны запросов которые он будет обрабатывать, с учетом выбранного порта
            listener = new HttpListener();
            listener.Prefixes.Add(string.Format("http://+:{0}/",port));

            //Запускаем listener
            try
            {
                listener.Start();
            }
            catch (HttpListenerException exc)
            {
                
                Console.WriteLine(string.Format("Ошибка запуска сервера: {0}", exc.Message));
                return;
            }
            Console.WriteLine(string.Format("Server started on port {0}", port));
            
            //Асинхронно запускаем ожидание первого запроса
            listener.BeginGetContext(StartAsyncProcessing, listener);

            //Ждем нажатия любой клавиши для остановки сервера
            Console.Write("Press any key to stop server... ");
            Console.ReadKey();

            //Выставляем флаг завершения работы сервера и ждем завершения обработки все ранее полученных запросов
            Console.WriteLine("\nWaiting all processing to finish... ");
            stop_requested = true;
            while (true)
            {
                lock (current_requests)
                {
                    //Проверяем хэш таблицу с интервалом в 1 секунду
                    if (current_requests.Count == 0) break;
                    Thread.Sleep(1000);
                }
            }

            //Завершаем работу
            listener.Close();
            Console.WriteLine("Server stopped");
        }

        //Запуск обработки запроса в отдельном потоке
        private void StartAsyncProcessing(IAsyncResult result)
        {         
            if (stop_requested) return; //Если сервер завершает свою работу, не обрабатываем запрос
 
            var context = listener.EndGetContext(result); //Завершаем получение текущего запроса
            lock (current_requests) current_requests.Add(context); //Добавляем текущий запрос в таблицу обрабатываемых запросов
            ThreadPool.QueueUserWorkItem(arg => ProcessRequest(context)); //Начинаем обработку полученного запроса в отдельном потоке из пула       
            listener.BeginGetContext(StartAsyncProcessing, listener); //Асинхронно запускаем ожидание следующего запроса
        }
        //Тип данных для десериализации JSON от клиента
        private class ClientRequest
        {
            public int type; //0 = добавление, 1 = запрос
            public string inn;
            public string name;
        }
        //Тип данных для сериализации ответа сервера
        private class ServerResponce { public string response_text; }

        //Обработка запроса
        private void ProcessRequest(HttpListenerContext context)
        {
            var responce = new ServerResponce();
            try
            {
                //Читаем текст запроса
                var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding);
                var request_string = reader.ReadToEnd();
                Console.Write(string.Format("\n>>Request: {0}", request_string));

                //Thread.Sleep(10000);

                //Десериализуем поступивший JSON в объект ClientRequest
                var request = JsonConvert.DeserializeObject<ClientRequest>(request_string);

                //Поиск в базе (тип == 1)
                if (request.type == 1)
                {
                    //Cоздаем подключение и составляем запрос в зависимости от полученных параметров
                    var con = OrganizationDB.CreateAndOpenConnection(database_path);
                    var query = "SELECT inn, name FROM organization ";
                    var parameters = new Dictionary<string, object>();
                    if (request.inn != "" && request.name != "")
                    {
                        query += "WHERE inn LIKE @INN AND name LIKE @NAME";
                        parameters["@INN"] = "%" + request.inn + "%";
                        parameters["@NAME"] = "%" + request.name + "%";
                    }
                    else if (request.inn != "")
                    {
                        query += "WHERE inn LIKE @INN";
                        parameters["@INN"] = "%" + request.inn + "%";
                    }
                    else if (request.name != "")
                    {
                        query += "WHERE name LIKE @NAME";
                        parameters["@NAME"] = "%" + request.name + "%";
                    }

                    var db_reader = OrganizationDB.Query(con, database_path, query, parameters);

                    if (!db_reader.HasRows) responce.response_text = "Не найдено организаций с указанными параметрами";
                    else
                    {
                        responce.response_text = "Найденные организации:";
                        while (db_reader.Read()) responce.response_text += string.Format("\nИНН: {0}, Название: {1}", db_reader["inn"], db_reader["name"]);    
                    }
                    db_reader.Close();
                    con.Close();
                }
                //Добавление новой организации
                else
                {
                    if (request.inn.Length != 10) responce.response_text = "Ошибка: ИНН должен состоять из 10 цифр";
                    else
                    {
                        OrganizationDB.AddOrganization(database_path, request.inn, request.name);
                        responce.response_text = "Организация успешно создана";
                    }
                }

            }
            catch (Exception exc)
            {
                responce.response_text = string.Format("Ошибка на сервере: {0}", exc.Message);
            }
            finally
            {
                //Отправляем ответ
                byte[] b = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responce));
                context.Response.ContentLength64 = b.Length;
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.OutputStream.Write(b, 0, b.Length);
                context.Response.Close();

                lock (current_requests) current_requests.Remove(context);
            }     
        }

        //Вход программы
        static void Main(string[] args)
        {
            int _port = 80; //HTTP порт по умолчанию
            int _max_thread_count = Environment.ProcessorCount * 4; //Максимальное количество потоков по умолчанию = количество ядер * 4
            string _database_file;

            //Получаем имя базы данных (первый параметр)
            if (args.Length < 1)
            {
                Console.WriteLine("no database specified");
                return;
            }
            else
            {
                _database_file = args[0];
                //Если файл или таблица не существует - создаем
                try {
                    OrganizationDB.CreateDataBaseFileIfNotExists(_database_file);
                    OrganizationDB.CreateOrganizationTableIfNotExists(_database_file);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(string.Format("error creating database file or table: {0}", exc.Message));
                    return;
                }
            }

            //Получаем порт из аргументов командной строки, если он задан
            if (args.Length > 1 && !int.TryParse(args[1], out _port))
            {
                Console.WriteLine("not a valid port number");
                return;
            }
            //Получаем макс число потоков из аргументов командной строки, если оно задано
            //Проверям чтобы оно было не меньше числа ядер, т.к. TreadPool требует чтобы их было >=
            if (args.Length > 2 && (!int.TryParse(args[2], out _max_thread_count) || _max_thread_count < Environment.ProcessorCount))
            {
                Console.WriteLine(string.Format("not a valid thread count, or thread count < {0} (current processor count)", Environment.ProcessorCount));
                Console.ReadKey();
                return;
            }
            //Проверяем на допустимость использования HttpListener
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Needs Windows XP SP2, Server 2003 or later");
                return;
            }

            //Создаем и запускаем сервер
            HttpServer server = new HttpServer(_database_file, _port, _max_thread_count);
            server.Start();
        }


    }

    class OrganizationDB
    {
        public static SQLiteConnection CreateAndOpenConnection(string database_path)
        {
            var con = new SQLiteConnection(string.Format("Data Source={0};Version=3;", database_path));
            con.Open();
            return con;
        }

        public static void CreateDataBaseFileIfNotExists(string database_path)
        {
            if (!File.Exists(database_path)) SQLiteConnection.CreateFile(database_path);
        }

        public static void CreateOrganizationTableIfNotExists(string database_path)
        {
            var con = CreateAndOpenConnection(database_path);
            var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS organization (inn TEXT PRIMARY KEY, name TEXT NOT NULL);", con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public static SQLiteDataReader Query(SQLiteConnection con, string database_path, string query, Dictionary<string,object> parameters)
        {
            var command = new SQLiteCommand(query, con);
            foreach (string param_name in parameters.Keys) command.Parameters.AddWithValue(param_name, parameters[param_name]);         
            return command.ExecuteReader();
        }

        public static void AddOrganization(string database_path, string inn, string name)
        {
            var con = CreateAndOpenConnection(database_path);
            var command = new SQLiteCommand("REPLACE INTO organization (inn, name) VALUES (@INN, @NAME)", con);
            command.Parameters.AddWithValue("@INN", inn);
            command.Parameters.AddWithValue("@NAME", name);
            command.ExecuteNonQuery();
            con.Close();
        }

    }
}
