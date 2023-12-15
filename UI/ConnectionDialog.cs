using UI.Settings;

namespace UI;

public class ConnectionDialog : Form
{
    private readonly SettingsManager _settingsManager;
    public string Server { get; private set; } = null!;
    public string Database { get; private set; } = null!;
    public string Uid { get; private set; } = null!;
    public string Pwd { get; private set; } = null!;

    private TextBox _serverTextBox = null!;
    private TextBox _databaseTextBox = null!;
    private TextBox _uidTextBox = null!;
    private TextBox _pwdTextBox = null!;

    public ConnectionDialog(SettingsManager settingsManager)
    {
        _settingsManager = settingsManager;
        InitializeComponent();
    }

    
    private void InitializeComponent()
    {
        // Загрузка сохраненных настроек
        var connectionSettings = _settingsManager.Load().ConnectionSettings;

        // Создание и настройка Label элементов
        Label serverLabel = new Label { Text = "Server:", Font = new Font(FontFamily.GenericSansSerif, 12), Top = 10, Left = 10 };
        Label databaseLabel = new Label { Text = "Database:", Font = new Font(FontFamily.GenericSansSerif, 12), Top = 40, Left = 10 };
        Label uidLabel = new Label { Text = "User ID:", Font = new Font(FontFamily.GenericSansSerif, 12), Top = 70, Left = 10 };
        Label pwdLabel = new Label { Text = "Password:", Font = new Font(FontFamily.GenericSansSerif, 12), Top = 100, Left = 10 };

        // Создание и настройка TextBox элементов с использованием загруженных значений
        _serverTextBox = new TextBox { Top = 10, Left = serverLabel.Right + 10, Text = connectionSettings.Server };
        _databaseTextBox = new TextBox { Top = 40, Left = databaseLabel.Right + 10, Text = connectionSettings.Database };
        _uidTextBox = new TextBox { Top = 70, Left = uidLabel.Right + 10, Text = connectionSettings.User };
        _pwdTextBox = new TextBox { Top = 100, Left = pwdLabel.Right + 10, Text = connectionSettings.Password };

        // Создание и настройка кнопки подтверждения
        Button confirmation = new Button { Text = "OK", Top = 130, Left = 65, Font = new Font(FontFamily.GenericSansSerif, 14), Size = new Size(100, 40) };
        confirmation.Click += Confirmation_Click;

        // Добавление элементов на форму
        Controls.Add(serverLabel);
        Controls.Add(databaseLabel);
        Controls.Add(uidLabel);
        Controls.Add(pwdLabel);
        Controls.Add(_serverTextBox);
        Controls.Add(_databaseTextBox);
        Controls.Add(_uidTextBox);
        Controls.Add(_pwdTextBox);
        Controls.Add(confirmation);

        // Настройка формы
        Text = "Connection";
        Size = new Size(250, 220);
    }

    private void Confirmation_Click(object? sender, EventArgs e)
    {
        Server = _serverTextBox.Text;
        Database = _databaseTextBox.Text;
        Uid = _uidTextBox.Text;
        Pwd = _pwdTextBox.Text;

        // Сохранение текущих значений в настройках
        _settingsManager.UpdateConnectionSettings(new ConnectionSettingsModel
        {
            Server = Server,
            Database = Database,
            User = Uid,
            Password = Pwd
        });

        DialogResult = DialogResult.OK;
        Close();
    }
}