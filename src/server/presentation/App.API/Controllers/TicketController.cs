using App.Application.ModelView.Ticket;
using App.Application.Repositories.Tickets;
using App.Domain;
using App.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly IWriteTicketRepository _writeTicketRepository;
        private readonly IReadTicketRepository _readTicketRepository;
        public TicketController(IWriteTicketRepository writeTicketRepository, IReadTicketRepository readTicketRepository)
        {
            _writeTicketRepository = writeTicketRepository;
            _readTicketRepository = readTicketRepository;
        }
        // GET: api/<TicketController>
        [HttpGet]
        public IEnumerable<GetTicketViewModel> Get()
        {
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            if (userRole == RoleEnum.Admin.Name)
                return _readTicketRepository.GetAll();
            else
                return _readTicketRepository.GetByUserId(userId);
        }

        // GET api/<TicketController>/5
        [HttpGet("{id}")]
        public GetTicketViewModel Get(int id)
        {
            var result = _readTicketRepository.GetById(id);

            return new GetTicketViewModel
            {
                Created = result.CreatedDate,
                CustomerFullName = result.Customer.Name + " " + result.Customer.Surname,
                Id = result.Id,
                Message = result.Message,
                Status = TicketStatus.From(result.Status).Name,
                StatusId = result.Status,
                Subject = result.Subject 
            };
        }

        // POST api/<TicketController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTicketViewModel model)
        {
            var response = new ServiceResponse();
            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.Message = "Boş alanları doldurun";
                return BadRequest(response);
            }
            else
            {
                var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

                await _writeTicketRepository.Create(model, userId);

                response.IsSuccess = true;

                return Ok(response);
            }
        }

        // PUT api/<TicketController>/5
        [HttpPut]
        public async Task<IActionResult> Put(PutTicketViewModel model)
        {
            var response = new ServiceResponse();
            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.Message = "Boş alanları doldurun";
                return BadRequest(response);
            }
            else
            {
                var statusCheck = TicketStatus.List().FirstOrDefault(x => x.Id == model.StatusId);
                if (statusCheck != null)
                {
                    var getTicket = _readTicketRepository.GetById(model.Id);
                    if (getTicket != null)
                    {
                        getTicket.Status = model.StatusId;
                        await _writeTicketRepository.UpdateStatus(getTicket);
                        response.IsSuccess = true;
                        return Ok(response);
                    }
                }
                response.IsSuccess = false;
                return BadRequest(response);

            }
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
