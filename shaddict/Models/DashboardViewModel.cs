using Shaddict.Models;

namespace Shaddict.Models
{
    public class DashboardViewModel
    {
        public int EntityCount { get; set; }
        public int TableCount { get; set; }
        public int ColumnCount { get; set; }
        public List<DatabaseTable> RecentTables { get; set; } = new List<DatabaseTable>();
        public List<EntityTableCount> EntitiesByTableCount { get; set; } = new List<EntityTableCount>();
    }

    public class EntityTableCount
    {
        public string EntityName { get; set; } = string.Empty;
        public int TableCount { get; set; }
    }
} 