using ASPNETCoreWebAPI.EFDBFirst;
using ASPNETCoreWebAPI.MyLogging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //EF DB First
        private readonly NorthwindContext _dbContext;

        private readonly ILogger<DemoController> _logger;
        //1. strongly coupled/ tightly coupled
        //public DemoController()
        //{
        //    _myLogger = new LogToDB();
        //}

        //2. loosely coupled
        public DemoController(ILogger<DemoController> logger, NorthwindContext northwindContext)
        {
            _logger = logger;
            _dbContext = northwindContext;
        }

        [HttpGet("log")]
        public ActionResult Index()
        {
            _logger.LogTrace("Log message from trace method");
            _logger.LogDebug("Log message from Debug method");
            _logger.LogInformation("Log message from Information method");
            _logger.LogWarning("Log message from Warning method");
            _logger.LogError("Log message from Error method");
            _logger.LogCritical("Log message from Critical method");

            return Ok();
        }

        [HttpGet("customers", Name = "GetCustomerData")]
        public IEnumerable<dynamic> Get()
        {
            return _dbContext.Customers.ToList();
        }
    }
}
