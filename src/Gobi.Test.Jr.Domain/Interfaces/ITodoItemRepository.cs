namespace Gobi.Test.Jr.Domain.Interfaces
{
    public interface ITodoItemRepository
    {
        IEnumerable<TodoItem> GetAll();
        TodoItem GetById(int id);
        void Add(TodoItem todoItem);
        void Update(TodoItem todoItem);
        void Delete(int id);
    }
}
