using ImagesTry;
using KiddyRanchWeb.BL;
using KiddyRanchWeb.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KiddyRanchWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IImageFileManager _imageFileManager;
        public ImagesController(AppDbContext context, IImageFileManager imageFileManager)
        {
            _context = context;
            _imageFileManager = imageFileManager;
        }

        [HttpGet]
        public IActionResult GetAllImageFiles()
        {
            var ImageFiles = _imageFileManager.GetImageFiles();
            return Ok(ImageFiles);
        }

        [HttpGet("{id}")]
        public IActionResult GetImageFileById(string id)
        {
            var ImageFile = _imageFileManager.GetImageFile(id);
            if (ImageFile == null)
            {
                return BadRequest("Image File not found");
            }
            return Ok(ImageFile);
        }
        [HttpPost]
        public IActionResult AddImageFile([FromForm] ImageAddDTO imageFile, IFormFile file)
        {

            #region Checking Extension
            var extension = Path.GetExtension(file.FileName);
            var allowedExtensions = new string[]
            {
                ".jpg",".png",".jpeg"
            };
            bool isExtensionAllowed = allowedExtensions.Contains(extension, StringComparer.InvariantCultureIgnoreCase);
            if (!isExtensionAllowed)
            {
                return BadRequest(new UploadFileDTO(false, "", "Extension is not allowed"));
            }
            #endregion
            #region Checking Size
            bool isSizeAllowed = file.Length is > 0 and < 70_000_000;
            if (!isSizeAllowed)
            {
                return BadRequest(new UploadFileDTO(false, "", "Size is not allowed"));
            }
            #endregion

            #region GUID
            var newFileName = $"{Guid.NewGuid()}{extension}";
            var imagesPath = Path.Combine(Environment.CurrentDirectory, "Uploads/StaticContent/");
            var FullFilePath = Path.Combine(imagesPath, newFileName);
            #endregion

            using var Stream = new FileStream(FullFilePath, FileMode.Create);
            file.CopyTo(Stream);
            var URL = $"{Request.Scheme}://{Request.Host}/FileManager/GetImage?imageName={newFileName}";
            imageFile.imgPath = URL;
            var im = _imageFileManager.Add(imageFile);
            if (im != null)
            {
                return Ok(im);
            }
            return BadRequest("Error Adding ImageFile");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteImageFile(string id)
        {
            ImageReadDTO? ImageFile = _imageFileManager.GetImageFile(id);
            if (ImageFile == null)
            {
                return NotFound("ImageFile not found");
            }
            _imageFileManager.Delete(id);
            return Ok("ImageFile " + id + " has been deleted successfully");
        }

        [HttpPut("{id}")]
        public ActionResult<ImageFile> UpdateImageFile(string id, ImageReadDTO ImageFile)
        {
            if (id != ImageFile.imageFileId)
            {
                return BadRequest();
            }
            _imageFileManager.Update(ImageFile);
            return NoContent();
        }


    }
}
