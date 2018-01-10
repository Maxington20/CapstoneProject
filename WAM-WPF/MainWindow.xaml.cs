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
using System.Diagnostics;
using System.ComponentModel;
using System.Timers;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net;
using System.Collections.ObjectModel;
using System.Threading;

namespace WAM
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	///
	public partial class MainWindow : Window
	{

		private VM vm = new VM();
		private static Mutex mut = new Mutex();

		UserCredentials UserCredentials = new UserCredentials();
		public MainWindow()
		{
			InitializeComponent();
			WebServer ws = new WebServer(SendResponse,"http://localhost:8080/tabs/");
			DataContext = vm;
			ws.Run();
		}

		public static string SendResponse(HttpListenerRequest request)
		{
			//return string.Format("<HTML><BODY>My web page.<br>{0}</BODY></HTML>", DateTime.Now);
			return ("I hate Mondays");
		}

		public void LoadProcesses()
		{
			mut.WaitOne();
			//clears out the list
			List<ProcessItem> processItems = new List<ProcessItem>();

			//create an array of the running processes
			Process[] runningProcesses = Process.GetProcesses();
			Process myProcess = new Process();
		

			//sort the array in alphabetical order
			Array.Sort(runningProcesses, delegate (Process p1, Process p2)
			 {
				 return p1.MainWindowTitle.CompareTo(p2.MainWindowTitle);
			 });
			
			//List the names of all of the processes
			foreach (Process process in runningProcesses)
			{
				//determine if the process actually has an active window.
				//should omit processes running in the background
				if (process.MainWindowTitle != "")
				{
					processItems.Add(new ProcessItem
					{
						FirstName = UserCredentials.FirstName,
						LastName = UserCredentials.LastName,
						StudentNumber = UserCredentials.StudentNumber,
						ExamNumber = UserCredentials.ExamNumber,
						ExamName = UserCredentials.ExamName,
						//adds the window title to the Window Name column
						WindowName = process.MainWindowTitle,
						//adds the process name to the Process Name column
						ProcessName = process.ProcessName,
						ProcessID = Convert.ToString(process.Id),
						//adds a timestamp to the Time Stamp column
						CurrentTime = Convert.ToString(DateTime.Now)
					});
				}
			}
			vm.Items = new ObservableCollection<ProcessItem>(processItems);
			mut.ReleaseMutex();
		}		
		public async void SendJson()
		{
			//converts the listview items into a Json string
			//var serializer = new JavaScriptSerializer();
			//var serializedResult = serializer.Serialize(lvwProcessList.Items);

			using (var client = new HttpClient())
			{
				//Shows the target Url for the Json String
				//client.BaseAddress = new Uri("http://wamprocessapi.azurewebsites.net");
				client.BaseAddress = new Uri("http://localhost:53627");
				//var content = new StringContent(serializedResult, UnicodeEncoding.UTF8,
				//	"application/json");
				mut.WaitOne();
				foreach (ProcessItem process in vm.Items)
				{
					await client.PostAsJsonAsync("/api/process", process);
					await client.PostAsJsonAsync("/api/process/students", process);
					await client.PostAsJsonAsync("/api/process/exams", process);
				}
				mut.ReleaseMutex();
			}
		}

		//method to finish the exam and send the last batch of data
		public async void FinishExam()
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:53627");
				mut.WaitOne();
				foreach (ProcessItem process in vm.Items)
				{
					await client.PostAsJsonAsync("/api/process/finished", process);
				}
				mut.ReleaseMutex();
			}
		}

		//method to start the timer and cause the process list to refresh every 5 seconds
		private void RefreshProcesses()
		{
			System.Windows.Forms.Timer processTimer = new System.Windows.Forms.Timer();
			processTimer.Interval = 15000;
			processTimer.Tick += new EventHandler(Timer1_Tick);
			processTimer.Start();
		}
		//run the LoadProcesses method with each tick (every 5 seconds)
		private void Timer1_Tick(object sender, EventArgs e)
		{
			LoadProcesses();
			SendJson();
		}

		//when the WAM app loads, it will automatically minimize, and the refresh processes method will run
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			UserCredentials.ShowDialog();
			LoadProcesses();
		}

		private void btnConnect_Click(object sender, RoutedEventArgs e)
		{
			if (System.Windows.MessageBox.Show("Are you sure you want to join an exam?", 
					"", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
			{
				//Nothing will happen when No is selected
			}
			else
			{
				btnFinishExam.IsEnabled = true;
				btnConnect.IsEnabled = false;
				System.Windows.MessageBox.Show("This application will minimize and run in the " +
					"background throughout your exam");
				WindowState = WindowState.Minimized;
				RefreshProcesses();
			}
			
		}
		//button to leave the current exam
		private void btnFinishExam_Click(object sender, RoutedEventArgs e)
		{
			if (System.Windows.MessageBox.Show("Are you sure you want to leave your exam?", 
					"Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
			{
				//code to run if no is selected
			}
			else
			{
				//code to run if yes is selected
				SendJson();
				FinishExam();
				System.Windows.MessageBox.Show("You have sucessfully left your exam");
				this.Close();
			}
		}
		private void btnCloseProcesses_Click(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
