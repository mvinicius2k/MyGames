using System.ComponentModel.DataAnnotations.Schema;

namespace Shared;

public class GamePlatform
{
    [ForeignKey(nameof(PlatformInstance))]
    public string Platform { get; set; }
    public int GameId { get; set; }
    public bool IsTarget { get; set; }

    
    public virtual Platform PlatformInstance { get; set; }
    public virtual Game Game { get; set; }

}

