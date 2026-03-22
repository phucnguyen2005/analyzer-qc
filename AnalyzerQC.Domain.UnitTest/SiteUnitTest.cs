using AnalyzerQC.Commons;

namespace AnalyzerQC.Domain.Test;

public class SiteUnitTest
{
    private static readonly List<WorkingDays> DefaultWorkingDays =
    [
        WorkingDays.Monday,
        WorkingDays.Tuesday,
        WorkingDays.Wednesday,
        WorkingDays.Thursday,
        WorkingDays.Friday
    ];

    // A A A - Arrange Act Assert
    [Fact]
    public void InitSite_WhenValid_ThenCreateValidObject()
    {
        // Arrange
        var siteName = "HelloSiteName";
        var siteCode = "HellCode";
        var address = "Address";
        var timeZone = "TimeZone";
        var isActive = true;

        // Act
        var site = new Site(siteName, siteCode, address, timeZone, isActive);

        // Assert
        Assert.Equal(site.SiteName, siteName);
        Assert.Equal(site.SiteCode, siteCode);
        Assert.Equal(site.Address, address);
        Assert.Equal(site.TimeZone, timeZone);
        Assert.Equal(site.IsActive, isActive);

        Assert.Equal(site.Analyzers.Count, 0);
        Assert.Equal(site.WorkingDays, DefaultWorkingDays);
    }

    // Act When Then
    [Fact]
    public void InitSite_WhenSiteNameIs1Character_ThenThrowException()
    {
        // Arrange
        var siteName = "H";
        var siteCode = "HellCode";
        var address = "Address";
        var timeZone = "TimeZone";
        var isActive = true;

        // Act Assert
        var ex = Assert.Throws<ArgumentException>(() => new Site(siteName, siteCode, address, timeZone, isActive));
        Assert.Equal(Site.SiteNameLengthError, ex.Message);
    }

    [Fact]
    public void UpdateSite_WhenSiteNameIs1Character_ThenThrowException()
    {
        // Arrange
        var siteName = "HelloSiteName";
        var siteCode = "HellCode";
        var address = "Address";
        var timeZone = "TimeZone";
        var isActive = true;

        var site = new Site(siteName, siteCode, address, timeZone, isActive);

        // Act Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            site.Update(
                "H", // Fail due to 1 character
                address,
                timeZone)
        );
        Assert.Equal(Site.SiteNameLengthError, ex.Message);
    }
    
    [Fact]
    public void UpdateSite_WhenValidIs_ThenUpdateValue()
    {
        // Arrange
        var siteName = "HelloSiteName";
        var siteCode = "HellCode";
        var address = "Address";
        var timeZone = "TimeZone";
        var isActive = true;

        var site = new Site(siteName, siteCode, address, timeZone, isActive);

        var updatedSiteName = "HelloSiteNameButUpdated";
        var updatedAddress = "New Address";
        var updatedTimeZone = "New TimeZone";
        // Act 
        site.Update(
            updatedSiteName, // Fail due to 1 character
            updatedAddress,
            updatedTimeZone);
        
        // Assert
        Assert.Equal(site.SiteName, updatedSiteName);
        Assert.Equal(site.Address, updatedAddress);
        Assert.Equal(site.TimeZone, updatedTimeZone);
    }
    
    [Fact]
    public void InitSite_WhenSiteCodeIsNot8Characters_ThenThrowException()
    {
        // Arrange
        var siteName = "HelloSiteName";
        var siteCode = "Hell"; // Fail due to not 8 characters
        var address = "Address";
        var timeZone = "TimeZone";
        var isActive = true;

        // Act Assert
        var ex = Assert.Throws<ArgumentException>(() => new Site(siteName, siteCode, address, timeZone, isActive));
        Assert.Equal(Site.SiteCodeLengthError, ex.Message);
    }
}