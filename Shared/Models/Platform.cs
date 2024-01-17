using System.ComponentModel.DataAnnotations;

namespace Shared;

public class Platform
{
    [Key]
    public string Name { get; set; }

    public ICollection<GamePlatform> GamePlatforms { get; set; }

}

