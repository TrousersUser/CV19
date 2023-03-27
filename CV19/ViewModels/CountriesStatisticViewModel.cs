using CV19.ViewModels.Base;
using CV19.Services;
namespace CV19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModelBase
    {
        /*------------------------------------------------------------------------------------------------------------------------------- */
        private MainWindowViewModel mainViewModel { get;}
        /*------------------------------------------------------------------------------------------------------------------------------- */
        public CountriesStatisticViewModel(MainWindowViewModel _mainViewModel) 
            => (mainViewModel) = _mainViewModel;
        /*------------------------------------------------------------------------------------------------------------------------------- */
        private readonly DataService _dataService = new DataService();

    }
}
