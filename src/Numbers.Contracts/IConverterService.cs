using System.ServiceModel;

namespace Numbers.Contracts
{
    [ServiceContract]
    public interface IConverterService
    {
        [OperationContract]
        ConversionResult Convert(string input);
    }
}