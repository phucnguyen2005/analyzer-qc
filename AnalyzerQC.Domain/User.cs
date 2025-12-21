using System.ComponentModel.DataAnnotations;

namespace AnalyzerQC;

public class User
{
    public const int MinUserNameLength = 3;
    public const int MaxUserNameLength = 20;
    public const string InvalidUsernameError = "Invalid username";
    
    [Key]
    public int Id { get; private set; }
    public string Username { get; private set; }

    public User(int id, string username)
    {
        if (username.Length < MinUserNameLength || username.Length > MaxUserNameLength)
        {
            throw new Exception(InvalidUsernameError);
        }
       
        Id = id;
        Username = username;
    }
}