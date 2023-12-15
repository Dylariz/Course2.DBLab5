using System.Reflection;
using DatabaseAggregator;
using DatabaseAggregator.Context;

namespace UI
{
    public partial class DbViewer : Form
    {
        private Type _dbContextType = typeof(MercenaryDatabaseContext);
        private MercenaryDatabaseContext? _dbContext;
        private DbContextParams _dbContextParams;
        
        public DbViewer()
        {
            InitializeComponent();
            _dbContextParams = new DbContextParams(_dbContextType);
            _dbContext = null;
            
            // Добавляем имена таблиц в ComboBox.
            if (_dbContextParams.DbSetProperties != null)
            {
                foreach (var prop in _dbContextParams.DbSetProperties)
                {
                    tableChoiseComboBox.Items.Add(prop.Name);
                }
                tableChoiseComboBox.SelectedIndex = 0;
                tableChoiseComboBox.SelectedIndexChanged += tableChoiseComboBox_SelectedIndexChanged;
            }
            else
            {
                MessageBox.Show("Указанный тип не содержит свойств типа DbSet<T>");
                Application.Exit();
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            _dbContext ??= new MercenaryDatabaseContext();
            LoadData();
        }
        
        private void tableChoiseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        
        private void LoadData()
        {
            // Получаем имя таблицы, выбранной в ComboBox.
            string tableName = tableChoiseComboBox.SelectedItem!.ToString()!;
            
            // Получаем свойство типа DbSet<T> с именем tableName.
            PropertyInfo? dbSetProperty = _dbContextParams.DbSetProperties?.FirstOrDefault(p => p.Name == tableName);

            if (dbSetProperty == null)
            {
                throw new Exception($"Не найдено свойство типа DbSet<T> с именем {tableName}");
            }
            
            // Получаем значение этого свойства.
            var dbSet = dbSetProperty.GetValue(_dbContext);
            
            // Получаем тип элементов этого свойства.
            var dbSetType = dbSetProperty.PropertyType.GenericTypeArguments[0];
            
            // Получаем метод ToList() для этого типа.
            MethodInfo? toListMethod = typeof(Enumerable).GetMethod("ToList")?.MakeGenericMethod(dbSetType);
            
            if (toListMethod == null)
            {
                throw new Exception($"Не найден метод ToList() для типа {dbSetType}");
            }
            
            // Вызываем метод ToList() для свойства dbSet.
            var list = toListMethod.Invoke(null, new[] {dbSet});
            
            // Заполняем DataGridView.
            tableDataGrid.DataSource = list;
        }
    }
}