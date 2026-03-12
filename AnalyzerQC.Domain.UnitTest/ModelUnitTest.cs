namespace AnalyzerQC.Domain.Test;

public class ModelUnitTest
{
    [Fact]
    public void InitModel_WhenValid_ThenCreateValidObject()
    {
        var modelName = "ModelName12";
        var modelCode = "ModelCode1";
        var modelGroupId = 1;
        
        var model = new Model(modelCode, modelName, modelGroupId);
        
        Assert.Equal(model.ModelName, modelName);
        Assert.Equal(model.ModelCode, modelCode);
        Assert.Equal(model.ModelGroupId, modelGroupId);
    }

    [Fact]
    public void InitModel_WhenModelNameIs1Character_ThenThrowException()
    {
        var modelName = "1";
        var modelCode = "ModelCode1";
        var modelGroupId = 1;
        
        
        
        var ex = Assert.Throws<ArgumentException>(() => new Model(modelCode, modelName, modelGroupId));
        Assert.Equal(Model.ModelNameLengthError, ex.Message);
    }

    [Fact]
    public void InitModel_WhenModelCodeIsMoreThan10Characters_ThenThrowException()
    {
        var modelName = "ModelName12";
        var modelCode = "ModelCode123";
        var modelGroupId = 1;
        
        
        
        var ex = Assert.Throws<ArgumentException>(() => new Model(modelCode, modelName, modelGroupId));
        Assert.Equal(Model.ModelCodeLengthError, ex.Message);
        
    }
    
}