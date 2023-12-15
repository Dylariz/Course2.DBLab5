using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAggregator;

public class DbContextParams
{
    private Type _dbContextType;
    public PropertyInfo[]? DbSetProperties { get; private set; }
    
    public DbContextParams(Type dbContextType)
    {
        _dbContextType = dbContextType;
        DbSetProperties = null;
        LoadTableNames();
    }
    
    private void LoadTableNames()
    {
        // Получаем все свойства этого типа.
        PropertyInfo[] properties = _dbContextType.GetProperties();

        // Фильтруем свойства, чтобы оставить только те, которые имеют тип DBSet.
        DbSetProperties = properties.Where(p =>
            p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)).ToArray();
    }
}