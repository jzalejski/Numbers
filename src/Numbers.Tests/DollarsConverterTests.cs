using System;
using System.Collections.Generic;
using NUnit.Framework;
using static System.String;

namespace Numbers.Tests
{
    [TestFixture]
    public class DollarsConverterTests
    {
        [TestCase("0", "zero dollars")]
        [TestCase("1", "one dollar")]
        [TestCase("25,10", "twenty-five dollars and ten cents")]
        [TestCase("0,1", "zero dollars and ten cents")]
        [TestCase("0,01", "zero dollars and one cent")]
        [TestCase("100", "one hundred dollars")]
        [TestCase("200", "two hundred dollars")]
        [TestCase("257", "two hundred fifty-seven dollars")]
        [TestCase("16", "sixteen dollars")]
        [TestCase("61", "sixty-one dollars")]
        [TestCase("45 100", "forty-five thousand one hundred dollars")]
        [TestCase("999 999 999,99", "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        [TestCase("16 000", "sixteen thousand dollars")]
        [TestCase("16 000 000", "sixteen million dollars")]
        public void ShouldConvertDollarsToWordAsExpected(string input, string expectedOutput)
        {
            var dl = new DollarsWithCentsConverter();
            var actual = dl.Convert(input);
            Assert.That(actual, Is.EqualTo(expectedOutput));
        }
    }

    public class DollarsWithCentsConverter
    {
        public string Convert(string input)
        {
            var parts = input.Replace(" ", "").Split(',');
            var dollarsString = parts[0];
            var dollars = new DollarsConverter().Convert(int.Parse(dollarsString));
            var cents = Empty;
            if (parts.Length > 1)
            {
                cents = parts[1];
                if (cents.Length != 2)
                {
                    cents += "0";
                }
                cents = $" and {new CentsConverter().Convert(int.Parse(cents))}";
            }
            return $"{dollars}{cents}";
        }
    }

    public class ThousandConverter : EnglishNumbersConverter
    {
        protected override string UnitName => "thousand";

        protected override string GetUnit(int value)
        {
            return UnitName;
        }
    }
    public class MillionConverter : EnglishNumbersConverter
    {
        protected override string UnitName => "million";

        protected override string GetUnit(int value)
        {
            return UnitName;
        }
    }

    public abstract class EnglishNumbersConverter
    {
        protected readonly Dictionary<int, string> Dictionary = new Dictionary<int, string>
        {
            {1, "one" },
            {2, "two" },
            {3, "three"},
            {4, "four"},
            {5, "five"},
            {6, "six"},
            {7, "seven"},
            {8, "eight"},
            {9, "nine"},
            {10, "ten"},
            {11, "eleven"},
            {12, "twelve"},
            {13, "thirteen"},
            {15, "fifteen"},
            {18, "eighteen"},
            {20, "twenty" },
            {30, "thirty" },
            {40, "forty" },
            {50, "fifty" },
            {80, "eighty" },

        };

        protected abstract string UnitName { get; }

        public string Convert(int value)
        {
            var resultParts = new List<string>();
            var toDo = value;
            if (toDo == 0)
            {
                resultParts.Add("zero");
                return ReturnResult(value, resultParts);
            }
            if (toDo > 1000000 - 1)
            {

                var milions = Math.DivRem(toDo, 1000000, out toDo);
                resultParts.Add(new MillionConverter().Convert(milions));
            }
            if (toDo > 1000 - 1)
            {

                var thousands = Math.DivRem(toDo, 1000, out toDo);
                resultParts.Add(new ThousandConverter().Convert(thousands));
            }
            if (toDo > 100 - 1)
            {
                var hundreds = Math.DivRem(toDo, 100, out toDo);
                resultParts.Add($"{Dictionary[hundreds]} hundred");
            }
            if (toDo > 20 - 1)
            {
                var tempParts = new List<string>();
                int units;
                var tens = Math.DivRem(toDo, 10, out units);
                if (Dictionary.ContainsKey(tens * 10))
                {
                    tempParts.Add(Dictionary[tens * 10]);
                }
                else
                {
                    tempParts.Add(Dictionary[tens]+"ty");
                }
                tempParts.Add(Dictionary[units]);
                resultParts.Add(Join("-", tempParts));
                toDo = 0;
            }
            if (toDo>10)
            {
                if (Dictionary.ContainsKey(toDo))
                {
                    resultParts.Add(Dictionary[toDo]);
                }
                else
                {
                    resultParts.Add($"{Dictionary[toDo%10]}teen");
                }
            }
            else if(toDo > 0)
            {
                resultParts.Add(Dictionary[toDo]);
            }
            

            return ReturnResult(value, resultParts);
        }

        private string ReturnResult(int value, List<string> resultParts)
        {
            var unit = GetUnit(value);
            resultParts.Add(unit);
            return Join(" ", resultParts);
        }

        protected virtual string GetUnit(int value)
        {
            return value != 1? UnitName + "s" : UnitName;
        }
    }

    public class DollarsConverter : EnglishNumbersConverter
    {
        protected override string UnitName => "dollar";
    }

    public class CentsConverter : EnglishNumbersConverter
    {
        protected override string UnitName => "cent";

    }
}
