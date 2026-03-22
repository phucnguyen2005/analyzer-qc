using MockQueryable.Moq;
using Moq;
namespace AnalyzerQC.Application.UnitTest;

public class SiteServiceUnitTest
{
    [Fact]
    public async Task DeleteSite_WhenSiteFound_ThenSiteDeleted()
    {
        // Arrange
        var siteName = "HelloSiteName";
        var siteCode = "HellCode";
        var address = "Address";
        var timeZone = "TimeZone";
        var isActive = true;

        var site = new Site(siteName, siteCode, address, timeZone, isActive);

        List<Site> tempList = [site];

        var mockAppDbContext = new Mock<IAppDbContext>();
        mockAppDbContext
            .Setup(x => x.Sites)
            .Returns(tempList.AsQueryable().BuildMockDbSet().Object);

        var siteService = new SiteService(mockAppDbContext.Object);

        // Act
        var result = await siteService.DeleteSite(site.SiteCode);

        // Assert
        Assert.True(result);
        mockAppDbContext.Verify(dep => dep.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }
    
    [Fact]
    public async Task DeleteSite_WhenSiteNotFound_ThenReturnFalse()
    {
        // Arrange
        var mockAppDbContext = new Mock<IAppDbContext>();
        mockAppDbContext
            .Setup(x => x.Sites)
            .Returns(new List<Site>().AsQueryable().BuildMockDbSet().Object);

        var siteService = new SiteService(mockAppDbContext.Object);

        // Act
        var result = await siteService.DeleteSite("SampleSite");

        // Assert
        Assert.False(result);
        mockAppDbContext.Verify(dep => dep.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
    }
}