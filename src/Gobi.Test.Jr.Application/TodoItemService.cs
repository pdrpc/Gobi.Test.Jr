using Gobi.Test.Jr.Domain;
using Gobi.Test.Jr.Domain.Interfaces;

namespace Gobi.Test.Jr.Application
{
    public class TodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _todoItemRepository.GetAll();
        }

        public TodoItem GetById(int id)
        {
            return _todoItemRepository.GetById(id);
        }

        public void Add(TodoItem todoItem)
        {
            _todoItemRepository.Add(todoItem);
        }

        public void Update(TodoItem todoItem)
        {
            _todoItemRepository.Update(todoItem);
        }

        public void Delete(int id)
        {
            _todoItemRepository.Delete(id);
        }

        
    }
}