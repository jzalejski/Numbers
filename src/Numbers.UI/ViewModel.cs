using System.ComponentModel;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Numbers.UI
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly IModel _model;
        private string _error;
        private string _result;
        private string _userInput;


        public ViewModel(IModel model)
        {
            _model = model;
            ConvertCommand = new RelayCommand(p => true, async p => await Convert());
        }

        public ICommand ConvertCommand { get; set; }

        public string UserInput
        {
            get { return _userInput; }
            set
            {
                if (value == _userInput) return;

                _userInput = value;
                OnPropertyChanged(nameof(UserInput));
            }
        }

        public string Result
        {
            get { return _result; }
            set
            {
                if (value == _result) return;

                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public string Error
        {
            get { return _error; }
            set
            {
                if (value == _error) return;
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        private async Task Convert()
        {
            try
            {
                var response = await _model.Convert(_userInput);
                Result = response.Words;
                Error = response.Error;
            }
            catch (CommunicationException e)
            {
                Error = e.Message;
            }
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