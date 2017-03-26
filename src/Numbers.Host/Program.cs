using System;
using System.ServiceModel;
using Numbers.Services;

namespace Numbers.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(DollarsConverterService));
            host.Open();
            Console.WriteLine("host is running");
            Console.ReadLine();
        }
    }
}
