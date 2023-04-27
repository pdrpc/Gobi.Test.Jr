using Gobi.Test.Jr.Application;
using Gobi.Test.Jr.Domain;
using Gobi.Tests.Jr.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gobi.Tests.Jr.WebApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoItemService _todoItemService;

        public TodoController(TodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        public IActionResult Index()
        {
            var items = _todoItemService.GetAll();

            var viewItems = new TodoViewModel { TodoItems = items };

            return View(viewItems);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var item = _todoItemService.GetById(id);

            if(item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(TodoViewModel todoViewModel)
        {
            _todoItemService.Add(todoViewModel.NewTodoItem);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
			var existingItem = _todoItemService.GetById(id);

			if (existingItem == null)
			{
				return NotFound();
			}

			var viewModel = new TodoViewModel
			{
				NewTodoItem = existingItem
			};

			return View(viewModel);
		}

        [HttpPost]
        public IActionResult Update(TodoViewModel todoViewModel)
        {
            var existingItem = _todoItemService.GetById(todoViewModel.NewTodoItem.Id);

            if(existingItem == null)
            {
                return NotFound();
            }

            existingItem.Description = todoViewModel.NewTodoItem.Description;
            existingItem.Completed = todoViewModel.NewTodoItem.Completed;

            _todoItemService.Update(existingItem);

            return RedirectToAction("Index");
        }

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var existingItem = _todoItemService.GetById(id);

			if (existingItem == null)
			{
				return NotFound();
			}

			var viewModel = new TodoViewModel
			{
				NewTodoItem = existingItem
			};

			return View(viewModel);
		}

		[HttpPost]
        public IActionResult Delete(TodoViewModel todoViewModel)
        {
            var item = _todoItemService.GetById(todoViewModel.NewTodoItem.Id);

            if (item == null)
            {
                return NotFound();
            }

            _todoItemService.Delete(todoViewModel.NewTodoItem.Id);

            return RedirectToAction("Index");
        }
    }
}