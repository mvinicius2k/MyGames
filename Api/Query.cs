using HotChocolate;
using Shared;

namespace Api;

public class Query
{
    public Tag GetTag(string id, [Service] Context context) => context.Tags.Find(id);
    public List<Tag> GetTags([Service] Context context) => context.Tags.ToList();
}
