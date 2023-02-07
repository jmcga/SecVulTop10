using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// POC NOTES: Manually added uses
using Microsoft.Extensions.Logging;
using LoggingDataLayerWEBAPI.Models;


namespace LoggingDataLayerWEBAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly LoggingPOCContext _context;

        public UsersController(ILogger<UsersController> logger, LoggingPOCContext context)
        {
            _logger = logger;
            _context = context;
        }


        /* ----- Users ----- */

        // GET: api/Users/GetUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            _logger.LogInformation("GetUsers() called");

            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/GetUsersSQLNullUHException
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersSQLNullUHException()
        {
            _logger.LogInformation("GetUsersSQLNullUHException() called");

            DbSet<User> nullUsers = null;

            return await nullUsers.ToListAsync();
        }

        // GET: api/Users/GetUsersSQLNullHException
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersSQLNullHException()
        {
            _logger.LogInformation("GetUsersSQLNullHException() called");

            List<User> userList;

            try
            {
                DbSet<User> nullUsers = null;

                userList = await nullUsers.ToListAsync();
            }
            catch (Exception ex)
            {
                userList = new List<User>();

                var errorAttributes = new Dictionary<string, string> { { "foo", "bar" }, { "baz", "luhr" } };

                NewRelic.Api.Agent.NewRelic.NoticeError(ex, errorAttributes);

                _logger.LogError(ex, "GetUsersSQLNullHException() called");
            }

            return userList;
        }


        /* ----- User ----- */

        // GET: api/User/GetUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            _logger.LogInformation("GetUser({@id}) called", id);

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/User/GetUserSQLSingleUHException/5
        [HttpGet]
        public async Task<ActionResult<User>> GetUserSQLSingleUHException(int id)
        {
            _logger.LogInformation("GetUserSQLSingleUHException({@id}) called", id);

            return await _context.Users.SingleAsync(x => x.UserName == "GENERATE NO SINGLE FOUND EXCEPTION");
        }

        // GET: api/User/GetUserSQLSingleHException/5
        [HttpGet]
        public async Task<ActionResult<User>> GetUserSQLSingleHException(int id)
        {
            _logger.LogInformation("GetUserSQLSingleHException({@id}) called", id);

            User aUser;

            try
            {
                aUser = await _context.Users.SingleAsync(x => x.UserName == "GENERATE NO SINGLE FOUND EXCEPTION");
            }
            catch (Exception ex)
            {
                aUser = new User();

                var errorAttributes = new Dictionary<string, string> { { "foo", "bar" }, { "baz", "luhr" } };

                NewRelic.Api.Agent.NewRelic.NoticeError(ex, errorAttributes);

                _logger.LogError(ex, "GetUserSQLSingleHException(id) called");
            }

            return aUser;
        }
    }
}
