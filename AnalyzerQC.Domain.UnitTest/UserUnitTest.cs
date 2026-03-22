namespace AnalyzerQC.Domain.Test;

public class UserUnitTest
{
    [Fact]
    public void InitUser_WhenValid_ThenCreateValidObject()
    {
        var userName = "UserName12";

        var user = new User(userName);

        Assert.Equal(user.UserName, userName);
    }

    [Fact]
    public void InitUser_WhenUserNameIs1Character_ThenThrowException()
    {
        var userName = "1";
        
        var ex = Assert.Throws<Exception>(() => new User(userName));
        Assert.Equal(User.InvalidUsernameError, ex.Message);
    }
}