using BlogAPP_BLL.Intarface;
using BlogAPP_BLL.Mappings;
using BlogAPP_BLL.Services;
using BlogAPP_Core;
using blogApp_DAL;
using blogApp_DAL.Intarface;
using blogApp_DAL.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Добавляем контроллеры
builder.Services.AddControllers();

var dbPath = GetPath.GetDatabasePath();
Console.WriteLine($"Путь к БД: {dbPath}");
Console.WriteLine($"Файл существует: {File.Exists(dbPath)}");
Console.WriteLine($"Полный путь: {Path.GetFullPath(dbPath)}");

var connectionString = $"Data source={GetPath.GetDatabasePath()}";
builder.Services.AddDbContext<Blog_DBcontext>(opt => opt.UseSqlite(connectionString));

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


// 2. Настраиваем CORS для Vue (важно!)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVue", policy =>
    {
        policy.WithOrigins("http://localhost:5173")  // адрес Vue
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 3. Добавляем Swagger для тестирования API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4. Подключаем ваши BLL и DAL сервисы
// builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Конфигурация middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowVue");
app.UseAuthorization();
app.MapControllers();

app.Run();