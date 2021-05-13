using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();

            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDTO>> GetUser(string username)
        {
            var user = await _userRepository.GetMemberAsync(username);
            return user;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDTO memberUpdateDTO) {
            // Get username from current token
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(username);

            /*
            use automapper for this functionality

            user.City = memberUpdateDTO.City;
            user.Country = memberUpdateDTO.Country;
            .
            .
            .*/

            // use automapper
            _mapper.Map(memberUpdateDTO, user);

            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) {
                return NoContent();
            }

            return BadRequest("Failed update user");
        }
    }
}