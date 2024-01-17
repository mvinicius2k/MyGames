namespace Shared;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public virtual ICollection<GamePlatform> GamePlatforms { get; set; }
    public virtual ICollection<GameTag>  GameTags { get; set; }
}

