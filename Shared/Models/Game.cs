using System.ComponentModel.DataAnnotations;

namespace Shared;

public class Game
{
    public const int NameMaxLength = 50;
    public const int DescriptionMaxLength = 1000;

    public int Id { get; set; }
    [MaxLength(NameMaxLength)]
    public string Name { get; set; }
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; }
    
    public virtual ICollection<GamePlatform> GamePlatforms { get; set; }
    public virtual ICollection<GameTag>  GameTags { get; set; }
}

