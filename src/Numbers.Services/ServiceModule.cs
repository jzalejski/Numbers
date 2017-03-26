using Ninject.Modules;
using Numbers.Contracts;

namespace Numbers.Services
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConverterService>().To<DollarsConverterService>();
            Bind<INumbersConverter>().To<DollarsWithCentsConverter>();
        }
    }
}