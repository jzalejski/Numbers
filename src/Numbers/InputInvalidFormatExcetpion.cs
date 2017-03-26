using System;

namespace Numbers
{
    public class InputInvalidFormatExcetpion : Exception
    {
        public InputInvalidFormatExcetpion(string input) : base($"{input} is not valid input.")
        {
        }
    }
}