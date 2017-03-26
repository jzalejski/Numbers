using System;

namespace Numbers
{
    public class InputInvalidFormatExcetpion : Exception
    {
        public InputInvalidFormatExcetpion(string input) : base($"Intput: {input} is not valid input.")
        {
        }
    }
}