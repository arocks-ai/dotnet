# Integrate GraphQL using HotChocolate into ASP.NET Core project

### 1.Install Required Packages for HotChocolate.:
dotnet add package HotChocolate.AspNetCore
dotnet add package HotChocolate.AspNetCore.Playground

### 2.Define GraphQL Types and Schema:
GraphQL/TodoItemType.cs   GraphQL schema definition with method  Configur(), to expose TodoItemModel using GraphQL API. 
It inherits from ObjectType<TodoItemModel>, which is a generic class provided by the HotChocolate library.

#### Data model
TodoItemModel.cs - TodoItemModel class encapsulate the essential data for a to-do item. It is a database context for storing and retrieving to-do items, 
and a GraphQL schema for exposing the data through an API.

#### Fetch the data from the DB
Query class is registed as AddQueryType()

GraphQL/Query.cs: Query class is a GraphQL API implementation for fetch (or query) the data from the DB
    // Get: api/TodoItems
    // Fetching all items form the database
    public IQueryable<TodoItemModel> GetTodoItems([Service] TodoContext context) => context.TodoItems;

    // Get: api/TodoItems/1
    // Fetching a specific item by its id form the database
    public TodoItemModel GetTodoItem([Service] TodoContext context, int id) => context.TodoItems.FirstOrDefault(t => t.Id == id);

#### Fetch the data from the DB
Mutation class for POST requests


###  Configure GraphQL services 
Configure the GraphQL services and middleware in Program.cs
// Add GraphQL services
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()    // For queries
    .AddMutationType<Mutation>()   // For post methods
    .AddType<TodoItemType>();
    
    
// Configure GraphQL endpoint and Playground
app.MapGraphQL();
app.UsePlayground(new PlaygroundOptions
{
    QueryPath = "/graphql",
    Path = "/playground"
});



###  Test API
####  Fetch Data
Fetch all the to-do items 
GraphQL URL - https://localhost:7087/playground/
Swagger URL - https://localhost:7087/swagger/index.html

https://localhost:7087/playground/../graphql
Get REST API: api/TodoItems

Graphy QL Query:
query {
  todoItems {
    id
    name
    isComplete
  }
}

Fetch the to-do item specified by Id 
Get REST API: api/TodoItems/1

query {
  todoItem(id: 1) {
    id
    name
    isComplete
  }
}


####  Create Data (POST Method

####  Update Data (PUT Method)

####  Delete Data (DELETE Method)
