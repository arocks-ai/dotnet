#  Asp.NET core Web API using REST API and also using GraphQL  
- Asp.NET core 8.0 based Web API
- API provides to store, retrieve and update to-do tasks
- Implements both REST API and GraphQL APIs
- HotChocolate packages are used for GrpahQL
- GraphQL related classes are in GraphQL directory 


### Install Required Packages for HotChocolate.:
```
dotnet add package HotChocolate.AspNetCore
dotnet add package HotChocolate.AspNetCore.Playground
```

### Define GraphQL Types and Schema:
GraphQL/TodoItemType.cs   GraphQL schema definition with method  Configur(), to expose TodoItemModel using GraphQL API. 
It inherits from ObjectType<TodoItemModel>, which is a generic class provided by the HotChocolate library.

####  Model class
TodoItemModel.cs - TodoItemModel class encapsulate the essential data for a to-do item. It is a database context for storing and retrieving to-do items, 
and a GraphQL schema for exposing the data through an API.

#### Query class
GraphQL/Query.cs - Query class is registed as AddQueryType()

```
Query class is a GraphQL API implementation for fetch (or query) the data from the DB
    // Get: api/TodoItems
    // Fetching all items form the database
    public IQueryable<TodoItemModel> GetTodoItems([Service] TodoContext context) => context.TodoItems;

    // Get: api/TodoItems/1
    // Fetching a specific item by its id form the database
    public TodoItemModel GetTodoItem([Service] TodoContext context, int id) => context.TodoItems.FirstOrDefault(t => t.Id == id);
```

#### Mutation class
GraphQL/Mutation.cs - Mutation class for updating the data similar to POST/PUT/DELETE methods


###  Configure GraphQL services 
Configure the GraphQL services and middleware in Program.cs

```
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
```


###  Test API


GraphQL URL - https://localhost:7087/playground/
Swagger URL - https://localhost:7087/swagger/index.html


####  Fetch Data for all the to-do items 
REST API: GET: api/TodoItems

```
Graphy QL Query:
query {
  todoItems {
    id
    name
    isComplete
  }
}
```

####  Fetch Data for single items based on Id
REST API: GET: api/TodoItems/1

```
query {
  todoItem(id: 1) {
    id
    name
    isComplete
  }
}
```

####  Create Data (POST Method)
REST API: POST: api/TodoItems
```
mutation {
  createTodoItem(name: "shower3", isComplete: true) {
    id
    name
    isComplete
  }
}
```

####  Update Data (PUT Method)
REST API: PUT: api/TodoItems/1
```
mutation {
  updateTodoItem(id: 1, name: "shower2", isComplete: false) {
    id
    name
    isComplete
  }
}
```

####  Delete Data (DELETE Method)
REST API: DELETE: api/TodoItems/1
```
mutation {
  deleteTodoItem(id: 1)
}
```
