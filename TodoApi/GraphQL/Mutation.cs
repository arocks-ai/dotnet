using HotChocolate;
using HotChocolate.Types;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class Mutation
{
    private readonly ILogger<Mutation> _logger;
    public Mutation(ILogger<Mutation> logger)
    {
        _logger = logger;
    }


    // REST API: PUT: api/TodoItems/1
    public async Task<TodoItemModel> UpdateTodoItem(
        long id,
        string name,
        bool isComplete,
        [Service] TodoContext context)
    {
        try
        {
            _logger.LogInformation("Updating TodoItem with id: {Id}", id);

            var todoItem = await context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                _logger.LogWarning("TodoItem with id: {Id} not found", id);
                throw new GraphQLException(new Error("Todo item not found", "TODO_ITEM_NOT_FOUND"));
            }

            todoItem.Name = name;
            todoItem.IsComplete = isComplete;

            context.TodoItems.Update(todoItem);
            await context.SaveChangesAsync();
            _logger.LogInformation("Updated TodoItem with id: {Id}", id);

            return todoItem;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating TodoItem with id: {Id}", id);
            throw new GraphQLException(new Error("An error occurred while updating the Todo item", "UPDATE_ERROR"));
        }
    }


    // REST API: POST: api/TodoItems
    public async Task<TodoItemModel> CreateTodoItem(
        string name,
        bool isComplete,
        [Service] TodoContext context)
    {
        var todoItem = new TodoItemModel
        {
            Name = name,
            IsComplete = isComplete
        };

        context.TodoItems.Add(todoItem);
        await context.SaveChangesAsync();

        return todoItem;
    } 

    // REST API: DELETE: api/TodoItems/1
    public async Task<bool> DeleteTodoItem(
        long id,
        [Service] TodoContext context)
    {
        try
        {
            _logger.LogInformation("Deleting TodoItem with id: {Id}", id);

            var todoItem = await context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                _logger.LogWarning("TodoItem with id: {Id} not found", id);
                throw new GraphQLException(new Error("Todo item not found", "TODO_ITEM_NOT_FOUND"));
            }

            context.TodoItems.Remove(todoItem);
            await context.SaveChangesAsync();
            _logger.LogInformation("Deleted TodoItem with id: {Id}", id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting TodoItem with id: {Id}", id);
            throw new GraphQLException(new Error("An error occurred while deleting the Todo item", "DELETE_ERROR"));
        }
    }

       
}