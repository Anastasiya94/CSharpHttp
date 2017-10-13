using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpClientApp
{
    public partial class HttpClientForm : Form
    {

        public HttpClientForm()
        {
            InitializeComponent();
        }

        //Тип данных для десериализации JSON от сервера
        private class ServerResponce { public string response_text; }

        private async void processAction(int _type)
        {
            var request = new
            {
                type = _type, //0 = добавление, 1 = запрос
                inn = textBox_inn.Text.Replace(" ", ""),
                name = textBox_name.Text
            };
            setEnabledUI(false);

            rtb_output.Text += string.Format("<< {0} -> ИНН: {1}, Название: {2}\n", _type == 1 ? "Поиск" : "Добавить", request.inn, request.name);

            try
            {
                var client = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var responce = await client.PostAsync(textBox_server.Text, content);
                var responceString = await responce.Content.ReadAsStringAsync();
                var responce_obj = JsonConvert.DeserializeObject<ServerResponce>(responceString);
                rtb_output.Text += string.Format(">> Ответ -> {0}\n", responce_obj.response_text);
            }
            catch (Exception exc)
            {
                rtb_output.Text += string.Format("Ошибка: {0}\n", exc.Message);
            }
            finally
            {
                setEnabledUI(true);
                rtb_output.SelectionStart = rtb_output.Text.Length;
                rtb_output.ScrollToCaret();
            }
        }

        private void setEnabledUI(bool enabled)
        {
            textBox_inn.Enabled = enabled;
            textBox_name.Enabled = enabled;
            textBox_server.Enabled = enabled;
            button_create.Enabled = enabled;
            button_find.Enabled = enabled;
        }

        private void button_find_Click(object sender, EventArgs e)
        {
            processAction(1);
        }


        private void button_create_Click(object sender, EventArgs e)
        {
            processAction(0);
        }

    }
}
