using HotChocolate;
using HotChocolate.Types;
using TodoApi.Models;

public class TodoItemType : ObjectType<TodoItemModel>
{
    protected override void Configure(IObjectTypeDescriptor<TodoItemModel> descriptor)
    {
        descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.Name).Type<StringType>();
        descriptor.Field(t => t.IsComplete).Type<BooleanType>();
    }
}