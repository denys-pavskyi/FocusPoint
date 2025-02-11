using Microsoft.Extensions.DependencyInjection;

namespace FocusPoint.DAL.Configurations;

public class AppDbContextFactory
{
    private readonly IServiceProvider _serviceProvider;

    public AppDbContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public AppDbContext CreateDbContext()
    {
        var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        return dbContext;
    }
}