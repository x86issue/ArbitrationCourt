using AC.Domain.Repositories.Abstractions;
using AC.Infrastructure.EntityFramework.RepositoriesEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AC.Infrastructure.EntityFramework;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped(typeof(IRepository<,>), typeof(EFRepository<,>));
        services.AddScoped<ICaseRepository, CaseRepository>();
        services.AddScoped<IArbitratorRepository, ArbitratorRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ISettlementProposalRepository, SettlementProposalRepository>();

        return services;
    }
}
