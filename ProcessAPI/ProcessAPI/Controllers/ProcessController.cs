using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProcessAPI.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProcessAPI.Controllers
{
	[Route("api/[controller]")]
	public class ProcessController : Controller
	{
		private readonly ProcessContext _context;

		public ProcessController(ProcessContext context)
		{
			_context = context;

			//if (_context.Process.Count() == 0)
			//{
			//	_context.Process.Add(new Process { WindowName = "", ProcessName = "" });
			//	_context.SaveChanges();
			//}
		}
		[HttpGet]
		[Route("Students")]
		public async Task <IActionResult> Students()
		{
			var students = from x in _context.Student
						  .OrderBy(x => x.LastName)
						  .ThenBy(x => x.FirstName)
						  select x;

			return View(await students.ToListAsync());
		}

		[HttpPost]
		[Route("Students")]
		public async Task<IActionResult> Create([FromBody] Student item)
		{
			if (item == null)
			{
				return BadRequest();
			}

			var checkForDuplicate = _context.Student
				.Where(r => r.FirstName == item.FirstName)
				.Where(r=> r.LastName == item.LastName)
				.Where(r => r.StudentNumber == item.StudentNumber)
				.Count();

			if (checkForDuplicate < 1)
			{
				_context.Student.Add(item);
				await _context.SaveChangesAsync();
				return View(item);
			}
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		[Route("Finished")]
		public async Task<IActionResult> Finished()
		{
			var finished = from x in _context.Finished
						  .OrderBy(x => x.LastName)
						  .ThenBy(x => x.FirstName)
						   select x;

			return View(await finished.ToListAsync());
		}

		[HttpPost]
		[Route("Finished")]
		public async Task<IActionResult> Create([FromBody] Finished item)
		{
			if (item == null)
			{
				return BadRequest();
			}

			var checkForDuplicate = _context.Finished
				.Where(r => r.FirstName == item.FirstName)
				.Where(r => r.LastName == item.LastName)
				.Where(r => r.StudentNumber == item.StudentNumber)
				.Count();

			if (checkForDuplicate < 1)
			{
				item.CompletionTime = DateTime.Parse(item.CurrentTime);
				_context.Finished.Add(item);
				await _context.SaveChangesAsync();
				return View(item);
			}
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		[Route("Exams")]
		public async Task<IActionResult> Exams()
		{
			var exams = from x in _context.Exam
						  .OrderBy(x => x.ExamName)
						   select x;

			return View(await exams.ToListAsync());
		}

		[HttpPost]
		[Route("Exams")]
		public async Task<IActionResult> Create([FromBody] Exam item)
		{
			if (item == null)
			{
				return BadRequest();
			}

			var checkForDuplicate = _context.Exam
				.Where(r => r.ExamName == item.ExamName)
				.Count();

			if (checkForDuplicate < 1)
			{
				_context.Exam.Add(item);
				await _context.SaveChangesAsync();
				return View(item);
			}
			return RedirectToAction(nameof(Index));
		}


		//GET: Process
		public async Task<IActionResult> Index
			(string LastName,string FirstName, string ProcessName,string WindowName, string sortOrder)
		{

			ViewBag.FirstNameSortParm = string.IsNullOrEmpty(sortOrder) ? "first_name_desc" : "";
			ViewBag.LastNameSortParm = string.IsNullOrEmpty(sortOrder) ? "last_name_desc" : "";
			ViewBag.StudentNumberSortParm = string.IsNullOrEmpty(sortOrder) ? "student_number_desc" : "";
			ViewBag.WindowNameSortParm = string.IsNullOrEmpty(sortOrder) ? "window_name_desc" : "";
			ViewBag.ProcessNameSortParm = string.IsNullOrEmpty(sortOrder) ? "process_name_desc" : "";

			ViewBag.FirstName = (from p in _context.Process
								  .OrderBy(x => x.FirstName)
									 select p.FirstName)
								  .Distinct();

				ViewBag.LastName = (from p in _context.Process
								  .OrderBy(x => x.LastName)
									select p.LastName)
								  .Distinct();

				ViewBag.WindowName = (from p in _context.Process
									.OrderBy(x => x.WindowName)
									  select p.WindowName)
									.Distinct();

				ViewBag.ProcessName = (from p in _context.Process
								   .OrderBy(x => x.ProcessName)
									  select p.ProcessName)
								   .Distinct();
			
			//var process = _context.Process
			//	.OrderBy(x => x.LastName)
			//	.ThenBy(x => x.FirstName);
			var process = from x in _context.Process
						  .OrderBy(x => x.LastName)
						  .ThenBy(x => x.FirstName)
						  .ThenBy(x => x.ProcessName)
						  //.Where(x => x.LastName == LastName || x.LastName == null || x.LastName == "")
						  //.Where(x => x.ProcessName == ProcessName || x.ProcessName == null || x.ProcessName == "")
						  select x;

			if (!String.IsNullOrEmpty(LastName))
			{
				process = process
					.Where(x => x.LastName == LastName);
			}

			if (!String.IsNullOrEmpty(ProcessName))
			{
				process = process
					.Where(x => x.ProcessName == ProcessName);
			}

			switch (sortOrder)
			{
				case "first_name_desc":
					process = process.OrderBy(p => p.FirstName);
					break;
				case "last_name_desc":
					process = process.OrderBy(p => p.LastName);
					break;
				case "student_number_desc":
					process = process.OrderBy(p => p.StudentNumber);
					break;
				case "window_name_desc":
					process = process.OrderBy(p => p.WindowName);
					break;
				case "process_name_desc":
					process = process.OrderBy(p => p.ProcessName);
					break;
				default:
					process = process.OrderBy(p => p.StudentNumber);
					break;
			}
			//return View(await _context.Process.ToListAsync());
			return View(await process.ToListAsync());
		}

		//[HttpGet]
		//public  IEnumerable<Process> GetAll()
		//{
		//	return _context.Process.ToList();
		//}

		//[HttpGet("{id}", Name = "Test 1")]
		//public IActionResult GetById(long id)
		//{
		//	var item = _context.Process.FirstOrDefault(t => t.ID == id);
		//	if (item == null)
		//	{
		//		return NotFound();
		//	}
		//	return new ObjectResult(item);
		//}

		//[HttpPost]
		//public IActionResult Create([FromBody] Process item)
		//{
		//	if (item == null)
		//	{
		//		return BadRequest();
		//	}

		//	_context.Process.Add(item);
		//	_context.SaveChanges();

		//	return CreatedAtRoute("Test 1", new { id = item.ID }, item);
		//}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Process item)
		{
			if (item == null)
			{
				return BadRequest();
			}

			List<Process> checkForDuplicate = await _context.Process
				.Where(r => r.ProcessID == item.ProcessID)
				.Where(r => r.StudentNumber == item.StudentNumber)
				.Where(r => r.FirstName == item.FirstName)
				.Where(r => r.LastName == item.LastName)
				.Where(r => r.ProcessName == item.ProcessName)
				.Where(r => r.WindowName == item.WindowName)
				.ToListAsync();



			if (checkForDuplicate.Count == 0)
			{
				item.StartTime = DateTime.Parse(item.CurrentTime);
				_context.Process.Add(item);
			}

			//this else statement will always happen after the initial post, which is not what we want.
			//we need to get this to only happen when the user has stopped running one of the programs
			//they ran previously, for us to get the end time. I cannot figure out what the logic would be
			//to allow this to happen. I will have to ask Harry if he shows up to class tomorrow.
			else 
			{

				var updateMe = await _context.Process
				.Where(r => r.ProcessID == item.ProcessID)
				.Where(r => r.StudentNumber == item.StudentNumber)
				.Where(r => r.FirstName == item.FirstName)
				.Where(r => r.LastName == item.LastName)
				.Where(r => r.ProcessName == item.ProcessName)
				.Where(r => r.WindowName == item.WindowName)
				.FirstOrDefaultAsync();
			
				updateMe.EndTime = DateTime.Parse(item.CurrentTime);
				//item.EndTime = DateTime.Parse(item.CurrentTime);
				_context.Process.Update(updateMe);
			}
			await _context.SaveChangesAsync();
			return View(item);
		}

		[HttpPut("{id}")]
		public IActionResult Update(long id, [FromBody] Process item)
		{
			if (item == null || item.ID != id)
			{
				return BadRequest();
			}

			var process = _context.Process.FirstOrDefault(t => t.ID == id);
			if (process == null)
			{
				return NotFound();
			}

			process.WindowName = item.WindowName;

			_context.Process.Update(process);
			_context.SaveChanges();
			return new NoContentResult();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(long id)
		{
			var process = _context.Process.FirstOrDefault(t => t.ID == id);
			if (process == null)
			{
				return NotFound();
			}

			_context.Process.Remove(process);
			_context.SaveChanges();
			return new NoContentResult();
		}
	}
}
