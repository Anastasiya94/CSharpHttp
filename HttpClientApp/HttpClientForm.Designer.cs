namespace HttpClientApp
{
    partial class HttpClientForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_inn = new System.Windows.Forms.TextBox();
            this.label_inn = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.button_find = new System.Windows.Forms.Button();
            this.button_create = new System.Windows.Forms.Button();
            this.rtb_output = new System.Windows.Forms.RichTextBox();
            this.textBox_server = new System.Windows.Forms.TextBox();
            this.label_server = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_inn
            // 
            this.textBox_inn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_inn.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_inn.Location = new System.Drawing.Point(120, 7);
            this.textBox_inn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_inn.Name = "textBox_inn";
            this.textBox_inn.Size = new System.Drawing.Size(615, 25);
            this.textBox_inn.TabIndex = 0;
            this.textBox_inn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_inn_KeyPress);
            // 
            // label_inn
            // 
            this.label_inn.AutoSize = true;
            this.label_inn.Location = new System.Drawing.Point(71, 11);
            this.label_inn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_inn.Name = "label_inn";
            this.label_inn.Size = new System.Drawing.Size(38, 17);
            this.label_inn.TabIndex = 1;
            this.label_inn.Text = "ИНН";
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(36, 52);
            this.label_name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(72, 17);
            this.label_name.TabIndex = 2;
            this.label_name.Text = "Название";
            // 
            // textBox_name
            // 
            this.textBox_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_name.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_name.Location = new System.Drawing.Point(120, 48);
            this.textBox_name.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(615, 25);
            this.textBox_name.TabIndex = 3;
            this.textBox_name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_name_KeyPress);
            // 
            // button_find
            // 
            this.button_find.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_find.Location = new System.Drawing.Point(528, 82);
            this.button_find.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_find.Name = "button_find";
            this.button_find.Size = new System.Drawing.Size(100, 28);
            this.button_find.TabIndex = 4;
            this.button_find.Text = "Найти";
            this.button_find.UseVisualStyleBackColor = true;
            this.button_find.Click += new System.EventHandler(this.button_find_Click);
            // 
            // button_create
            // 
            this.button_create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_create.Location = new System.Drawing.Point(633, 82);
            this.button_create.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_create.Name = "button_create";
            this.button_create.Size = new System.Drawing.Size(100, 28);
            this.button_create.TabIndex = 5;
            this.button_create.Text = "Создать";
            this.button_create.UseVisualStyleBackColor = true;
            this.button_create.Click += new System.EventHandler(this.button_create_Click);
            // 
            // rtb_output
            // 
            this.rtb_output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_output.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtb_output.Location = new System.Drawing.Point(20, 118);
            this.rtb_output.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rtb_output.Name = "rtb_output";
            this.rtb_output.ReadOnly = true;
            this.rtb_output.Size = new System.Drawing.Size(712, 272);
            this.rtb_output.TabIndex = 6;
            this.rtb_output.Text = "";
            this.rtb_output.WordWrap = false;
            // 
            // textBox_server
            // 
            this.textBox_server.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_server.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_server.Location = new System.Drawing.Point(120, 82);
            this.textBox_server.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_server.Name = "textBox_server";
            this.textBox_server.Size = new System.Drawing.Size(399, 25);
            this.textBox_server.TabIndex = 7;
            this.textBox_server.Text = "http://127.0.0.1:80/";
            // 
            // label_server
            // 
            this.label_server.AutoSize = true;
            this.label_server.Location = new System.Drawing.Point(16, 89);
            this.label_server.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_server.Name = "label_server";
            this.label_server.Size = new System.Drawing.Size(93, 17);
            this.label_server.TabIndex = 8;
            this.label_server.Text = "Сервер:Порт";
            // 
            // HttpClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 406);
            this.Controls.Add(this.label_server);
            this.Controls.Add(this.textBox_server);
            this.Controls.Add(this.rtb_output);
            this.Controls.Add(this.button_create);
            this.Controls.Add(this.button_find);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.label_inn);
            this.Controls.Add(this.textBox_inn);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "HttpClientForm";
            this.Text = "Организации";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_inn;
        private System.Windows.Forms.Label label_inn;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Button button_find;
        private System.Windows.Forms.Button button_create;
        private System.Windows.Forms.RichTextBox rtb_output;
        private System.Windows.Forms.TextBox textBox_server;
        private System.Windows.Forms.Label label_server;
    }
}

