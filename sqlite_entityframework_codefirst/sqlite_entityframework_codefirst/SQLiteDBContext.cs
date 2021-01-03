using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sqlite_entityframework_codefirst
{
    public class SQLiteDBContext : DbContext
    {
        public SQLiteDBContext()
            : base(new SQLiteConnection(Path.Combine(Path.GetTempPath(), "MigrationDb.sqlite3")), false)
        {

        }

        public SQLiteDBContext(DbConnection connection) : base(connection, true)
        {
        }

        public DbSet<Table1> Table1 { get; set; }
    }
}
