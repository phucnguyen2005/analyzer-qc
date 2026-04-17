using AnalyzerQC.Commons;
using AnalyzerQC.ValueObject;

namespace AnalyzerQC;

public class AssayLimit : FullAuditedEntity<Guid>
{
    public Guid LotId { get; set; }
    public int ReagentId { get; set; }
    public Lot Lot { get; set; } = null!;
    public Reagent Reagent { get; set; } = null!;
    public List<AssayLimitParameter> AssayLimitParameters { get; set; } = [];
    public Level Level { get; set; }

    public AssayLimit()
    {
        
    }
    public AssayLimit(Guid lotId, int reagentId, Level level)
    {
        LotId = lotId;
        ReagentId = reagentId;
        Level = level;
    }
}