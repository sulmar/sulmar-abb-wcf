using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TasksConsoleClient;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Sender sender1 = new Sender();

            this.Result.Content = await sender1.PrintAsync("Hello", 5);

            //int result = sender1.PrintAsync("Hello", 5).MyWait();

            //this.Result.Content = result;
        }

        private void Test()
        {
        // Current Thread
        // Dispatcher.CurrentDispatcher.InvokeAsync(delegate)

        // Main UI (user)
        // Application.Current.Dispatcher.InvokeAsync()


            // https://stackoverflow.com/a/32298493/1180808
        }
    }
}
