using System.ServiceModel;
using System.Threading.Tasks;
using Numbers.Contracts;
using NUnit.Framework;
using Rhino.Mocks;

namespace Numbers.UI.Tests
{
    [TestFixture]
    public class ViewModelTests
    {
        [Test]
        public void ShouldCallConvertMethodFromModel()
        {
            var model = MockRepository.GenerateMock<IModel>();
            var result = Task.FromResult(new ConversionResult());
            model.Stub(p => p.Convert(Arg<string>.Is.Anything)).Return(result);
            var vm = new ViewModel(model);
            var testInput = "1231";
            vm.UserInput = testInput;
            vm.ConvertCommand.Execute(null);
            model.AssertWasCalled(p => p.Convert(testInput), p => p.Repeat.Once());
        }

        [Test]
        public void ShouldNotifyOnErrorChanged()
        {
            var wasCalled = false;
            var vm = new ViewModel(null);
            string propertyName = null;
            vm.PropertyChanged += (s, e) =>
            {
                wasCalled = true;
                propertyName = e.PropertyName;
            };
            vm.Error = "test";
            Assert.That(wasCalled);
            Assert.That(propertyName, Is.EqualTo(nameof(vm.Error)));
        }

        [Test]
        public void ShouldNotifyOnResultChanged()
        {
            var wasCalled = false;
            var vm = new ViewModel(null);
            string propertyName = null;
            vm.PropertyChanged += (s, e) =>
            {
                wasCalled = true;
                propertyName = e.PropertyName;
            };
            vm.Result = "test";
            Assert.That(wasCalled);
            Assert.That(propertyName, Is.EqualTo(nameof(vm.Result)));
        }

        [Test]
        public void ShouldNotifyOnUserInputChanged()
        {
            var wasCalled = false;
            var vm = new ViewModel(null);
            string propertyName = null;
            vm.PropertyChanged += (s, e) =>
            {
                wasCalled = true;
                propertyName = e.PropertyName;
            };
            vm.UserInput = "test";
            Assert.That(wasCalled);
            Assert.That(propertyName, Is.EqualTo(nameof(vm.UserInput)));
        }

        [Test]
        public void ShouldSetErrorIfCommunicationError()
        {
            var model = MockRepository.GenerateMock<IModel>();
            var testError = "timeout error";
            model.Stub(p => p.Convert(Arg<string>.Is.Anything)).Throw(new CommunicationException(testError));
            var vm = new ViewModel(model);
            var testInput = "1231";
            vm.UserInput = testInput;
            vm.ConvertCommand.Execute(null);
            Assert.That(vm.Error, Is.EqualTo(testError));
        }

        [Test]
        public void ShouldSetErrorIfParsingFailed()
        {
            var model = MockRepository.GenerateMock<IModel>();
            var testError = "test error";
            var result = Task.FromResult(new ConversionResult
            {
                Error = testError
            });
            model.Stub(p => p.Convert(Arg<string>.Is.Anything)).Return(result);
            var vm = new ViewModel(model);
            var testInput = "1231";
            vm.UserInput = testInput;
            vm.ConvertCommand.Execute(null);
            Assert.That(vm.Error, Is.EqualTo(testError));
        }

        [Test]
        public void ShouldSetValueReturnedByModelToResult()
        {
            var model = MockRepository.GenerateMock<IModel>();
            var testResult = "test result";
            var result = Task.FromResult(new ConversionResult
            {
                Words = testResult
            });
            model.Stub(p => p.Convert(Arg<string>.Is.Anything)).Return(result);
            var vm = new ViewModel(model);
            var testInput = "1231";
            vm.UserInput = testInput;
            vm.ConvertCommand.Execute(null);
            Assert.That(vm.Result, Is.EqualTo(testResult));
        }
    }
}