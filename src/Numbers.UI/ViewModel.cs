using System.ComponentModel;

namespace Numbers.UI
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly IController controller;
        private string userInput;
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel(IController controller)
        {
            this.controller = controller;
        }

        public string UserInput
        {
            get { return userInput; }
            set
            {
                if (value != userInput)
                {
                    userInput = value;
                    OnPropertyChanged(nameof(UserInput));
                }
                
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public interface IController
    {
        string Convert(string userInput);
    }
}