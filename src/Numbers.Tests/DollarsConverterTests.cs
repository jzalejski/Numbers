using NUnit.Framework;

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
        [TestCase("999 999 999,99",
            "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents"
            )]
        [TestCase("16 000", "sixteen thousand dollars")]
        [TestCase("16 000 000", "sixteen million dollars")]
        [TestCase("567,05", "five hundred sixty-seven dollars and five cents")]
        [TestCase("90", "ninety dollars")]
        public void ShouldConvertDollarsToWordAsExpected(string input, string expectedOutput)
        {
            var dl = new DollarsWithCentsConverter();
            var actual = dl.Convert(input);
            Assert.That(actual, Is.EqualTo(expectedOutput));
        }

        [TestCase("not numbers")]
        [TestCase("13,sadfsad")]
        [TestCase("asf,12")]
        [TestCase("123,122")]
        [TestCase("123,12, 5214")]
        [TestCase("1 000 000 000")]
        [TestCase("-1")]
        [TestCase("1,-1")]
        [TestCase(null)]
        public void ShouldThrowExceptionWithExpectedMessageIfInputIsNotValid(string input)
        {
            var dl = new DollarsWithCentsConverter();
            var e = Assert.Throws<InputInvalidFormatExcetpion>(() => dl.Convert(input));
            Assert.That(e.Message, Is.EqualTo($"{input} is not valid input."));
        }
    }
}