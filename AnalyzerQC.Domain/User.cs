using AnalyzerQC.Commons;

namespace AnalyzerQC;

public class User : FullAuditedEntity<Guid>
{
    public const int MinUserNameLength = 3;
    public const int MaxUserNameLength = 20;
    public const string InvalidUsernameError = "Invalid username";

    public string UserName { get; private set; }

    private User()
    {
    }

    public User(string userName)
    {
        if (userName.Length < MinUserNameLength || userName.Length > MaxUserNameLength)
        {
            throw new Exception(InvalidUsernameError);
        }

        UserName = userName;
    }
}