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
        public const char OPEN_TAG = '<';

        public const char CLOSE_TAG = '>';

        public const string COMMENT_OPEN = "<!---";

        public const string COMMENT_CLOSE = "-->";

        public const string EXTENSION = ".html";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void saveFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();


            openFileDialog.DefaultExt = ".html";
            openFileDialog.Filter = "Html documents (.html)|*.html";

            bool? result = openFileDialog.ShowDialog();
           
            if (result == true)
            {
                string extension = System.IO.Path.GetExtension(openFileDialog.FileName);
                if (extension == EXTENSION)
                {
                    var content = System.IO.File.ReadAllText(openFileDialog.FileName);
                    var list = new List<string>();

                    SetTextContent(content, list);

                    itemsListView.ItemsSource = list;
                }
                else
                {
                    MessageBox.Show("Podaj plik HTML", "Uwaga", MessageBoxButton.OK);
                }
                
            }
        }

        private void SetTextContent(string content, List<string> list)
        {
            var contentToCheck = content.Trim().Replace(COMMENT_OPEN, "").Replace(COMMENT_CLOSE, "");

            if (!contentToCheck.Any(c => c == OPEN_TAG) && !contentToCheck.Any(c => c == CLOSE_TAG))
            {
                list.Add(contentToCheck);
            }
            else if (contentToCheck.Any())
            {
                if (contentToCheck.First() != OPEN_TAG)
                {
                    var text = GetString(content.TakeWhile(c => c != OPEN_TAG));
                    list.Add(text);

                    var newContent = GetString(content.SkipWhile(c => c != OPEN_TAG));
                    SetTextContent(newContent, list);
                }
                else
                {
                    var newContent = GetString(contentToCheck.SkipWhile(c => c != CLOSE_TAG).Skip(1));
                    SetTextContent(newContent, list);
                }
            }
        }

        private string GetString(IEnumerable<char> charList)
        {
            return new string(charList.ToArray());
        }
    }
}
