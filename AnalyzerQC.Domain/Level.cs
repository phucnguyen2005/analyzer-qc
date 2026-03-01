using AnalyzerQC.Commons;

namespace AnalyzerQC;

public class Level: CreationAuditedEntity<int>
{
    
    public string LevelCode { get; private set; }
    public string LevelName { get; private set; }
    public LevelOrders LevelOrders { get; private set; }
    
    
    public Level(string levelCode, string levelName, LevelOrders levelOrders)
    {
        LevelCode = levelCode;
        LevelOrders = levelOrders;
        LevelName = levelName;
    }
}