
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //With the [EnableCors] attribute.
    [EnableCors(PolicyName = "AllowOnlyMicrosoft")]
    public class MicrosoftController : ControllerBase
    {

    }
}
