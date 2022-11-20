using WorkOrderManagerServer.App.Extensions;
using WorkOrderManagerServer.App;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplication.CreateBuilder(args).UseStartup<Startup>();
    }
}