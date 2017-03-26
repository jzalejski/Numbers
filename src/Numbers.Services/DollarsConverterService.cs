using log4net;
using Numbers.Contracts;

namespace Numbers.Services
{
    public class DollarsConverterService : IConverterService
    {
        private readonly INumbersConverter _converter;
        private static ILog Log = LogManager.GetLogger(typeof (DollarsConverterService));

        public DollarsConverterService(INumbersConverter converter)
        {
            _converter = converter;
        }

        public ConversionResult Convert(string input)
        {
            try
            {
                Log.Info($"Converting input: {input}.");
                var result = _converter.Convert(input);
                Log.Info($"Input: {input} converted. Result: {result}.");
                return new ConversionResult
                {
                    Words = result
                };
            }
            catch (InputInvalidFormatExcetpion e)
            {
                Log.Warn($"Converting input: {input} failed. Error:", e);
                return new ConversionResult
                {
                    Error = e.Message
                };
            }
        }
    }
}