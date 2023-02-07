using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoggingBusinessDomainWEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientOnlyController : ControllerBase
    {
        private readonly ILogger<ClientOnlyController> _logger;

        public ClientOnlyController(ILogger<ClientOnlyController> logger)
        {
            _logger = logger;
        }


        /* ----- Values ----- */

        // GET: api/ClientOnly/GetValues
        [HttpGet]
        public IEnumerable<string> GetValues()
        {
            _logger.LogInformation("GetValues() called");

            return new string[] { "value1", "value2" };
        }

        // GET: api/ClientOnly/GetValuesNullUHException
        [HttpGet]
        public IEnumerable<string> GetValuesNullUHException()
        {
            _logger.LogInformation("GetValuesNullUHException() called");

            string[] testNulls = null;

            testNulls.Count();

            return testNulls;
        }

        // GET: api/ClientOnly/GetValuesNullHException
        [HttpGet]
        public IEnumerable<string> GetValuesNullHException()
        {
            _logger.LogInformation("GetValuesNullHException() called");

            string[] testNulls = null;

            try
            {
                testNulls.Count();
            }
            catch (Exception ex)
            {
                testNulls = new string[] { "testNull1", "testNull2" };

                var errorAttributes = new Dictionary<string, string> { { "foo", "bar" }, { "baz", "luhr" } };

                NewRelic.Api.Agent.NewRelic.NoticeError(ex, errorAttributes);

                _logger.LogError(ex, "GetValuesNullHException() called");
            }

            return testNulls;
        }


        /* ----- Value ----- */

        // GET api/ClientOnly/GetValue/5
        [HttpGet("{id}")]
        public string GetValue(int id)
        {
            _logger.LogInformation("GetValue({@id}) called", id);

            return "value";
        }

        // GET api/ClientOnly/GetValueNullUHException/5
        [HttpGet("{id}")]
        public string GetValueNullUHException(int id)
        {
            _logger.LogInformation("GetValueNullUHException({@id}) called", id);

            string testNull = null;

            testNull.Trim();

            return testNull;
        }

        // GET api/ClientOnly/GetValueNullHException/5
        [HttpGet("{id}")]
        public string GetValueNullHException(int id)
        {
            _logger.LogInformation("GetValueNullHException({@id}) called", id);

            string testNull = null;

            try
            {
                testNull.Trim();
            }
            catch (Exception ex)
            {
                testNull = "testNull";

                var errorAttributes = new Dictionary<string, string> { { "foo", "bar" }, { "baz", "luhr" } };

                NewRelic.Api.Agent.NewRelic.NoticeError(ex, errorAttributes);

                _logger.LogError(ex, "GetValueNullHException(id) called");
            }

            return testNull;
        }
    }
}
