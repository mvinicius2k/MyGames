using System.ComponentModel.DataAnnotations;

namespace Shared;

public class Tag
{
    public const int NameMaxLength = 20;
    
    [Key, MaxLength(NameMaxLength)]
    public string Name { get; set; }

    public virtual ICollection<GameTag> GameTags { get; set; }

}

public record TagDto
{
    public string Name { get; init; }

    public static explicit operator Tag(TagDto dto)
    {
        return new Tag { Name = dto.Name };
    }
}
