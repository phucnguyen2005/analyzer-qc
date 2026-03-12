namespace AnalyzerQC.Domain.Test;

public class ModelGroupUnitTest
{
    [Fact]
    public void InitModelGroup_WhenValid_ThenCreateValidObject()
    {
        var modelGroupName = "abcdefgh";
        var modelGroupCode = "01234567";
        
        
        var modelGroup = new ModelGroup(modelGroupName, modelGroupCode);
        
        Assert.Equal(modelGroup.ModelGroupCode, modelGroupCode);
        Assert.Equal(modelGroup.ModelGroupCode, modelGroupCode);
    }

    [Fact]
    public void InitModelGroup_WhenModelGroupNameIs1Character_ThenThrowException()
    {
        var modelGroupName = "1";
        var modelGroupCode = "01234567";
        
        
        
        
        var ex = Assert.Throws<ArgumentException>(() => new ModelGroup(modelGroupName, modelGroupCode));
        Assert.Equal(ModelGroup.ModelGroupNameLengthError, ex.Message);
    }

    [Fact]
    public void InitModelGroup_WhenModelGroupCodeIsMoreThan10Characters_ThenThrowException()
    {
        var modelGroupName = "12345678";
        var modelGroupCode = "012345678910";
        
        
        
        var ex = Assert.Throws<ArgumentException>(() => new ModelGroup(modelGroupName, modelGroupCode));
        Assert.Equal(ModelGroup.ModelGroupCodeLengthError, ex.Message);
        
    }
    
}