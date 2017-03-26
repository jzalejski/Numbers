using System.ComponentModel;
using System.Windows.Input;

namespace Numbers.UI
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly IModel model;
        private string userInput;
        private string result;
        private string _error;


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

        public string Error
        {
            get { return _error; }
            set
            {
                if(value == _error) return;
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        private void Convert()
        {
            var response = model.Convert(userInput);
            Result = response.Words;
            Error = response.Error;
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