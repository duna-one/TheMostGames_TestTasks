using System.Text.RegularExpressions;
using System.Windows;

namespace TheMostGames_TestTasks
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Identificators_Box_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (e.Handled = Identificators_Box.Text.Length == 0 && (e.Text == "," || e.Text == ";" || e.Text==" ")) { return; }
            Regex regex = new Regex("[0-9]|[,;]");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
