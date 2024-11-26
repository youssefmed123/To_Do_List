using Microsoft.AspNetCore.Mvc;
using To_Do_List.Data;
using To_Do_List.Models;

namespace To_Do_List.Controllers
{
	public class ToDoController : Controller
	{
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public IActionResult Index()
        {
            var todos = _dbContext.ToDos.ToList();
            return View(todos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ToDo toDo)
        {
            _dbContext.ToDos.Add(toDo);
            _dbContext.SaveChanges();
            TempData["success"] = "To-Do created successfully!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var toDo = _dbContext.ToDos.Find(id);
            if (toDo == null) return RedirectToAction("NotFoundPage", "Home");
            return View(toDo);
        }

        [HttpPost]
        public IActionResult Edit(ToDo toDo)
        {
            _dbContext.ToDos.Update(toDo);
            _dbContext.SaveChanges();
            TempData["success"] = "To-Do updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var toDo = _dbContext.ToDos.Find(id);
            if (toDo == null) return RedirectToAction("NotFoundPage", "Home");

            _dbContext.ToDos.Remove(toDo);
            _dbContext.SaveChanges();
            TempData["success"] = "To-Do deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
