namespace AnalyzerQC;

public class Analyzer
{
    public Guid Id { get; private set; }
    public int ModelId { get; set; }
    public Model Model { get; set; }

    public Guid SiteId { get; set; }
    public Site AssignedSite { get; set; }
    public string SerialNumber { get; set; }
    public bool Status { get; set; }


    public Analyzer()
    {
    }

    public Analyzer(Model model, Site assignedSite, string serialNumber, bool status)
    {
        if (serialNumber.Length != 8)
        {
            throw new ArgumentException("serialNumber must be 8 characters long");
        }

        Model = model;
        AssignedSite = assignedSite;
        SerialNumber = serialNumber;
        Status = status;
    }

    public void Update(Model model, string serialNumber)
    {
        Model = model;
        SerialNumber = serialNumber;
    }

    public void SetStatus(bool status)
    {
        Status = status;
    }
}