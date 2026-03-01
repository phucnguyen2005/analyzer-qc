using AnalyzerQC.Commons;

namespace AnalyzerQC;

public class User : FullAuditedEntity<Guid>
{
    public const int MinUserNameLength = 3;
    public const int MaxUserNameLength = 20;
    public const string InvalidUsernameError = "Invalid username";

    public string Username { get; private set; }

    public User(string username)
    {
        if (username.Length < MinUserNameLength || username.Length > MaxUserNameLength)
        {
            throw new Exception(InvalidUsernameError);
        }

        Username = username;
    }
}