using System.ServiceModel;

namespace Numbers.Contracts
{
    public class ConverterClient : ClientBase<IConverter>, IConverter
    {
        public ConversionResult Convert(string input)
        {
            return Channel.Convert(input);
        }
    }
}