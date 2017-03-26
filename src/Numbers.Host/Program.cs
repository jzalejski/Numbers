﻿using System;
using System.Reflection;
using System.ServiceModel;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Ninject;
using Ninject.Extensions.Wcf;
using Ninject.Extensions.Wcf.SelfHost;
using Ninject.Web.Common.SelfHost;
using Numbers.Services;

namespace Numbers.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var bs = new Bootstrapper();
            bs.Start();
            Console.WriteLine("host is running");
            Console.ReadLine();
        }

        
    }

    public class Bootstrapper
    {
        private NinjectSelfHostBootstrapper selfHost;

        private void StartNinjectSelfHost()
        {
            var someWcfService = NinjectWcfConfiguration.Create<DollarsConverterService, NinjectServiceSelfHostFactory>();
            var webApiConfiguration = new HttpSelfHostConfiguration("http://localhost:8080");
            webApiConfiguration.Routes.MapHttpRoute(
                        name: "DefaultApi",
                        routeTemplate: "{controller}/{id}",
                        defaults: new { id = RouteParameter.Optional, controller = "values" });

            this.selfHost = new NinjectSelfHostBootstrapper(
                CreateKernel,
                someWcfService,
                webApiConfiguration);
            this.selfHost.Start();
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Load(typeof(DollarsConverterService).Assembly);
            return kernel;
        }

        public void Start()
        {
            StartNinjectSelfHost();
        }
    }
}
