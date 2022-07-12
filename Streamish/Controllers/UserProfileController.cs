//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Streamish.Models;
//using Streamish.Repositories;
//using System;

//namespace Streamish.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserProfileController : ControllerBase
//    {
//        private readonly IUserProfileRepository _profileRepository;

//        public UserProfileController(IUserProfileRepository profileRepository)
//        {
//            _profileRepository = profileRepository;
//        }

//        [HttpGet]
//        public IActionResult Get()
//        {
//            return Ok(_profileRepository.GetAll());
//        }

//        [HttpGet("{id}")]
//        public IActionResult Get(int id)
//        {
//            var profile = _profileRepository.Get(id);
//            if (profile == null)
//            {
//                return NotFound();
//            }
//            return Ok(profile);
//        }

//        [HttpDelete("{id}")]
//        public IActionResult Delete(int id)
//        {
//            _profileRepository.Delete(id);
//            return NoContent();
//        }

//        [HttpGet("{id}")]
//        public IActionResult GetWithVideos(int id)
//        {
//            var profile = _profileRepository.GetWithAuthoredVideos(id);

//            if (profile == null)
//            {
//                return NotFound();
//            }
//            return Ok(profile);
//        }
//        [HttpPost]
//        public IActionResult AddUser(UserProfile user)
//        {
//            throw new NotImplementedException();
//        }
//        [HttpPut("{id}")]
//        public IActionResult Put(int id, UserProfile profile)
//        {
//            throw new NotImplementedException();
//        }

//    }
//}
