using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_Core
{
    public static class GetPath
    {
        public static string GetDatabasePath()
        {
            var testProjectDirectory = Directory.GetCurrentDirectory();

            var solutionDirectory = Directory.GetParent(testProjectDirectory);
            var dalProjectDirectory = Path.Combine(solutionDirectory.FullName, "blogApp_DAL");
            var dbDirectory = Path.Combine(dalProjectDirectory, "db");
            var dbPath = Path.Combine(dbDirectory, "db.db");

            return dbPath;
        }
    }
}
