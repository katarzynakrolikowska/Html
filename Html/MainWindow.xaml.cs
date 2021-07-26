using Microsoft.Win32;
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

namespace Html
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string EXTENSION = ".html";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void saveFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.DefaultExt = ".html";
            openFileDialog.Filter = "Html documents (.html)|*.html";

            bool? result = openFileDialog.ShowDialog();
           
            if (result == true)
            {
                string extension = System.IO.Path.GetExtension(openFileDialog.FileName);

                if (extension == EXTENSION)
                {
                    var content = System.IO.File.ReadAllText(openFileDialog.FileName);

                    var list = HtmlTagHelper.GetTagContentList(content);

                    itemsListView.ItemsSource = list;
                }
                else
                {
                    MessageBox.Show("Podaj plik HTML", "Uwaga", MessageBoxButton.OK);
                }
            }
        }
    }
}
