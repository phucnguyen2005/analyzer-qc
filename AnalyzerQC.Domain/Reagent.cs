using AnalyzerQC.Commons;
using AnalyzerQC.ValueObject;

namespace AnalyzerQC;

public class Reagent : CreationAuditedEntity<int>
{
    public string ReagentCode { get; private set; }
    public string ReagentName { get; private set; }
    public string Description { get; private set; }
    public bool Status { get; private set; }
    public List<Level> Levels { get; private set; } = [];
    public ModelGroup ModelGroup { get; private set; }
    public int ModelGroupId { get; private set; }
   

    public Reagent(string reagentCode, string reagentName, int modelGroupId, string description, bool status)
    {
        ModelGroupId = modelGroupId;
        ReagentCode = reagentCode;
        ReagentName = reagentName;
        Description = description;
        Status = status;
    }
    public void AddLevel(Level level)
    {
        Levels.Add(level);
    }
}