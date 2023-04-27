using Gobi.Test.Jr.Domain;
using Gobi.Test.Jr.Domain.Interfaces;
using System.Data.SQLite;

namespace Gobi.Test.Jr.Infra
{
	public class TodoItemRepository : ITodoItemRepository
	{
		public TodoItemRepository()
		{
			CreateDatabase();
			CreateTable();
		}

		private static SQLiteCommand CreateCommand()
		{
			var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gobi.sqlite");
			var connectionString = $"Data Source={filePath}; Version=3;";
			var connection = new SQLiteConnection(connectionString);

			return new SQLiteCommand(connection);
		}

		private void CreateDatabase()
		{
			var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gobi.sqlite");

			if (File.Exists(filePath) is false)
			{
				SQLiteConnection.CreateFile(filePath);
			}			
		}

		private void CreateTable()
		{
			var command = CreateCommand();

			command.CommandText = """
                CREATE TABLE IF NOT EXISTS "TodoItem" 
                (
                    "Id" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                    "Description" TEXT NOT NULL,
                    "Completed" integer NOT NULL
                );
                """;

			command.Connection.Open();
			command.ExecuteNonQuery();
			command.Connection.Close();
		}

	
		public IEnumerable<TodoItem> GetAll()
		{
			var command = CreateCommand();

			command.CommandText = "SELECT * FROM TodoItem";

			command.Connection.Open( );
			var reader = command.ExecuteReader();

			var todoItems = new List<TodoItem>();

			while (reader.Read() )
			{
				var todoItem = new TodoItem
				{
					Id = reader.GetInt32(0),
					Description = reader.GetString(1),
					Completed = reader.GetBoolean(2)
				};

				todoItems.Add(todoItem);
			}

			command.Connection.Clone();
			return todoItems;
		}

		public TodoItem GetById(int id)
		{
			var command = CreateCommand();
			command.CommandText = "SELECT * FROM TodoItem WHERE Id = @id";
			command.Parameters.AddWithValue("@id", id);

			command.Connection.Open();
			var reader = command.ExecuteReader();
			var todoItem = default(TodoItem);

			if(reader.Read())
			{
				todoItem = new TodoItem
				{
					Id = reader.GetInt32(0),
					Description = reader.GetString(1),
					Completed = reader.GetBoolean(2)
				};
			}

			reader.Close();
			command.Connection.Close();

			return todoItem;
		}

		public void Add (TodoItem todoItem)
		{
			var command = CreateCommand();
			command.CommandText = "INSERT INTO TodoItem (Description, Completed) VALUES(@description, @completed)";
			command.Parameters.AddWithValue("@description", todoItem.Description);
			command.Parameters.AddWithValue("@completed", todoItem.Completed);

			command.Connection.Open();
			command.ExecuteNonQuery();
			command.Connection.Close();
		}

		public void Update(TodoItem todoItem)
		{
			var command = CreateCommand();

			command.CommandText = "UPDATE TodoItem SET Description = @description, Completed = @completed WHERE Id = @id";
			command.Parameters.AddWithValue("@description", todoItem.Description);
			command.Parameters.AddWithValue("@completed", todoItem.Completed);
			command.Parameters.AddWithValue("@id", todoItem.Id);

			command.Connection.Open();
			command.ExecuteNonQuery();
			command.Connection.Close();
		}

		public void Delete (int id)
		{
			var command = CreateCommand();

			command.CommandText = "DELETE FROM TodoItem WHERE Id = @id";
			command.Parameters.AddWithValue("@id", id);

			command.Connection.Open();
			command.ExecuteNonQuery ();
			command.Connection.Close();
		}
	}
}
