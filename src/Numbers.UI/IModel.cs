using Numbers.Contracts;

namespace Numbers.UI
{
    public interface IModel
    {
        string Convert(string userInput);
    }

    class Model : IModel
    {
        public string Convert(string userInput)
        {
            return new ConverterClient().Convert(userInput).Words;
        }
    }
}