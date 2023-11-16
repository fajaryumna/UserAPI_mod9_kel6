using APImod9kel6.Data;
using APImod9kel6.Models.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APImod9kel6.Controllers
{
    [EnableCors("AllowReactApp")]
    [Route("api/UserAPI")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            return Ok(UserStore.userList);
        }

        
        [HttpGet("{id:int}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(404)]
        public ActionResult<UserDTO> GetUser(int id)
        {
            if (id == 0) return BadRequest();

            var user = UserStore.userList.FirstOrDefault(u => u.Id == id);

            if (user == null) return NotFound();

            return Ok(user);
        }

       
        [HttpPost]
        public ActionResult<UserDTO> CreateUser([FromBody] UserDTO userDTO)
        {
            if (UserStore.userList.FirstOrDefault(u => u.Username.ToLower() == userDTO.Username.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Username already exists");
                return BadRequest(ModelState);
            }

            if (userDTO == null)
            {
                return BadRequest(userDTO);
            }
            if (userDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            userDTO.Id = UserStore.userList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            UserStore.userList.Add(userDTO);
            string response = "Sukses menambahkan data User baru" + "\nId : " + userDTO.Id.ToString() + "\nUsername : " + userDTO.Username;

            return CreatedAtRoute("GetUser", new { id = userDTO.Id }, response);
        }

        [HttpPost("Login")]
        public ActionResult<UserDTO> LoginUser([FromBody] UserDTO userDto)
        {
            var user = UserStore.userList.FirstOrDefault(u =>
                u.Username.ToLower() == userDto.Username.ToLower() && u.Password == userDto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(new { 
                Message = "Login successful!",
                token = "Qwerwoy34897h",
            });
        }



        [HttpDelete("{id:int}", Name = "DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var user = UserStore.userList.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            UserStore.userList.Remove(user);
            return NoContent();
        }

       
        [HttpPut("{id:int}", Name = "UpdateUser")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO userDTO)
        {
            if (userDTO == null || id != userDTO.Id)
            {
                return BadRequest();
            }
            var user = UserStore.userList.FirstOrDefault(u => u.Id == id);
            user.Username = userDTO.Username;
            user.Password = userDTO.Password;

            return NoContent();
        }
    }
}
