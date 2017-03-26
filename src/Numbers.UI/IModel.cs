using Numbers.Contracts;

namespace Numbers.UI
{
    public interface IModel
    {
        ConversionResult Convert(string userInput);
    }

    class Model : IModel
    {
        public ConversionResult Convert(string userInput)
        {
            return new ConverterClient().Convert(userInput);
        }
    }
}