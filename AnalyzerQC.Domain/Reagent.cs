using AnalyzerQC.Commons;
using AnalyzerQC.ValueObject;

namespace AnalyzerQC;

public class Reagent : CreationAuditedEntity<int>
{
    public string ReagentName { get; private set; }
    public string Description { get; private set; }
    public bool Status { get; private set; }
    public List<Level> Levels { get; private set; } = [];
    public ModelGroup ModelGroup { get; private set; }
    public int ModelGroupId { get; private set; }
    //TODO: Reagent - ModelGroup: N-N relationship, currently it's 1-N for simplicity

    public Reagent(string reagentName, ModelGroup modelGroup, string description, bool status)
    {
        ModelGroup = modelGroup;
        ReagentName = reagentName;
        Description = description;
        Status = status;
    }

    public void AddLevel(Level level)
    {
        Levels.Add(level);
    }
}