using Microsoft.AspNetCore.Mvc;
using Subscriber.CORE.DTO;
using Subscriber.CORE.Interface_Service;
using Subscriber.CORE.Response;
using Subscriber.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Subscriber.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightWatchersController : ControllerBase
    {
        IWeight_WatchersService _Weight_WatchersService;
        public WeightWatchersController(IWeight_WatchersService Weight_WatchersService)
        {
            _Weight_WatchersService = Weight_WatchersService;
        }


        // GET: api/<WeightWatchersController>
        [HttpGet]
        public async Task<ActionResult<BaseResponseGeneric<bool>>> Register([FromBody] SubscriberDTO subscriberDTO)
        {
            var response = await _Weight_WatchersService.Register(subscriberDTO);
           if(response.Succeed) return Ok(response);
           return BadRequest(response);
        }

        // GET api/<WeightWatchersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponseGeneric<CardResponse>>> GetCardById(int id)
        {
            var response = await _Weight_WatchersService.GetCardDetails(id);
            if (response.Succeed)
                return Ok(response);
            return NotFound(response);
        }

        // POST api/<WeightWatchersController>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<BaseResponseGeneric<int>>> Login([FromBody] string email,string password)
        {
            var response =await _Weight_WatchersService.Login(email, password);
            if(!response.Succeed)
                return NotFound(response);
            return Ok(null);
        }
    }
}
