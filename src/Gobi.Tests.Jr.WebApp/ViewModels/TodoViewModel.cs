using Gobi.Test.Jr.Domain;

namespace Gobi.Tests.Jr.WebApp.ViewModels
{
	public class TodoViewModel
	{
		public IEnumerable<TodoItem> TodoItems { get; set; }
		public TodoItem NewTodoItem { get; set; }
	}
}
