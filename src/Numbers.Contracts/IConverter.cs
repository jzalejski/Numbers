using System.ServiceModel;

namespace Numbers.Contracts
{
    [ServiceContract]
    public interface IConverter
    {
        [OperationContract]
        ConversionResult Convert(string input);
    }
}