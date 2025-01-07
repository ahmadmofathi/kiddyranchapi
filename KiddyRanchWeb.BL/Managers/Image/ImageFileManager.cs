using KiddyRanchWeb.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class ImageFileManager : IImageFileManager
    {
        private readonly IImageRepo _imageFileRepo;

        public ImageFileManager(IImageRepo imageFileRepo)
        {
            _imageFileRepo = imageFileRepo;
        }
        public IEnumerable<ImageReadDTO> GetImageFiles()
        {
            IEnumerable<ImageFile> imageFilesDB = _imageFileRepo.GetImages();
            return imageFilesDB.Select(u => new ImageReadDTO
            {
                imageFileId = u.imageFileId,
                imgDescription = u.imgDescription,
                imgName = u.imgName,
                imgPath = u.imgPath,
                sectionId = u.sectionId,
            });
        }

        public List<ImageReadDTO> GetSectionImages(string sectionId)
        {
            IEnumerable<ImageFile> imageFilesDB = _imageFileRepo.GetImages();
            return imageFilesDB.Select(u => new ImageReadDTO
            {
                imageFileId = u.imageFileId,
                imgDescription = u.imgDescription,
                imgName = u.imgName,
                imgPath = u.imgPath,
                sectionId = u.sectionId,
            }).Where(u=>u.sectionId==sectionId).ToList();
        }
        public string Add(ImageAddDTO u)
        {
            ImageFile imageFile = new ImageFile
            {
                imageFileId = Guid.NewGuid().ToString(),
                imgName = u.imgName,
                imgDescription = u.imgDescription,
                imgPath = u.imgPath,
                sectionId = u.sectionId,
            };
            _imageFileRepo.Add(imageFile);
            _imageFileRepo.SaveChanges();
            return imageFile.imageFileId;
        }

        public bool Delete(string id)
        {
            ImageFile? imageFile = _imageFileRepo.GetById(id);
            if (imageFile == null)
            {
                return false;
            }
            _imageFileRepo.Delete(imageFile);
            _imageFileRepo.SaveChanges();
            return true;
        }

        public ImageReadDTO GetImageFile(string id)
        {
            ImageFile? imageFile = _imageFileRepo.GetById(id);
            if (imageFile is null)
            {
                return null;
            }

            return new ImageReadDTO
            {
                imageFileId = imageFile.imageFileId,
                imgName = imageFile.imgName,
                imgDescription = imageFile.imgDescription,
                imgPath = imageFile.imgPath,
                sectionId = imageFile.sectionId,
            };
        }
        public ImageFile GetImageFileById(string id)
        {
            ImageFile? imageFile = _imageFileRepo.GetById(id) as ImageFile;
            if (imageFile == null)
            {
                return null;
            }
            return imageFile;
        }

        public bool Update(ImageReadDTO imageFileRequest)
        {
            if (imageFileRequest.imageFileId == null)
            {
                return false;
            }
            ImageFile? imageFile = _imageFileRepo.GetById(imageFileRequest.imageFileId);
            if (imageFile is null)
            {
                return false;
            }
            imageFile.imgName = imageFileRequest.imgName;
            imageFile.imgDescription = imageFileRequest.imgDescription;
            imageFile.imgPath = imageFileRequest.imgPath;
            imageFile.sectionId = imageFileRequest.sectionId;
            _imageFileRepo.Update(imageFile);
            _imageFileRepo.SaveChanges();
            return true;

        }

    }
}
