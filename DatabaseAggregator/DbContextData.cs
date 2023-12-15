using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAggregator;

public class DbContextData
{
    private DbContext _dbContext;
    private Dictionary<string, IList> _dataTable = new();
    private Dictionary<string, List<string>> _unvisibleProperties = new();
    public IReadOnlyDictionary<string, IList> DataTable => _dataTable;
    public IReadOnlyDictionary<string, List<string>> UnvisibleProperties => _unvisibleProperties;
    
    public DbContextData(DbContext dbContext)
    {
        _dbContext = dbContext;
        LoadTables();
    }

    private void LoadTables()
    {
        var dbSetProperties = _dbContext.GetType().GetProperties()
            .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

        foreach (var prop in dbSetProperties)
        {
            // Получаем значение DbSet
            var dbSet = prop.GetValue(_dbContext);

            // Получаем тип элементов DbSet
            var dbSetElementType = dbSet!.GetType().GetGenericArguments()[0];
            AddInvisibleProperties(dbSetElementType);
            
            // Получаем лист из DbSet
            var list = (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(dbSetElementType))!;
            foreach (var item in (IEnumerable) dbSet)
            {
                list.Add(item);
            }
            _dataTable.Add(dbSetElementType.Name, list);
        }
    }
    
    private void AddInvisibleProperties(Type tableType)
    {
        var result = new List<string>();
        
        if (tableType == null)
        {
            throw new ArgumentException($"Model {tableType} not found");
        }
        
        var tableProperties = tableType.GetProperties();
        
        foreach (var tableProperty in tableProperties)
        {
            var toStringMethod = tableProperty.PropertyType.GetMethod("ToString", Type.EmptyTypes);
            
            if (toStringMethod == null || toStringMethod.DeclaringType == typeof(object))
            {
                result.Add(tableProperty.Name);
            }
        }
        
        _unvisibleProperties.Add(tableType.Name, result);
    }
}