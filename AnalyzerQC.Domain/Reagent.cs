using AnalyzerQC.Commons;
using AnalyzerQC.ValueObject;

namespace AnalyzerQC;

public class Reagent : CreationAuditedEntity<int>
{
    public string ReagentName { get; private set; }
    public string ReagentCode { get; private set; }
    public string Description { get; private set; }
    public bool Status { get; private set; }
    public List<Level> Levels { get; private set; } = [];


    public List<ModelGroup> ModelGroups { get; private set; } = [];

    public List<Lot> Lots { get; private set; } = [];
    public List<AssayLimit> AssayLimits { get; private set; } = [];
    private Reagent()
    {
    }

    public Reagent(string reagentName, string reagentCode, string description, bool status, List<Level> levels, List<ModelGroup> modelGroups)
    {
        Levels = levels;
        ModelGroups = modelGroups;
        ReagentName = reagentName;
        ReagentCode = reagentCode;
        Description = description;
        Status = status;
        CreatorId = "System";
        CreationTime = DateTime.UtcNow;
    }
}