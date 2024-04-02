using Microsoft.AspNetCore.Mvc;
using Model;
using Stack_Overflow_Tag_List_API.Interfaces;
using System.Runtime.CompilerServices;

namespace Stack_Overflow_Tag_List_API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly ITagRepository _tagRepository;
        public TagController(ITagService tagService, ITagRepository tagRepository)
        {
            _tagService = tagService;
            _tagRepository = tagRepository;
        }

        [HttpGet("Get_tags_from_StackOverflow")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tag>))]
        [ProducesResponseType(500)]
        public IActionResult GetFromStackOverflow()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");
            }

            if(!_tagService.PutTagsToRepository())
            {
                return BadRequest("Something went wrong while saving");
            }

            return Ok("Correctly downloaded files");
        }

        [HttpGet("Get_Tags_by_Name")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tag>))]
        [ProducesResponseType(400)]
        public IActionResult GetTagsByName(bool isAscending)
        {
            if(!_tagRepository.AnyTagExists())
            {
                return NotFound("Database is empty");
            }

            var result = _tagRepository.GetAllTagsSortedByName(isAscending);

            if (!ModelState.IsValid)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("Get_Tags_by_Count")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tag>))]
        [ProducesResponseType(400)]
        public IActionResult GetTagsByCount(bool isAscending)
        {
            if (!_tagRepository.AnyTagExists())
            {
                return NotFound("Database is empty");
            }

            var result = _tagRepository.GetAllTagsSortedByCount(isAscending);

            if (!ModelState.IsValid)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
