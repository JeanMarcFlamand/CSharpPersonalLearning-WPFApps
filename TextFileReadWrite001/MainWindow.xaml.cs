using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using TextFileChallenge;

namespace TextFileReadWrite001
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

        private void BrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string myFile;
                string myPath;

                //  myFile = openFileDialog.SafeFileName; //Only the file name
                myFile = openFileDialog.FileName; //Only the file name
                myPath = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
                //myPath = myPath.Replace(@"\\", @"\");
                //MessageBox.Show (myPath);

                readtextfile(myFile);
            }
        }

        public void readtextfile(string thepathfile)
        {
            int FirstNameIndex;
            int LastNameIndex;
            int AgeIndex;
            int IsAliveIndex;

            List<UserModel> users = new List<UserModel>();

            var reader = new StreamReader(thepathfile);
            List<string> headers = new List<string>();
            headers = reader.ReadLine().Split(',').ToList(); //Read the first line (header line) and put in a list

            // Get The Index of each header in case of not in correct order
            FirstNameIndex = headers.FindIndex(headers => headers.StartsWith("FirstName"));
            LastNameIndex = headers.FindIndex(headers => headers.StartsWith("LastName"));
            AgeIndex = headers.FindIndex(headers => headers.StartsWith("Age"));
            IsAliveIndex = headers.FindIndex(headers => headers.StartsWith("IsAlive"));


            while (!reader.EndOfStream)
            {
                var values = reader.ReadLine().Split(',');
                UserModel newUser = new UserModel();

                newUser.FirstName = values[FirstNameIndex];
                newUser.LastName = values[LastNameIndex];
                newUser.Age = int.Parse(values[AgeIndex]);
                newUser.IsAlive = bool.Parse(values[IsAliveIndex]);
                // if 0 or 1 use newUser.IsAlive = Convert.ToBoolean(Convert.ToInt32(values[3]));
                MessageBox.Show(newUser.DisplayText);
                //MessageBox.Show(string.Format("{0} = {1};", headers[i], values[i]));
                FileLstB.Items.Add(newUser.DisplayText);
            }

        }
    }
}
