using NUnit.Framework;
using Rhino.Mocks;

namespace Numbers.UI.Tests
{
    [TestFixture]
    public class ViewModelTests
    {
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
        public void ShouldCallConvertMethodFromModel()
        {
            var model = MockRepository.GenerateMock<IModel>();
            var vm = new ViewModel(model);
            var testInput = "1231";
            vm.UserInput = testInput;
            vm.ConvertCommand.Execute(null);
            model.AssertWasCalled(p=>p.Convert(testInput), p=>p.Repeat.Once());
        }

        [Test]
        public void ShouldSetValueReturnedByModelToResult()
        {
            var model = MockRepository.GenerateMock<IModel>();
            var result = "test result";
            model.Stub(p => p.Convert(Arg<string>.Is.Anything)).Return(result);
            var vm = new ViewModel(model);
            var testInput = "1231";
            vm.UserInput = testInput;
            vm.ConvertCommand.Execute(null);
            Assert.That(vm.Result, Is.EqualTo(result));
        }
    }
}

