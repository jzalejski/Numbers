using Ninject.Modules;
using Numbers.Contracts;

namespace Numbers.Services
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IConverter>().To<DollarsConverterService>();
            this.Bind<INumbersConverter>().To<DollarsWithCentsConverter>();
        }
    }
}