using System.ServiceModel;

namespace Numbers.Contracts
{
    public class ConverterClient : ClientBase<IConverterService>, IConverterService
    {
        public ConversionResult Convert(string input)
        {
            return Channel.Convert(input);
        }
    }
}