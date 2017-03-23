using System.ComponentModel;
using System.Windows.Input;

namespace Numbers.UI
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly IModel model;
        private string userInput;
        private string result;
        

        public ViewModel(IModel model)
        {
            this.model = model;
            ConvertCommand = new RelayCommand(p=>true, p=>Convert());

        }

        public ICommand ConvertCommand { get; set; }

        public string UserInput
        {
            get { return userInput; }
            set
            {
                if (value == userInput) return;

                userInput = value;
                OnPropertyChanged(nameof(UserInput));
            }

        }

        public string Result
        {
            get { return result; }
            set
            {
                if (value == result) return;

                result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        private void Convert()
        {
            Result = model.Convert(userInput);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion



    }
}