using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PassSaver.Entities;
using PassSaver.Models;
using PassSaver.Services;

namespace PassSaver.Controllers
{
    [Route("password")]
    public class PasswordController : ControllerBase
    {
        private readonly PassSaverDbContext _passwordDbContext;
        private readonly IMapper _mapper;
        private readonly PasswordServices passwordServices;
        public PasswordController(PassSaverDbContext passwordDbContext,IMapper mapper)
        {
            _passwordDbContext = passwordDbContext;
            _mapper = mapper;
            passwordServices= new PasswordServices(_passwordDbContext,_mapper);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var passwordId = passwordServices.AddPassword(dto);
            return Created($"password/{passwordId}",null);
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
