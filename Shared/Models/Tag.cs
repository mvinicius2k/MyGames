using System.ComponentModel.DataAnnotations;

namespace Shared;

public class Tag
{
    [Key]
    public string Name { get; set; }

    public virtual ICollection<GameTag> GameTags { get; set; }

}

