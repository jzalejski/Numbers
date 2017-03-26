using System;
using System.Collections.Generic;

namespace Numbers
{
    public abstract class EnglishNumbersConverter
    {
        protected readonly Dictionary<int, string> Dictionary = new Dictionary<int, string>
        {
            {0, "zero" },
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
                resultParts.Add(Dictionary[toDo]);
                return ReturnResult(GetUnit(toDo), resultParts);
            }
            CalculateMillions(ref toDo, resultParts);
            CalculateThousands(ref toDo, resultParts);
            return ConvertInternal(toDo, resultParts, GetUnit(value));
        }

        private string ConvertInternal(int toDo, List<string> resultParts, string unit)
        {
            CalculateHundreds(ref toDo, resultParts);
            CalculateTens(ref toDo, resultParts);
            if (toDo > 10)
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


            return ReturnResult(unit, resultParts);
        }

        private void CalculateTens(ref int toDo, List<string> resultParts)
        {
            if (toDo > 20 - 1)
            {
                var tempParts = new List<string>();
                int units;
                var tens = Math.DivRem(toDo, 10, out units);
                if (Dictionary.ContainsKey(tens*10))
                {
                    tempParts.Add(Dictionary[tens*10]);
                }
                else
                {
                    tempParts.Add(Dictionary[tens] + "ty");
                }
                if (units > 0)
                {
                    tempParts.Add(Dictionary[units]);
                }
                resultParts.Add(string.Join("-", tempParts));
                toDo = 0;
            }
        }

        private void CalculateHundreds(ref int toDo, List<string> resultParts)
        {
            if (toDo > 100-1)
            {
                var hundreds = Math.DivRem(toDo, 100, out toDo);
                ConvertInternal(hundreds, resultParts, "hundred");
            }
        }

        private void CalculateThousands(ref int toDo, List<string> resultParts)
        {
            if (toDo > 1000 - 1)
            {
                var thousands = Math.DivRem(toDo, 1000, out toDo);
                ConvertInternal(thousands, resultParts, "thousand");
            }
        }

        private void CalculateMillions(ref int toDo, List<string> resultParts)
        {
            if (toDo > 1000000 - 1)
            {
                var milions = Math.DivRem(toDo, 1000000, out toDo);
                ConvertInternal(milions, resultParts, "million");
            }
        }

        private string ReturnResult(string unit, List<string> resultParts)
        {
            resultParts.Add(unit);
            return String.Join(" ", resultParts);
        }

        protected virtual string GetUnit(int value)
        {
            return value != 1? UnitName + "s" : UnitName;
        }
    }
}