using System.Windows;

namespace CV19
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        //private void CollectionViewSource_Filter(object sender, System.Windows.Data.FilterEventArgs e)
        //{
        //    if (!(e.Item is Models.Decanat.Group group)) return;
        //    else if (group.Name is null) return;
        

        //    string filterText = GroupNameFilterText.Text;
        //    if (filterText.Length == 0) return;

        //    if (group.Name.Contains(filterText, System.StringComparison.OrdinalIgnoreCase)) return;
        //    else if (group.Description != null &&
        //        group.Description.Contains(filterText, System.StringComparison.OrdinalIgnoreCase)) return;

        //    e.Accepted = false;
        //}

        //private void GroupNameFilterText_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        //{
        //    var textBox = (System.Windows.Controls.TextBox)sender;
        //    var collection = (System.Windows.Data.CollectionViewSource)textBox.FindResource("GroupsCollection");
        //    collection.View.Refresh();
        //}
    }
}
