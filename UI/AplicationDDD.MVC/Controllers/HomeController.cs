using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AplicationDDD.MVC.Models;
using AplicationDDD.Interfaces;
using AplicatioDDD.Domain.Entities;

namespace AplicationDDD.MVC.Controllers;

public class HomeController : Controller
{
    private readonly IRepositoryAsync<Employe> Employes;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IRepositoryAsync<Employe> repositoryAsync,ILogger<HomeController> logger)
    {
        Employes = repositoryAsync;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {   
        var employe= await Employes.GetAllAsync();
        return View(employe);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
