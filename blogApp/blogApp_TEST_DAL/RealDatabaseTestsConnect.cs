using blogApp_DAL;
using blogApp_DAL.Model;
using blogApp_DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace blogApp_TEST_DAL
{
    public class RealDatabaseTestsConnect
    {
        private static string GetDatabasePath()
        {
            // Получаем путь к директории с тестовым проектом
            var testProjectDirectory = Directory.GetCurrentDirectory();

            // Поднимаемся на уровень выше к решению, затем к DAL проекту
            var solutionDirectory = Directory.GetParent(testProjectDirectory).Parent.Parent.Parent;
            var dalProjectDirectory = Path.Combine(solutionDirectory.FullName, "blogApp_DAL");
            var dbDirectory = Path.Combine(dalProjectDirectory, "db");
            var dbPath = Path.Combine(dbDirectory, "db.db");

            return dbPath;
        }

        [Fact]
        public void Test1()
        {
            SQLitePCL.Batteries.Init();

            // Используем правильный путь
            var connectionString = $"Data Source={GetDatabasePath()}";

            var options = new DbContextOptionsBuilder<Blog_DBcontext>()
               .UseSqlite(connectionString)
               .Options;

            var context = new Blog_DBcontext(options);

            var repo = new UserRepo(context);

            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Иван",
                Email = "1123123@example.com",
                Password = "2313213",
                Created_at = DateTime.UtcNow,
                Role = "User",
                Is_active = true,
                Avatar_url = "https://example.com/avatar.jpg",
                Bio = "Разработчик на C#"
            };

            repo.CreateUser(user);

            var dbUser = context.Users.FirstOrDefault(u => u.Id == user.Id);
            Assert.NotNull(dbUser);
        }
    }
}