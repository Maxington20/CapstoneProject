using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessAPI.Models
{
	public class Process
	{
		[Key]
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string StudentNumber { get; set; }
		public string ExamNumber { get; set; }
		public string ExamName { get; set; }
		public string WindowName { get; set; }
		public string ProcessName { get; set; }
		public string ProcessID { get; set; }

		[DisplayFormat(DataFormatString = "{0:h:mm:ss tt}")]
		public DateTime StartTime { get; set; }

		[DisplayFormat(DataFormatString = "{0:h:mm:ss tt}")]
		public DateTime EndTime { get; set; }

		[NotMapped]
		public string CurrentTime { get; set; }
	}

	public class Finished
	{
		public Finished()
		{
			StudentExam = new HashSet<StudentExam>();
		}

		[Key]
		public int FinishedID { get; set; }
		public string StudentNumber { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		[DisplayFormat(DataFormatString = "{0:h:mm:ss tt}")]
		public DateTime CompletionTime { get; set; }
		[NotMapped]
		public string CurrentTime { get; set; }

		public ICollection<StudentExam> StudentExam { get; set; }
	}

	public class Student
	{
		public Student()
		{
			StudentExam = new HashSet<StudentExam>();
		}

		[Key]
		public int StudentID { get; set; }
		public string StudentNumber { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public ICollection<StudentExam> StudentExam { get; set; }
	}

	public class Exam
	{
		public Exam()
		{
			StudentExam = new HashSet<StudentExam>();
		}

		[Key]
		public int ExamID { get; set; }
		public string ExamName { get; set; }

		public ICollection<StudentExam> StudentExam { get; set; }
	}

	public class StudentExam
	{
		public StudentExam()
		{
			TimeFrame = new HashSet<TimeFrame>();
		}

		[Key]
		public int StudentExamID { get; set; }
		public string ProcessID { get; set; }
		public string BaseURL { get; set; }
		public string ProcessName { get; set; }

		public ICollection<TimeFrame> TimeFrame { get; set; }
	}

	public class TimeFrame
	{
		public TimeFrame()
		{
			StartTime = DateTime.Now;
			EndTime = DateTime.Now;
		}

		[Key]
		public int TimeFrameID { get; set; }
		public string FullURL { get; set; }
		public string TabTitle { get; set; }
		public string WindowName { get; set; }

		[DisplayFormat(DataFormatString = "{0:h:mm:ss tt}")]
		public DateTime StartTime { get; set; }

		[DisplayFormat(DataFormatString = "{0:h:mm:ss tt}")]
		public DateTime EndTime { get; set; }
	}

	
}
