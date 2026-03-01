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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return LevelCode;
        yield return LevelName;
    }

    
}