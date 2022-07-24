using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoBackend.Models;

namespace TodoBackend.Services
{
    public class TodoService
    {
        private readonly  IMongoCollection<Todo> _todoDatabase;

        public TodoService(IOptions<TodoDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);

            _todoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<Todo>(options.Value.CollectionName);
        }

        public async Task<List<Todo>> Get()
        {
            return await _todoDatabase.Find(_ => true).ToListAsync();
        }

        public async Task<Todo> Get(string id)
        {
            var todo = await _todoDatabase.Find(todo => todo.Id == id).FirstOrDefaultAsync();
            return todo;
        }


        public async Task Create(Todo newTodo)
        {
            await _todoDatabase.InsertOneAsync(newTodo);
        }

        public async Task Update(string id, Todo updatedTodo)
        {
            updatedTodo.Id = id;
            await _todoDatabase.ReplaceOneAsync(todo => todo.Id == id, updatedTodo);
        }

        public async Task Remove(string id)
        {
            await _todoDatabase.DeleteOneAsync(todo => todo.Id == id);
        }

    }
}
