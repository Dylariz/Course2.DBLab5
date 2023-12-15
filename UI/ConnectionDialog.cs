using UI.Settings;
using static System.String;

namespace UI;

public partial class ConnectionDialog : Form
{
    private readonly SettingsManager _settingsManager;
    public string Server { get; private set; } = Empty;
    public string Database { get; private set; } = Empty;
    public string Uid { get; private set; } = Empty;
    public string Pwd { get; private set; } = Empty;

    public ConnectionDialog(SettingsManager settingsManager)
    {
        _settingsManager = settingsManager;
        InitializeComponent();
    }

    private void Confirmation_Click(object? sender, EventArgs e)
    {
        Server = serverTextBox.Text;
        Database = databaseTextBox.Text;
        Uid = uidTextBox.Text;
        Pwd = pwdTextBox.Text;

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