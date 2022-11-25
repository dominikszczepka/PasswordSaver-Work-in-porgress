using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PassSaver.Entities;
using PassSaver.Models;
using PassSaver.Services;

namespace PassSaver.Controllers
{
    [Route("passwords")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly PassSaverDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordServices _passwordServices;
        public PasswordController(PassSaverDbContext dbContext,IMapper mapper,IPasswordServices passwordServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordServices= passwordServices;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PasswordDto>> GetAll([FromBody] User currentUser)
        {
            var passwords = _passwordServices.GetAllUsersPasswords(currentUser);
            if (passwords.Any())
            {
                return Ok(passwords);
            }
            return BadRequest();
        }
        [Route("/delete")]
        [HttpDelete]
        public ActionResult DeletePassword([FromHeader]int passId, [FromHeader]int userId)
        {
            var isDeleted = _passwordServices.DeletePassword(passId,userId);
            return isDeleted?NoContent():NotFound();
        }
        [Route("/add")]
        [HttpPost]
        public ActionResult AddPassword([FromBody] AddPasswordDto dto)
        {
            var passwordId = _passwordServices.AddPassword(dto);
            return Created($"password/{passwordId}",null);
        }
        [Route("/edit")]
        [HttpPut]
        public ActionResult EditPassword([FromBody] EditPasswordDto dto)
        {
            var isUpdated = _passwordServices.EditPassword(dto);
            return isUpdated ? Ok() : NotFound();
        }
        
    }
}
