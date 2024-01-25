using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Tag = Shared.Tag;

namespace Api;

public class Query
{
    public Tag GetTag(string id, [Service] Context context) => context.Tags.Find(id);
    [UsePaging, UseFiltering, UseSorting]
    public IQueryable<Tag> GetTags([Service] Context context) => context.Tags.AsQueryable();


}
