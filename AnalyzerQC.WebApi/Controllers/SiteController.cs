using Microsoft.AspNetCore.Mvc;

namespace AnalyzerQC.WebApi.Controllers;

using AnalyzerQC;

[ApiController]
[Route("[controller]")]
public class SiteController : ControllerBase
{
    [HttpGet] 
    [Route("get-site")]
    public List<Site> GetSite()
    {
        return Repositories.Sites;
    }

    
    
    
}




