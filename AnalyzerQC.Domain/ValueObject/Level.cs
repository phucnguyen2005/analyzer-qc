namespace AnalyzerQC.ValueObject;

public class Level : Commons.ValueObject
{
    public string LevelCode { get; private set; }
    public string LevelName { get; private set; }


    public Level(string levelCode, string levelName)
    {
        LevelCode = levelCode;
        LevelName = levelName;
    }

    //TODO: no magic string
    public static bool TryParse(string? input, out Level level)
    {
        level = null!;
        if (input == null)
        {
            return false;
        }

        if (input.Equals("High"))
        {
            level = new Level("H", "High");
            return true;
        }

        if (input.Equals("Medium"))
        {
            level = new Level("M", "Medium");
            return true;
        }

        if (input.Equals("Low"))
        {
            level = new Level("L", "Low");
            return true;
        }

        return false;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return LevelCode;
        yield return LevelName;
    }
}