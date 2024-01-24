using System.ComponentModel.DataAnnotations;

namespace Shared;

public class Platform
{
    public const int NameMaxLength = 20;

    [Key, MaxLength(NameMaxLength)]
    public string Name { get; set; }

    public ICollection<GamePlatform> GamePlatforms { get; set; }

}

