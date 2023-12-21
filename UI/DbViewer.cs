using DatabaseAggregator;
using Microsoft.EntityFrameworkCore;
using UI.Settings;

namespace UI;

public partial class DbViewer : Form
{
    private DbContextData? _dbContextData;
    private DbContext? _dbContext;
    private bool _dataChanged;

    public DbViewer()
    {
        InitializeComponent();
    }

    private void connectButton_Click(object sender, EventArgs e)
    {
        using var dialog = new ConnectionDialog(new SettingsManager(Path.Combine("Settings", "settings.json")));

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            var modelGenerator = new DatabaseModelGenerator(dialog.Server, dialog.Database, dialog.Uid, dialog.Pwd);
            if (!modelGenerator.TryGenerate())
            {
                Reset();
                MessageBox.Show("Не удалось подключиться к базе данных");
                return;
            }
            
            _dbContextData = new DbContextData(modelGenerator);

            if (!_dbContextData.DbContext.Database.CanConnect())
            {
                Reset();
                MessageBox.Show("Подключение прервано");
            }
            else
            {
                connectButton.Text = "Переподключение";
                _dbContext = _dbContextData.DbContext;
                LoadComboBox();
                tableChoiseComboBox.Enabled = true;
                tableChoiseComboBox.SelectedIndex = 0;
                LoadData();
            }
        }
    }

    private void tableChoiseComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_dbContext != null)
        {
            LoadData();
        }
    }

    private void tableDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (_dbContext != null)
        {
            _dataChanged = true;
        }
    }

    private async void ConnectionCheckTimer_Tick(object sender, EventArgs e)
    {
        if (_dbContext != null)
        {
            if (!await _dbContext.Database.CanConnectAsync())
            {
                Reset();
                MessageBox.Show("Соединение с базой данных было разорвано");
            }
            else if(_dataChanged)
            {
                await _dbContext.SaveChangesAsync();
            }
        }
    }

    private void DbViewer_FormClosing(object sender, FormClosingEventArgs e)
    {
        _dbContext?.Dispose();
    }

    private void Reset()
    {
        _dbContext?.Dispose();
        _dbContext = null;
        _dbContextData = null;
        tableChoiseComboBox.Enabled = false;
        tableChoiseComboBox.Items.Clear();
        tableDataGrid.DataSource = null;
        connectButton.Text = "Подключение";
    }

    private void LoadComboBox()
    {
        tableChoiseComboBox.Items.Clear();
        foreach (var tableName in _dbContextData!.DataTable.Keys)
        {
            tableChoiseComboBox.Items.Add(tableName);
        }
    }

    private void LoadData()
    {
        // Получаем имя таблицы, выбранной в ComboBox.
        string tableName = tableChoiseComboBox.SelectedItem!.ToString()!;

        // Получаем данные из таблицы.
        var source = _dbContextData!.DataTable[tableName];

        // Заполняем DataGridView.
        tableDataGrid.DataSource = source;

        // Скрываем невидимые столбцы.
        foreach (var columnName in _dbContextData.InvisibleProperties[tableName])
        {
            tableDataGrid.Columns[columnName].Visible = false;
        }
    }
}