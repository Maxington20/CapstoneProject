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
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;

namespace WAM
{
	/// <summary>
	/// Interaction logic for UserCredentials.xaml
	/// </summary>
	public partial class UserCredentials : Window
	{
        const int FIRSTNAME_INDEX = 0;
        const int LASTNAME_INDEX = 1;
        const int STUDENTNUMBER_INDEX = 2;

		public UserCredentials()
		{
			InitializeComponent();
            string[] creds = LoadCredentials();
            txtFirstName.Text = creds[FIRSTNAME_INDEX];
            txtLastName.Text = creds[LASTNAME_INDEX];
            txtStudentNumber.Text = creds[STUDENTNUMBER_INDEX];
		}

		private void NumberValidationTextbox(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void TextValidationTextbox(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^a-zA-z]");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void btnOk_Click(object sender, RoutedEventArgs e)
		{
            if (FirstName != "" && LastName != "" && StudentNumber != "" 
				&& ExamNumber != "" && ExamName != "")
			{
                SaveCredentials();
				this.Close();
			}
			else
			{
				MessageBox.Show("You must fill out all boxes");
			}
		}

        private string getPath()
        {
            string path = string.Empty;
            string appdataPath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            string directoryPath = appdataPath + "\\WAM";
            if (Directory.Exists(appdataPath))
            {
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                path = directoryPath;
            }

            return path;
        }
        const string CREDENTIAL_FILENAME = "Credentials.txt";
        private void SaveCredentials()
        {
            string path = getPath();

            if (path.Length > 0)
                File.WriteAllText(Path.Combine(path, CREDENTIAL_FILENAME), FirstName + "\n" + LastName + "\n" + StudentNumber);
        }
        private string[] LoadCredentials()
        {
            string[] creds = new string[3];
            string path = getPath();

            if (path.Length > 0)
            {
                string filepath = Path.Combine(path, CREDENTIAL_FILENAME);
                if (File.Exists(filepath))
                    creds = File.ReadAllLines(filepath);
            }

            return creds;
        }

		public string FirstName
		{
			get
			{
				if (txtFirstName.Text == null)
				{
					return string.Empty;
				}
				return txtFirstName.Text;
			}
		}

		public string LastName
		{
			get
			{
				if (txtLastName.Text == null)
				{
					return string.Empty;
				}
				return txtLastName.Text;
			}
		}

		public string StudentNumber
		{
			get
			{
                if (txtStudentNumber.Text == null)
                {
                    return string.Empty;
                }
                return txtStudentNumber.Text;
            }
        }

		public string ExamNumber
		{
			get
			{
                if (txtExamNumber.Text == null)
                {
                    return string.Empty;
                }
                return txtExamNumber.Text;
            }
        }

		public string ExamName
		{
			get
			{
				if (txtExamName.Text == null)
				{
					return string.Empty;
				}
				return txtExamName.Text;
			}
		}
	}
}
