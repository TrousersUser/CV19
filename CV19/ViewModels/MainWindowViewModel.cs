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
using System.Windows.Data;
using System.ComponentModel;

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

            _SelectedGroupStudents.Filter += OnStudentsFiltred;
            _GroupList = SourceCreating();
            _GroupList.Filter += OnGroupsFiltred;
            _GroupList.SortDescriptions.Add(new SortDescription("Name",ListSortDirection.Descending));

            
            //_SelectedGroupStudents.SortDescriptions.Add
            //    (
            //     new SortDescription("Name", ListSortDirection.Descending)
            //    );
            //_SelectedGroupStudents.GroupDescriptions.Add(new PropertyGroupDescription("Name"));
        }

        /*------------------------------------------------------------------------------------------------------------------------------- */
        public DirectoryViewModel DriveRootDir { get; } = new DirectoryViewModel(@"C:\");

        #region SeletedDirectory : DirectoryViewModel - Выбранный каталог/директория из фаловой системы.
        /// <summary>
        /// Свойство, возвращающее ссылку на экземпляр класса DirectoryViewMode, что является моделью представления,
        /// хранящей информацию об конкретном каталоге.
        /// </summary>
        private DirectoryViewModel _SelectedDirectory;
        public DirectoryViewModel SelectedDirectory
        {
            get => _SelectedDirectory;
            set => Set(ref _SelectedDirectory, value);
        }
        #endregion


        #region object[] : CompositeCollection - Массив, включающий информацию с различными типами - значимыми, ссылочными.
        private object[] _CompositeCollection;
        /// <summary>
        /// Массив, включающий информацию с различными типами - значимыми, ссылочными.
        /// </summary>
        public object[] CompositeCollection => _CompositeCollection ??= CompositeCollectionFilling();
        private object[] CompositeCollectionFilling()
        {
            var newGroup = Groups[0];

            var data_list = new List<object>()
            {
                "Dudes",
                12,
               newGroup,
               newGroup.Students[0]
            };

            return data_list.ToArray();
        }
        #endregion

        #region SelectedCompositeValue : object - Выбранное значение из композиции
        private object _SelectedCompositeValue;
        /// <summary>
        /// Выделенный объект из массива с различными типами данных.
        /// </summary>
        public object SelectedCompositeValue
        {
            get => _SelectedCompositeValue;
            set => Set(ref _SelectedCompositeValue, value);
        }
        #endregion


        #region ObservableCollection<Group> : Groups - Поле, возвращающее коллекцию наблюдаемую, с сущностями - экземплярами класса Group
        private ObservableCollection<Group> _Groups;
        /// <summary>
        /// Groups - Поле, возвращающее коллекцию наблюдаемую, с сущностями - экземплярами класса Group
        /// </summary>
        public ObservableCollection<Group> Groups => _Groups ??= AddGroupsInCollection();
        private ObservableCollection<Group> AddGroupsInCollection()
        {
            Random rand = new Random();

            int studentIndex = 1;
            var students = Enumerable.Range(1, 10)
                .Select(i => new Student()
                {
                    Name = $"Имя {studentIndex}",
                    Surname = $"Фамилия {studentIndex}",
                    Patronymic = $"Отчество {studentIndex++}",
                    Rating = Math.Round(rand.NextDouble(), 3),
                    Birthday = DateTime.Now
                });

            var groups = Enumerable.Range(1, 20)
                .Select(i => new Group()
                {
                    Name = $"Группа {i}",
                    Students = new ObservableCollection<Student>(students)
                });
            ObservableCollection<Group> groupsCollection = new ObservableCollection<Group>(groups);


            return groupsCollection;
        }
        #endregion

        #region Groups : ICollectionView - Свойство, возвращающее объект-список, включающий в себя экземпляры сущности Group
        /// <summary>
        /// Свойство, возвращающее объект-список, включающий в себя экземпляры сущности Group
        /// </summary>
        public CollectionViewSource _GroupList;
        private CollectionViewSource SourceCreating()
        {
            CollectionViewSource toReturn = new CollectionViewSource();
            toReturn.Source = Groups;
            return toReturn;
        }
        public ICollectionView? GroupList => _GroupList?.View;
        #endregion

        #region GroupsFilterText : string - Свойство, возвращающее данные, вводимые в строку для фильтрации.
        private string _GroupsFilterText;
        public string GroupsFilterText
        {
            get => _GroupsFilterText;
            set
            {
                Set(ref _GroupsFilterText, value);
                _GroupList.View.Refresh();
            }
        }
        #endregion

        #region GroupsFiltred : void - Метод, помещающийся в событие Filter, для фильтрации объектов, выводимых в представлении, из коллекции.
        /// <summary>
        /// Метод, помещающийся в событие Filter, для фильтрации объектов, выводимых в представлении, из коллекции.
        /// </summary>
        /// <param name="sender"> Объект, совершивый действий, что вызывали событие</param>
        /// <param name="e"> экземпляр класса FilterEventArgs. Содержит информацию о событии</param>
        public void OnGroupsFiltred(object? sender, FilterEventArgs e)
        {
            if (!(e.Item is Group group))
            {
                e.Accepted = false;
                return;
            }

            string filterText = GroupsFilterText;
            if (string.IsNullOrEmpty(filterText))
            {
                return;
            }

            if (group.Name == null)
            {
                e.Accepted = false;
                return;
            }

            if (group.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;
            else if (!(string.IsNullOrEmpty(group.Description)) &&
                group.Description.Contains(filterText)) return;

            e.Accepted = false;
        }
        #endregion

        #region SelectedGroup : Group - Выбранная группа
        private Group _SelectedGroup;
        /// <summary>
        /// Свойство, указывающее на поле, хранящее выбранную в главном представлении группу, в качестве экземпляра класса Group.
        /// </summary>
        public Group SelectedGroup
        {
            get => _SelectedGroup;
            set
            {
                if (!Set(ref _SelectedGroup, value)) return;
                _SelectedGroupStudents.Source = value?.Students;
                OnPropertyChanged(nameof(SelectedGroupStudents));
            }

        }
        #endregion


        #region SelectedGroupStudents : ICollectionView  - Свойство, возвращающее объект в виде исходных данных, в лице коллекции.
        /// <summary>
        /// Свойство, возвращающее объект - представление,
        /// Экземпляр класса, реализующего интерфейс IView.
        /// </summary>
        private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();
        public ICollectionView? SelectedGroupStudents => _SelectedGroupStudents?.View;
        #endregion
        
        #region OnStudentsFiltred : void - Метод, подключемый к событию Filter, у экземпляра класса CollectionViewSource(ICollectionView)
        /// <summary>
        /// Метод, соответствующий сигнатуре делегата FilterEventHandler.
        /// </summary>
        /// <param name="obj">Объект, от лица которого вызвано было событие </param>
        /// <param name="e">Экземпляр класса FilterEventArgs, содержит информацию о событии</param>
        private void OnStudentsFiltred(object? obj, FilterEventArgs e)
        {
            if (!(e.Item is Student student))
            {
                e.Accepted = false;
                return;
            }

            string filterText = StudentFilterText;
            if (string.IsNullOrEmpty(filterText))
                return;

            else if (student.Name is null || student.Surname == null ||
                     student.Patronymic is null)
            {
                e.Accepted = false;
                return;
            }

            else if (student.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;
            else if (student.Surname.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;
            else if (student.Patronymic.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;
            //else if (filterText.Contains(student.Birthday.Year.ToString())) return;

            e.Accepted = false;
        }
        #endregion

        #region StudentFilterText : string - Свойство, возвращающее значение из строки для фильтрации.
        /// <summary>
        /// Свойство, возвращающее значение из строки для фильтрации отображаемых DataGridItem's из DataGrid,
        /// в представлении главного окна.
        /// </summary>
        private string _StudentFilterText;
        public string StudentFilterText
        {
            get => _StudentFilterText;
            set
            {
                if (!Set(ref _StudentFilterText, value)) return;
                _SelectedGroupStudents.View.Refresh();
            }

        }
        #endregion

        #region Students : IEnumerable<Student> - Перечисление, возвращающее экземпляры класса Students
        public IEnumerable<Student> Students => Enumerable.Range(0, App.IsDesignMode ? 10 : 10_000)
           .Select<int, Student>(number => new Student()
           {
               Name = $"Имя {number}",
               Birthday = DateTime.Now,
               Surname = $"Фамилия {number}",
               Rating = 0,
           });
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

        #region SelectedTabIndex : int - Выбранная вкладка, у объекта TabControl
        private int _SelectedTabIndex = 1;
        /// <summary>
        /// Номер текущей вкладки, выбранной у объекта TabControl в представлении.
        /// </summary>
        public int SelectedTabIndex
        {
            get => _SelectedTabIndex;
            set => Set(ref _SelectedTabIndex, value);
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
        
        #region CreateNewGroup
        private ICommand _CreateNewGroupCommand;
        public ICommand CreateNewGroupCommand => _CreateNewGroupCommand ??= new LambdaCommand(OnCreateNewGroupCommandExecute, CanCreateNewGroupCommandExecuted);
        private bool CanCreateNewGroupCommandExecuted(object parameter) => true;
        private void OnCreateNewGroupCommandExecute(object parameter)
        {
          int group_max_index = Groups.Count() + 1;
          var new_group = new Group()
          {
                Name = $"Группа {group_max_index}",
                Students = new ObservableCollection<Student>()
          };

          Groups.Add(new_group);
        }
        #endregion

        #region DeleteGroupCommand
        private ICommand _DeleteGroupCommand;
        public ICommand DeleteGroupCommand => _DeleteGroupCommand ??= new LambdaCommand(OnDeleteGroupCommandExecute, CanDeleteGroupCommandExecuted);
        private bool CanDeleteGroupCommandExecuted(object parameter) => 
            parameter is Group group && Groups.Contains(group);
        private void OnDeleteGroupCommandExecute(object parameter)
        {
            if (!(parameter is Group group)) return;
            int index = Groups.IndexOf(group);
            Groups.Remove(group);

            if (index < Groups.Count)
                SelectedGroup = Groups[index];
            
        }
        #endregion
        #endregion

        /*------------------------------------------------------------------------------------------------------------------------------- */

    }
}
