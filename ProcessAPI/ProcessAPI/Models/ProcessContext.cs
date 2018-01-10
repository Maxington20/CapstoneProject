using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProcessAPI.Models
{
	public class ProcessContext : DbContext
	{
		public ProcessContext(DbContextOptions<ProcessContext> options)
			: base(options)
		{
		}

		public DbSet<Process> Process { get; set; }
		public DbSet<Student> Student { get; set; }
		public DbSet<Exam> Exam { get; set; }
		public DbSet<StudentExam> StudentExam { get; set; }
		public DbSet<Finished> Finished { get; set; }

	}
}
