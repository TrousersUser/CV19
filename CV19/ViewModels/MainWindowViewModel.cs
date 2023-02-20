using CV19.ViewModels.Base;
using System.Windows;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using CV19.Models.Decanat;
using System.Linq;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        /*------------------------------------------------------------------------------------------------------------------------------- */
        public MainWindowViewModel()
        {
            //#region Commands
            //CloseAppllicationCommand = new LambdaCommand()
            //#endregion

            #region GraphLogic
            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }
            TestDataPoints = data_points;
            #endregion

            #region Student groups enumerable creating
            Random rand = new Random();

            int studentIndex = 1;
            var students = Enumerable.Range(1, 10)
                .Select(i => new Student()
                {
                    Name = $"Имя {studentIndex}",
                    Surname = $"Фамилия {studentIndex}",
                    Patronymic = $"Отчество {studentIndex++}",
                    Rating = Math.Round(rand.NextDouble(),3),
                    Birthday = DateTime.Now 
                }) ;

            var groups = Enumerable.Range(1, 20)
                .Select(i => new Group()
                {
                    Name = $"Группа {i}",
                    Students = new ObservableCollection<Student>(students)
                });
            Groups = new ObservableCollection<Group>(groups);
            #endregion
        }

        /*------------------------------------------------------------------------------------------------------------------------------- */

        public ObservableCollection<Group> Groups { get; }

        #region SelectedGroup : Group - Выбранная группа
        private Group _SelectedGroup;
        /// <summary>
        /// Свойство, указывающее на поле, хранящее выбранную в главном представлении группу, в качестве экземпляра класса Group.
        /// </summary>
        public Group SelectedGroup
        {
            get => _SelectedGroup;
            set => Set(ref _SelectedGroup, value);
        }
        #endregion

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

        #region TestDataPoints : IEnumerable - Пробная информация, выводимая на график
        private IEnumerable<DataPoint> _TestDataPoints;
        /// <summary>
        /// Перечисление, включающее в себя объекты, хранящие координаты для построения графика.
        /// </summary>
        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _TestDataPoints;
            set => Set(ref _TestDataPoints, value);
        }
        #endregion

        #region SelectedTabIndex : int - Выбранная вкладка, у объекта TabControl
        private int _SelectedTabIndex = 0;
        /// <summary>
        /// Номер текущей вкладки, выбранной у объекта TabControl в представлении.
        /// </summary>
        public int SelectedTabIndex
        {
            get => _SelectedTabIndex;
            set => Set(ref _SelectedTabIndex, value);
        }

        #endregion


        /*------------------------------------------------------------------------------------------------------------------------------- */

        #region Commands
        #region CloseApplicationCommand
        private ICommand _CloseAppllicationCommand;
        public ICommand CloseAppllicationCommand => _CloseAppllicationCommand ??= new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
        private bool CanCloseApplicationCommandExecute(object parameters)
            => true;
        private void OnCloseApplicationCommandExecuted(object parameters)
            => Application.Current.Shutdown();
        #endregion

        #region ChangeTabIndexCommand
        private ICommand _ChangeTabIndexCommand;
        public ICommand ChangeTabIndexCommand => _ChangeTabIndexCommand ??= new LambdaCommand(OnChangeTabIndexCommandExecute, CanChangeTabIndexCommandExecuted);

        private bool CanChangeTabIndexCommandExecuted(object parameters) => _SelectedTabIndex >= 0;
        private void OnChangeTabIndexCommandExecute(object parameters)
        {
            if ((parameters is null)) return;
            SelectedTabIndex += Convert.ToInt32(parameters);
        }
        #endregion
        #endregion

        /*------------------------------------------------------------------------------------------------------------------------------- */

    }
}
