using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using sqlite_entityframework_codefirst.Migrations;

namespace sqlite_entityframework_codefirst
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Migration_2files()
        {

            var exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dbPath = Path.Combine(exeDir, "db.sqlite3");
            var dbPath2 = Path.Combine(exeDir, "db2.sqlite3");
            var connectString = $"DATA Source={dbPath}";
            var connectString2 = $"DATA Source={dbPath2}";


            using (var connection = new SQLiteConnection(connectString))
            using (var context = new SQLiteDBContext(connection))
            {
                var migrator = new DbMigrator(new Configuration
                {
                    TargetDatabase = new DbConnectionInfo(context.Database.Connection.ConnectionString, "System.Data.SQLite")
                });

                migrator.Update();
            }

            using (var connection = new SQLiteConnection(connectString2))
            using (var context = new SQLiteDBContext(connection))
            {
                var migrator = new DbMigrator(new Configuration
                {
                    TargetDatabase = new DbConnectionInfo(context.Database.Connection.ConnectionString, "System.Data.SQLite")
                });

                migrator.Update();
            }
        }

        [Test]
        public void DBDataAddAndGet()
        {
            var exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dbPath = Path.Combine(exeDir, "db.sqlite3");
            var connectString = $"DATA Source={dbPath}";

            using (var connection = new SQLiteConnection(connectString))
            using (var context = new SQLiteDBContext(connection))
            {
                context.Table1.AddOrUpdate(new Table1
                {
                    data1 = new byte[] {1, 2},
                    idStr1 = "str1-1",
                    idStr2 = "str2-1",
                    value1 = "value1"
                });
                context.Table1.AddOrUpdate(new Table1
                {
                    data1 = new byte[] { 3, 2 },
                    idStr1 = "str1-2",
                    idStr2 = "str2-2",
                    value1 = "value2"
                });
                context.Table1.AddOrUpdate(new Table1
                {
                    data1 = new byte[] { 4, 5 },
                    idStr1 = "str1-1",
                    idStr2 = "str2-3",
                    value1 = "value3"
                });
                context.SaveChanges();
            }

            using (var connection = new SQLiteConnection(connectString))
            using (var context = new SQLiteDBContext(connection))
            {
                var datas = context.Table1.Where(d => d.idStr1 == "str1-1").ToArray();
                Assert.That(datas.Count(), Is.EqualTo(2));
                Assert.That(datas.Count(d => d.data1.SequenceEqual(new byte[] { 1, 2 }) && d.value1 == "value1"), Is.EqualTo(1));
                Assert.That(datas.Count(d => d.data1.SequenceEqual(new byte[] { 4, 5 }) && d.value1 == "value3"), Is.EqualTo(1));
            }

        }
    }
}
