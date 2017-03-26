using NUnit.Framework;
using Rhino.Mocks;

namespace Numbers.Services.Tests
{
    [TestFixture]
    public class DollarsConvererServiceTests
    {
        [Test]
        public void ShouldSetErrorIfParsingInvalidMessage()
        {
            var mock = MockRepository.GenerateMock<INumbersConverter>();
            var input = "bad input";
            var excetpionThrown = new InputInvalidFormatExcetpion(input);
            mock.Stub(p => p.Convert(Arg<string>.Is.Anything)).Throw(excetpionThrown);
            var service = new DollarsConverterService(mock);

            var result = service.Convert(input);
            Assert.That(result.Error, Is.EqualTo(excetpionThrown.Message));
            Assert.That(result.Words, Is.Null);
        }

        [Test]
        public void ShouldSetResultIfParsingValidMessage()
        {
            var mock = MockRepository.GenerateMock<INumbersConverter>();
            var returnedValue = "test result";
            mock.Stub(p => p.Convert(Arg<string>.Is.Anything)).Return(returnedValue);
            var service = new DollarsConverterService(mock);
            var result = service.Convert("12,21");
            Assert.That(result.Words, Is.EqualTo(returnedValue));
            Assert.That(result.Error, Is.Null);
        }
    }
}