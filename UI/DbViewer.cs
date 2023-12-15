﻿using DatabaseAggregator;
using DatabaseAggregator.Context;
using UI.Settings;

namespace UI;

public partial class DbViewer : Form
{
    private MercenaryDatabaseContext? _dbContext;
    private DbContextData? _dbContextData;

    public DbViewer()
    {
        InitializeComponent();
    }

    private void connectButton_Click(object sender, EventArgs e)
    {
        using var dialog = new ConnectionDialog(new SettingsManager(Path.Combine("Settings", "settings.json")));

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            _dbContext = new MercenaryDatabaseContext(dialog.Server, dialog.Database, dialog.Uid, dialog.Pwd);


            if (!_dbContext.Database.CanConnect())
            {
                Reset();
                MessageBox.Show("Не удалось подключиться к базе данных");
            }
            else
            {
                _dbContextData = new DbContextData(_dbContext);
                connectButton.Text = "Переподключение";

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
            _dbContext.SaveChanges();
        }
    }

    private void ConnectionCheckTimer_Tick(object sender, EventArgs e)
    {
        if (_dbContext != null && !_dbContext.Database.CanConnect())
        {
            Reset();
            MessageBox.Show("Соединение с базой данных было разорвано");
        }
    }

    private void DbViewer_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (_dbContext != null)
        {
            _dbContext.Dispose();
        }
    }

    private void Reset()
    {
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
        foreach (var columnName in _dbContextData.UnvisibleProperties[tableName])
        {
            tableDataGrid.Columns[columnName].Visible = false;
        }
    }
}