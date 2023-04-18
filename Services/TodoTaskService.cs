using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyApi.Models;

namespace MyApi.Services;

public class TodoTaskService
{
    private readonly IMongoCollection<TodoTask> _taskCollection;

    public TodoTaskService(IOptions<TodoDatabaseSettings> todoDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            todoDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            todoDatabaseSettings.Value.DatabaseName);

        _taskCollection = mongoDatabase.GetCollection<TodoTask>(
            todoDatabaseSettings.Value.TodoCollectionName);
    }

    public async Task<List<TodoTask>> GetAsync() =>
        await _taskCollection.Find(_ => true).ToListAsync();

    public async Task<TodoTask?> GetAsync(string id) =>
        await _taskCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TodoTask newBook) =>
        await _taskCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, TodoTask updatedBook) =>
        await _taskCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _taskCollection.DeleteOneAsync(x => x.Id == id);
}