using Numbers.Contracts;
using NUnit.Framework;
using Rhino.Mocks;

namespace Numbers.UI.Tests
{
    [TestFixture]
    public class ModelTests
    {
        [Test]
        public void ShouldCallConverter()
        {
            var mock = MockRepository.GenerateMock<IConverterService>();
            var model = new Model(mock);
            var expectedResult = new ConversionResult();
            mock.Stub(p => p.Convert(Arg<string>.Is.Anything)).Return(expectedResult);
            var result = model.Convert("test").Result;
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}