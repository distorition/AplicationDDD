using AplicatioDDD.Domain.Entities;
using AplicationDDD.Interfaces;
using AplicationDDD.WebApe.Client.Enmployees;

var builder = WebApplication.CreateBuilder(args);

var service = builder.Services;
service.AddControllersWithViews();

/// <summary>
/// ����� ������� �� ������������� ������ ������� �� ������ WebApi( ��� ���� �������� , ��� ����� ���������� � json �����)  ����� ������ ����� �� ���� �����������
/// </summary>
service.AddHttpClient("AplicationWebAPI",client=>client.BaseAddress=new(builder.Configuration["WebApi"]))
    .AddTypedClient<IRepositoryAsync<Employe>,EmployesClient>();// ��� �� ��������� ���� ������� � ����� ������� �� ������ ������������ ��� ����������� � ����������� � ���� ������� 

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
 
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
