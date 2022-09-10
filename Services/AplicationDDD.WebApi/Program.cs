using AplicationDDD.DAL;
using AplicationDDD.DAL.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var service = builder.Services;
var connection_type = builder.Configuration["DataBase"];//так мы сможем менять пдключения к нашей базе данных не использую постоянно строку подключения (указанно в appseting.json)
var connectio_string=builder.Configuration.GetConnectionString(connection_type);

switch (connection_type)//таким образом мы сделали переклюения между базами данных
{
    case "SqlServer"://если указан SqlServer то подключаем его
        service.AddDbContext<AppDB>(opt => opt.UseSqlServer(connectio_string,opt=>opt.MigrationsAssembly("AplicationDDD.DAL.MsQLServer")));//но нам надо указать название сборки базы данных
        break;
    case "SqlLite":// если указан SqlLite то подключаем его
        service.AddDbContext<AppDB>(opt => opt.UseSqlite(connectio_string,opt=>opt.MigrationsAssembly("ApplicationDDD.DAL.SqlLite")));
        break;
}

service.AddTransient<AppDbInicalizator>();//добавили наш ДБ иницализатор в сервисы

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using(var scope = app.Services.CreateScope())//using чтобы все наши верменные обьекты были удалены 
{
    var initializer = scope.ServiceProvider.GetRequiredService<AppDbInicalizator>();

    await initializer.InitialAsync(RemoveBefore:true);//таким образом наша бд всегда будет удаляться и создаваться заново при запуске приложения 
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
