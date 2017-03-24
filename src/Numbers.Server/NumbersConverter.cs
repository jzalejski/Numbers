using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Numbers.Server
{
    public class NumbersConverter : INumbersConverter
    {
        
        public string Convert(string input)
        {
            return "Hello world";
        }
    }
}
