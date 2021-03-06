﻿using System.Threading.Tasks;
using Numbers.Contracts;

namespace Numbers.UI
{
    public interface IModel
    {
        Task<ConversionResult> Convert(string userInput);
    }

    public class Model : IModel
    {
        private readonly IConverterService _converter;

        public Model(IConverterService converter)
        {
            _converter = converter;
        }

        public async Task<ConversionResult> Convert(string userInput)
        {
            return await Task.Run(() => _converter.Convert(userInput));
        }
    }
}