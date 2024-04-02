using Microsoft.AspNetCore.Mvc;
using Model;
using Stack_Overflow_Tag_List_API.Interfaces;

namespace Stack_Overflow_Tag_List_API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("GetFromStackOverflow")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tag>))]
        [ProducesResponseType(500)]
        public IActionResult GetFromStackOverflow()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");
            }

            if(!_tagService.GetTags().Result)
            {
                return BadRequest("Something went wrong while saving");
            }

            return Ok("Correctly downloaded files");
        }
    }
}
