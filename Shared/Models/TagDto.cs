namespace Shared;

public record TagDto
{
    public string Name { get; init; }

    public static explicit operator Tag(TagDto dto)
    {
        return new Tag { Name = dto.Name };
    }
}

