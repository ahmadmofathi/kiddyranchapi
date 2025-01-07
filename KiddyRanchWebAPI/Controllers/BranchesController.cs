using ImagesTry;
using KiddyRanchWeb.BL;
using KiddyRanchWeb.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KiddyRanchWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IBranchManager _branchManager;
        public BranchesController(AppDbContext context, IBranchManager branchManager)
        {
            _context = context;
            _branchManager = branchManager;
        }

        [HttpGet]
        public IActionResult GetAllBranchs()
        {
            var Branchs = _branchManager.GetBranchs();
            return Ok(Branchs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBranchById(string id)
        {
            var Branch = _branchManager.GetBranch(id);
            if (Branch == null)
            {
                return BadRequest("Branch not found");
            }
            return Ok(Branch);
        }
        [HttpPost]
        public IActionResult AddBranch([FromForm] BranchAddDTO branch, IFormFile file,IFormFile file2)
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




            #region Checking Extension
            var extension1 = Path.GetExtension(file2.FileName);
            var allowedExtensions1 = new string[]
            {
                ".jpg",".png",".jpeg"
            };
            bool isExtensionAllowed1 = allowedExtensions1.Contains(extension1, StringComparer.InvariantCultureIgnoreCase);
            if (!isExtensionAllowed1)
            {
                return BadRequest(new UploadFileDTO(false, "", "Extension 2 is not allowed"));
            }
            #endregion
            #region Checking Size
            bool isSizeAllowed1 = file.Length is > 0 and < 70_000_000;
            if (!isSizeAllowed1)
            {
                return BadRequest(new UploadFileDTO(false, "", "Size 2 is not allowed"));
            }
            #endregion

            #region GUID
            var newFileName1 = $"{Guid.NewGuid()}{extension1}";
            var imagesPath1 = Path.Combine(Environment.CurrentDirectory, "Uploads/StaticContent/");
            var FullFilePath1 = Path.Combine(imagesPath1, newFileName1);
            #endregion

            using var Stream1 = new FileStream(FullFilePath1, FileMode.Create);
            file.CopyTo(Stream1);
            var URL1 = $"{Request.Scheme}://{Request.Host}/FileManager/GetImage?imageName={newFileName1}";
            branch.logo1 = URL;
            branch.logo2 = URL1;
            var emp = _branchManager.Add(branch);
            if (emp != null)
            {
                return Ok(emp);
            }
            return BadRequest("Error Adding Branch");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBranch(string id)
        {
            BranchDTO? Branch = _branchManager.GetBranch(id);
            if (Branch == null)
            {
                return NotFound("Branch not found");
            }
            _branchManager.Delete(id);
            return Ok("Branch " + id + " has been deleted successfully");
        }

        [HttpPut("{id}")]
        public ActionResult<Branch> UpdateBranch(string id, BranchDTO Branch, IFormFile file, IFormFile file2)
        {
            if (id != Branch.branchId)
            {
                return BadRequest();
            }
            if (file != null) {
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
                Branch.logo1 = URL;
            }

            if (file2 != null)
            {
                #region Checking Extension
                var extension = Path.GetExtension(file2.FileName);
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
                bool isSizeAllowed = file2.Length is > 0 and < 70_000_000;
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
                file2.CopyTo(Stream);
                var URL = $"{Request.Scheme}://{Request.Host}/FileManager/GetImage?imageName={newFileName}";
                Branch.logo2 = URL;
            }
            _branchManager.Update(Branch);
            return NoContent();
        }


    }
}
