using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// POC NOTES: Manually added uses
using System.Net.Http;
using LoggingDataLayerWEBAPI;
using Microsoft.Extensions.Configuration;

namespace LoggingBusinessDomainWEBAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataLayerController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly ILogger<DataLayerController> _logger;
        private HttpClient _httpClient;
        private LoggingDataLayerWEBAPIProxy _loggingDataLayerWEBAPIProxy;

        string _testLoggingDataLayerWEBAPI = null;

        public DataLayerController(IConfiguration configuration, ILogger<DataLayerController> logger, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClient = clientFactory.CreateClient();
            _testLoggingDataLayerWEBAPI = configuration["LoggingDataLayerWEBAPI"];
            _loggingDataLayerWEBAPIProxy = new LoggingDataLayerWEBAPIProxy(_testLoggingDataLayerWEBAPI, _httpClient);
        }


        /* ----- Users ----- */

        // GET: api/DataLayer/GetUsers
        [HttpGet]
        public List<User> GetUsers()
        {
            _logger.LogInformation("DL GetUsers() called");

            return _loggingDataLayerWEBAPIProxy.GetUsersAsync().Result.ToList();
        }

        // GET: api/DataLayer/GetUsersSQLNullUHException
        [HttpGet]
        public List<User> GetUsersSQLNullUHException()
        {
            // return new string[] { "value1", "value2" };

            _logger.LogInformation("DL GetUsersSQLNullUHException() called");

            return _loggingDataLayerWEBAPIProxy.GetUsersSQLNullUHExceptionAsync().Result.ToList();
        }

        // GET: api/DataLayer/GetUsersSQLNullHException
        [HttpGet]
        public List<User> GetUsersSQLNullHException()
        {
            _logger.LogInformation("DL GetUsersSQLNullHException() called");

            return _loggingDataLayerWEBAPIProxy.GetUsersSQLNullHExceptionAsync().Result.ToList();
        }


        /* ----- User ----- */

        // GET api/DataLayer/GetUser/5
        [HttpGet("{id}")]
        public User GetUser(int id)
        {
            _logger.LogInformation("DL GetUser({@id}) called", id);

            return _loggingDataLayerWEBAPIProxy.GetUserAsync(id).Result;
        }

        // GET api/DataLayer/GetUserSQLSingleUHException/5
        [HttpGet("{id}")]
        public User GetUserSQLSingleUHException(int id)
        {
            _logger.LogInformation("DL GetUserSQLSingleUHException({@id}) called", id);

            return _loggingDataLayerWEBAPIProxy.GetUserSQLSingleUHExceptionAsync(id).Result;
        }

        // GET api/DataLayer/GetUserSQLSingleHException/5
        [HttpGet("{id}")]
        public User GetUserSQLSingleHException(int id)
        {
            _logger.LogInformation("DL GetUserSQLSingleHException({@id}) called", id);

            return _loggingDataLayerWEBAPIProxy.GetUserSQLSingleHExceptionAsync(id).Result;
        }
    }
}
