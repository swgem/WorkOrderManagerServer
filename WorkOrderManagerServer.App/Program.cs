using WorkOrderManagerServer.Services;
using WorkOrderManagerServer.Repo;
using Microsoft.EntityFrameworkCore;
using WorkOrderManagerServer.Identity.Data;
using WorkOrderManagerServer.App.Extensions;
using WorkOrderManagerServer.App;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplication.CreateBuilder(args).UseStartup<Startup>();
    }
}