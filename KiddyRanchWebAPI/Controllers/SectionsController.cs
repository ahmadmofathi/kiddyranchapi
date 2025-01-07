using KiddyRanchWeb.BL;
using KiddyRanchWeb.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KiddyRanchWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly ISectionManager _sectionManager;
        public SectionsController(AppDbContext context, ISectionManager sectionManager)
        {
            _sectionManager = sectionManager;
        }

        [HttpGet]
        public IActionResult GetAllSections()
        {
            var Sections = _sectionManager.GetSections();
            return Ok(Sections);
        }

        [HttpGet("{id}")]
        public IActionResult GetSectionById(string id)
        {
            var Section = _sectionManager.GetSection(id);
            if (Section == null)
            {
                return BadRequest("Section not found");
            }
            return Ok(Section);
        }
        [HttpPost]
        public IActionResult AddSection(SectionAddDTO section)
        {
            var emp = _sectionManager.Add(section);
            if (emp != null)
            {
                return Ok(emp);
            }
            return BadRequest("Error Adding Section");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSection(string id)
        {
            SectionWithImagesDTO? Section = _sectionManager.GetSection(id);
            if (Section == null)
            {
                return NotFound("Section not found");
            }
            _sectionManager.Delete(id);
            return Ok("Section " + id + " has been deleted successfully");
        }

        [HttpPut("{id}")]
        public ActionResult<Section> UpdateSection(string id, SectionUpdateDTO Section)
        {
            if (id != Section.sectionId)
            {
                return BadRequest();
            }
            _sectionManager.Update(Section);
            return NoContent();
        }

    }
}
