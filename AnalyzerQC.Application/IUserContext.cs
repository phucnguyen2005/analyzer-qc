namespace AnalyzerQC.Application;

public interface IUserContext
{
    public Guid UserId { get; }
    public bool IsAuthenticated { get; }
}