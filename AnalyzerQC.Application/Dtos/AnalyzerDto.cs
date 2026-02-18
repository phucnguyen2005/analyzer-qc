using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AnalyzerQC.Application.Dtos;

public class AnalyzerDto
{
    /*Serial, ModelName*/
    public string SerialNumber { get; set; }
    public string ModelName { get; set; }
}