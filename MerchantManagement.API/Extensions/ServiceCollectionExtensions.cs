using FluentValidation;
using MerchantManagement.API.Requests;
using MerchantManagement.API.Validators;
using MerchantManagement.App.Merchants.Commands.Create;
using MerchantManagement.App.Merchants.Validators;
using MerchantManagement.Infra;
using Microsoft.EntityFrameworkCore;

namespace MerchantManagement.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CreateMerchantCommandHandler>());
            services.AddValidatorsFromAssemblyContaining<CreateMerchantCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateMerchantRequestValidator>();
            services.AddHttpClient();
            return services;

        }

        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("MerchantDb"));
            return services;
        }
    }
}
