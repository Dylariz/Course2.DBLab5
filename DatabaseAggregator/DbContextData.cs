using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAggregator;

public class DbContextData
{
    public DbContext DbContext { get; }
    private Dictionary<string, IList> _dataTable = new();
    private Dictionary<string, List<string>> _invisibleProperties = new();
    public IReadOnlyDictionary<string, IList> DataTable => _dataTable;
    public IReadOnlyDictionary<string, List<string>> InvisibleProperties => _invisibleProperties;

    public DbContextData(DatabaseModelGenerator modelGenerator)
    {
        if (!modelGenerator.IsGenerated)
        {
            throw new ArgumentException("DatabaseModel.dll not found");
        }

        var dbContextTypeName = modelGenerator.DbContextTypeName;

        var assembly = modelGenerator.GetAssembly();
        if (assembly == null)
        {
            throw new ArgumentException("Assembly not found");
        }
        
        var test = assembly.GetTypes();

        var dbContextType = assembly.GetType(dbContextTypeName);
        if (dbContextType == null)
        {
            throw new ArgumentException($"Type {dbContextTypeName} not found");
        }

        if (!typeof(DbContext).IsAssignableFrom(dbContextType))
        {
            throw new ArgumentException($"Type {dbContextTypeName} is not a DbContext");
        }

        DbContext = (DbContext)Activator.CreateInstance(dbContextType)!;

        if (DbContext == null)
        {
            throw new ArgumentException($"Type {dbContextTypeName} is not a DbContext");
        }

        LoadTables();
    }

    private void LoadTables()
    {
        var dbSetProperties = DbContext.GetType().GetProperties()
            .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

        foreach (var prop in dbSetProperties)
        {
            // Получаем значение DbSet
            var dbSet = prop.GetValue(DbContext);

            // Получаем тип элементов DbSet
            var dbSetElementType = dbSet!.GetType().GetGenericArguments()[0];
            AddInvisibleProperties(dbSetElementType);

            // Получаем лист из DbSet
            var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(dbSetElementType))!;
            foreach (var item in (IEnumerable)dbSet)
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

        _invisibleProperties.Add(tableType.Name, result);
    }
}