namespace UI
{
    public partial class ConnectionDialog
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // Загрузка сохраненных настроек
            var connectionSettings = _settingsManager.Load().ConnectionSettings;

            // Создание и настройка Label элементов
            this.serverLabel = new Label { Text = "Server:", Font = new Font(FontFamily.GenericSansSerif, 12), Top = 10, Left = 10 };
            this.databaseLabel = new Label { Text = "Database:", Font = new Font(FontFamily.GenericSansSerif, 12), Top = 40, Left = 10 };
            this.uidLabel = new Label { Text = "User ID:", Font = new Font(FontFamily.GenericSansSerif, 12), Top = 70, Left = 10 };
            this.pwdLabel = new Label { Text = "Password:", Font = new Font(FontFamily.GenericSansSerif, 12), Top = 100, Left = 10 };

            // Создание и настройка TextBox элементов с использованием загруженных значений
            serverTextBox = new TextBox { Top = 10, Left = serverLabel.Right + 10, Text = connectionSettings.Server };
            databaseTextBox = new TextBox { Top = 40, Left = databaseLabel.Right + 10, Text = connectionSettings.Database };
            uidTextBox = new TextBox { Top = 70, Left = uidLabel.Right + 10, Text = connectionSettings.User };
            pwdTextBox = new TextBox { Top = 100, Left = pwdLabel.Right + 10, Text = connectionSettings.Password };

            // Создание и настройка кнопки подтверждения
            this.confirmation = new Button { Text = "OK", Top = 130, Left = 65, Font = new Font(FontFamily.GenericSansSerif, 14), Size = new Size(100, 40) };
            confirmation.Click += Confirmation_Click;

            // Добавление элементов на форму
            Controls.Add(serverLabel);
            Controls.Add(databaseLabel);
            Controls.Add(uidLabel);
            Controls.Add(pwdLabel);
            Controls.Add(serverTextBox);
            Controls.Add(databaseTextBox);
            Controls.Add(uidTextBox);
            Controls.Add(pwdTextBox);
            Controls.Add(confirmation);

            // Настройка формы
            Text = "Connection";
            Size = new Size(250, 220);
        }

        private TextBox serverTextBox;
        private TextBox databaseTextBox;
        private TextBox uidTextBox;
        private TextBox pwdTextBox;
        private Label serverLabel;
        private Label databaseLabel;
        private Label uidLabel;
        private Label pwdLabel;
        private Button confirmation;
    }
}