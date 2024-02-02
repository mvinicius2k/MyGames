using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Tag = Shared.Tag;

namespace Api;

public class Query
{
    // public Tag GetTag(string id, [Service] Context context) => context.Tags.Find(id);
    // [UsePaging, UseFiltering, UseSorting]
    // public IQueryable<Tag> GetTags([Service] Context context) => context.Tags.AsQueryable();

    public Book GetBook() =>
       new Book
       {
           Title = "C# in depth.",
           Author = new Author
           {
               Name = "Jon Skeet"
           }
       };

    public Author GetAuthor() =>
        new Author
        {
            Name = "Jon Skeet"
        };

}

//Rascunho
public class Book
{
    public string Title { get; set; }

    public Author Author { get; set; }
}

public class Author
{
    public string Name { get; set; }
}