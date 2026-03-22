namespace AnalyzerQC.Domain.Test;

public class AnalyzerUnitTest
{
    [Fact]
    public void InitAnalyzer_WhenValid_ThenCreateValidObject()
    {
        var model = new Model("ModelName", "ModelCode", 1);
        var site = new Site("SiteName", "SiteCode", "Address", "TimeZone", true);
        var serialNumber = "12345678";
        var status = true;
        var analyzer = new Analyzer(model, site, serialNumber, status);
        
        Assert.Equal(analyzer.Model, model);
        Assert.Equal(analyzer.AssignedSite, site);
        Assert.Equal(analyzer.SerialNumber, serialNumber);
        Assert.Equal(analyzer.Status, status);
    }

    [Fact]
    public void InitAnalyzer_WhenSerialNumberIsLessThan8Characters_ThenThrowException()
    {
        var model = new Model("ModelName", "ModelCode", 1);
        var site = new Site("SiteName", "SiteCode", "Address", "TimeZone", true);
        var serialNumber = "12345";
        var status = true;
     
        
        var ex = Assert.Throws<ArgumentException>(() => new Analyzer(model, site, serialNumber, status));
        Assert.Equal(Analyzer.SerialNumberLengthErrorMessage, ex.Message);
    }
    
    [Fact]
    public void UpdateAnalyzer_WhenValid_ThenUpdateObject()
    {
        var model = new Model("ModelCode", "ModelName", 1);
        var site = new Site("SiteName", "SiteCode", "Address", "TimeZone", true);
        var serialNumber = "12345678";
        var status = true;
        var analyzer = new Analyzer(model, site, serialNumber, status);
        
        var newModel = new Model("NewCode", "NewModelName", 2);
        var newSerialNumber = "87654321";
        
        analyzer.Update(newModel, newSerialNumber);
        
        Assert.Equal(analyzer.Model, newModel);
        Assert.Equal(analyzer.SerialNumber, newSerialNumber);
     }
}