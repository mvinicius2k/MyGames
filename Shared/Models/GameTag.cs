using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared;

public class GameTag
{
    
    public const int TagMaxLength = Shared.Tag.NameMaxLength;

    [ForeignKey(nameof(TagInstance)), MaxLength(TagMaxLength)]
    public string Tag { get; set; }
    public int GameId { get; set; }
    public bool Highlight { get; set; }


    
    public virtual Tag TagInstance { get; set; }
    public virtual Game Game { get; set; }

}

