using AplicationDDD.DAL;
using AplicationDDD.DAL.Context;
using AplicationDDD.DAL.Repositories;
using AplicationDDD.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var service = builder.Services;
var connection_type = builder.Configuration["DataBase"];//��� �� ������ ������ ���������� � ����� ���� ������ �� ��������� ��������� ������ ����������� (�������� � appseting.json)
var connectio_string=builder.Configuration.GetConnectionString(connection_type);

switch (connection_type)//����� ������� �� ������� ����������� ����� ������ ������
{
    case "DockerDb":
    case "SqlServer"://���� ������ SqlServer �� ���������� ���
        service.AddDbContext<AppDB>(opt => opt.UseSqlServer(connectio_string,opt=>opt.MigrationsAssembly("AplicationDDD.DAL.MsQLServer")));//�� ��� ���� ������� �������� ������ ���� ������
        break;
    case "SqlLite":// ���� ������ SqlLite �� ���������� ���
        service.AddDbContext<AppDB>(opt => opt.UseSqlite(connectio_string,opt=>opt.MigrationsAssembly("ApplicationDDD.DAL.SqlLite")));
        break;
}

service.AddTransient<AppDbInicalizator>();//�������� ��� �� ������������ � �������

service.AddScoped(typeof(IRepositoryAsync<>), typeof(EntitiesRepositories<>));// ����� ������� ���� ������� ������ ����������� ������ ��� ���� ������ � ������������ 

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using(var scope = app.Services.CreateScope())//using ����� ��� ���� ��������� ������� ���� ������� 
{
    var initializer = scope.ServiceProvider.GetRequiredService<AppDbInicalizator>();

    await initializer.InitialAsync(RemoveBefore:false);//����� ������� ���� �� ������ ����� ��������� � ����������� ������ ��� ������� ���������� 
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
