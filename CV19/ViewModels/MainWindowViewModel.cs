
using CV19.ViewModels.Base;
using System.Windows;
using System.Windows.Input;
using CV19.Infrastructure.Commands;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            //#region Commands
            //CloseAppllicationCommand = new LambdaCommand()
            //#endregion
        }

        #region Title : string - Заголовок окна
        private string _Title = "Анализ статистики CV19";
        /// <summary>
        /// Заголовок для - представления главного окна
        /// </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
            #region Аналог того, что осуществляет, реализованный в базовом классе метод - Set
            //if (Equals(_Title, value)
            //return;
            //_Title = value;
            //OnPropertyChanged();
            #endregion
        }
        #endregion

        #region Status : string - Статус программы
        private string _Status = "Функционирует";
        /// <summary>
        /// Статус у программы
        /// </summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }
        #endregion

        #region Commands
        #region CloseApplicationCommand
        private ICommand _CloseAppllicationCommand;
        public ICommand CloseAppllicationCommand => _CloseAppllicationCommand ??= new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
        private bool CanCloseApplicationCommandExecute(object parameters)
            => true;
        private void OnCloseApplicationCommandExecuted(object parameters)
            => Application.Current.Shutdown(); 
        #endregion
        #endregion
    }
}
