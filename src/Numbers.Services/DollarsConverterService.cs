using Numbers.Contracts;

namespace Numbers.Services
{
    public class DollarsConverterService : IConverterService
    {
        private readonly INumbersConverter _converter;

        public DollarsConverterService(INumbersConverter converter)
        {
            _converter = converter;
        }

        public ConversionResult Convert(string input)
        {
            try
            {
                var result = _converter.Convert(input);
                return new ConversionResult
                {
                    Words = result
                };
            }
            catch (InputInvalidFormatExcetpion e)
            {
                return new ConversionResult
                {
                    Error = e.Message
                };
            }
        }
    }
}