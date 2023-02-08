
using CV19.ViewModels.Base;
namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
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
    }
}
