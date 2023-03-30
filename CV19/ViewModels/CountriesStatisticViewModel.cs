using CV19.ViewModels.Base;
using CV19.Services;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models;
using System.Collections.Generic;
using System;

namespace CV19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModelBase
    {
        /*------------------------------------------------------------------------------------------------------------------------------- */
        /// <summary>
        /// ViewModels objects, placed inside fields or properties with readonly modificator.
        /// </summary>
        private MainWindowViewModel mainViewModel { get;}
        /*------------------------------------------------------------------------------------------------------------------------------- */
        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="_mainViewModel">MainWindowViewModel object</param>
        public CountriesStatisticViewModel(MainWindowViewModel _mainViewModel)
        => mainViewModel = _mainViewModel;
        
        /// <summary>
        /// Отладочный конструктор, в ходе разработки используемый в зависимых представлениях UserControl, в визуальном дизайнере
        /// </summary>
        public CountriesStatisticViewModel() : this(null)
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("Попытка использовать конструктор, используемый для визуального дизайнера, для компиляции целой сборки.");
        } 
        /*------------------------------------------------------------------------------------------------------------------------------- */
        /// <summary>
        /// Fields/Properties.
        /// </summary>
        #region dataService : DataService - Поле (readonly), хранящее экземпляр класса DataService
        private readonly DataService dataService = new DataService();
        #endregion

        #region Countries : IEnumerable<CountryInfo> - Свойство, возвращающее перечисление статистики о странах.
        /// <summary>
        /// Через свойство, получаем перечисление, возвращающее экземпляры класса CountryInfo, содержающие информацию о стране, в контексте сбора 
        /// данных об эпидемии Covid19.
        /// </summary>
        private IEnumerable<CountryInfo> _Countries;
        public IEnumerable<CountryInfo> Countries
        {
            get => _Countries;
            private set => Set(ref _Countries, value);
        }
        #endregion

        #region SelectedCountry : CountryInfo - Выбранный в представлении экземпляр класса CountryInfo.
        /// <summary>
        /// Свойство, возвращающее экземпляр для класса CountryInfo. 
        /// Объект, передаваемый, содержит информацию о стране, как источнике заболевания COVID19.
        /// </summary>
        private CountryInfo _SelectedCountry;
        public CountryInfo SelectedCountry
        {
            get => _SelectedCountry;
            set => Set(ref _SelectedCountry, value);
        }
        #endregion
        /*------------------------------------------------------------------------------------------------------------------------------- */
        #region Commands
        #region RefreshDataCommand - Обновление списка из данных.
        /// <summary>
        /// Команда, выполняющая функцию обноавления источника данных, хранящего перечисление CountryInfo объектов, представляющих описание,
        /// в контексте эпидемии Covid19 о странах.
        /// </summary>
        private ICommand  _RefreshDataCommand;
        public ICommand RefreshDataCommand => _RefreshDataCommand ??= new LambdaCommand(OnRefreshDataExecuted,CanRefreshDataExecute);
        private bool CanRefreshDataExecute(object param) => true;
        private void OnRefreshDataExecuted(object param)
        {
            Countries = dataService.GetData();
        }
	    #endregion
        #endregion
        /*------------------------------------------------------------------------------------------------------------------------------- */
    }
}
