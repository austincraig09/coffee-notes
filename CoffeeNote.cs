using SQLite;
using SQLitePCL;

public class CoffeeNote
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string? Brand { get; set; }

    [NotNull]
    public string? Roast { get; set; }

    [NotNull]
    public string? ImagePath { get; set; }
    public DateTime ImportDate { get; set; }
}
