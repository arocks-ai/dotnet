using HotChocolate;
using HotChocolate.Types;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class Query
{
    public IQueryable<TodoItemModel> GetTodoItems([Service] TodoContext context) =>
        context.TodoItems;

    public TodoItemModel GetTodoItem([Service] TodoContext context, long id) =>
        context.TodoItems.FirstOrDefault(t => t.Id == id);
}