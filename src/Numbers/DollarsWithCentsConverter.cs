using static System.String;

namespace Numbers
{
    public class DollarsWithCentsConverter : INumbersConverter
    {
        public string Convert(string input)
        {
            int dollarsToDo, centsToDo;
            ParseInput(input, out dollarsToDo, out centsToDo);
            var dollars = new DollarsConverter().Convert(dollarsToDo);
            var cents = Empty;
            if (centsToDo > 0)
            {
                cents = $" and {new CentsConverter().Convert(centsToDo)}";
            }
            return $"{dollars}{cents}"; 
        }

        private void ParseInput(string input, out int dollars, out int cents)
        {
            if (IsNullOrEmpty(input))
            {
                throw new InputInvalidFormatExcetpion(input);
            }
            var parts = input.Replace(" ", "").Split(',');
            var dollarsString = parts[0];
            if (!int.TryParse(dollarsString, out dollars))
            {
                throw new InputInvalidFormatExcetpion(input);
            }
            if (dollars > 999999999 || dollars < 0)
            {
                throw new InputInvalidFormatExcetpion(input);
            }
            cents = 0;
            if (parts.Length == 2)
            {
                var centsString = parts[1];
                if (centsString.Length > 2)
                {
                    throw new InputInvalidFormatExcetpion(input);
                }
                if (centsString.Length == 1)
                {
                    centsString += "0";
                }
                if (!int.TryParse(centsString, out cents))
                {
                    throw new InputInvalidFormatExcetpion(input);
                }
                if (cents < 0)
                {
                    throw new InputInvalidFormatExcetpion(input);
                }

            }
            else if (parts.Length > 2)
            {
                throw new InputInvalidFormatExcetpion(input);
            }
        }
    }

    public interface INumbersConverter
    {
        string Convert(string input);
    }
}