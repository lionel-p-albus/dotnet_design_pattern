using Todo.Shared.Constants;
using Todo.Shared.Facades.Interface;
using Todo.Shared.Mappers;
using Todo.Shared.Mappers.Interface;
using Todo.Shared.Models;
using Todo.Shared.Utils;

namespace Todo.Extentions
{
    public static class ServicesExtension
    {
        public static void ConfigureScopeFacades(this IServiceCollection services)
        {
            var readings =
                SharedUtils
                    .GenerateMeterElectricityReadings(); // Generate the electricity readings for the smart meters.

            services
                .AddTransient<IMeterReadingFacade,
                    MeterReadingFacade>(); // Add a transient service of the IMeterReadingFacade type to the services container.
            services.AddTransient<IAccountFacade, AccountFacade>();
            services.AddTransient<IPricePlanComparatorFacade, PricePlanComparatorFacade>();

            // Add a singleton service of the Dictionary<string, List<ElectricityReading> type to the services container.
            services.AddSingleton<MeterAssociatedReadings, MeterAssociatedReadings>(x =>
                new MeterAssociatedReadings(
                    readings)); // Add a singleton service of the IServiceProvider type to the services container.
            services.AddSingleton<Dictionary<string, string>, Dictionary<string, string>>(x =>
                Data.SmartMeterToPricePlanAccounts);
            services.AddSingleton<List<PricePlan>, List<PricePlan>>(x => Data.PricePlans);
        }

        public static void ConfigureScopeRepository(this IServiceCollection services)
        {
        }

        public static void ConfigureScopeService(this IServiceCollection services)
        {
        }

        public static void ConfigureScopeMappers(this IServiceCollection services)
        {
            services.AddTransient<IModelMapper, ModelMapper>();
        }

        public static void ConfigureAddHttpClient(this IServiceCollection services)
        {
        }
    }
}