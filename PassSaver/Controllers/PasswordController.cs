using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PassSaver.Entities;
using PassSaver.Models;
using PassSaver.Services;

namespace PassSaver.Controllers
{
    [Route("password")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly PassSaverDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly PasswordServices passwordServices;
        public PasswordController(PassSaverDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            passwordServices= new PasswordServices(_dbContext,_mapper);
        }
        [HttpDelete]
        public ActionResult DeletePassword([FromHeader]int passId, [FromHeader]int userId)
        {
            var isDeleted = passwordServices.DeletePassword(passId,userId);
            return isDeleted?NoContent():NotFound();
        }
        [HttpPost]
        public ActionResult AddPassword([FromBody] AddPasswordDto dto)
        {
            var passwordId = passwordServices.AddPassword(dto);
            return Created($"password/{passwordId}",null);
        }
        [HttpPut]
        public ActionResult EditPassword([FromBody] EditPasswordDto dto)
        {
            var isUpdated = passwordServices.EditPassword(dto);
            return isUpdated ? Ok() : NotFound();
        }
        [HttpGet]
        public ActionResult<IEnumerable<PasswordDto>> GetAll([FromBody] User currentUser)
        {
            var passwords = passwordServices.GetAllUsersPasswords(currentUser);
            if (passwords.Any())
            {
                return Ok(passwords);
            }
            return BadRequest();
        }
    }
}
