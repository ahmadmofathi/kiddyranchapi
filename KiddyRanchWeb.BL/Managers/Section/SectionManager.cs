using KiddyRanchWeb.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class SectionManager : ISectionManager
    {
        private readonly ISectionRepo _sectionRepo;
        private readonly IImageFileManager _imageFileManager;

        public SectionManager(ISectionRepo sectionRepo, IImageFileManager imageFileManager)
        {
            _sectionRepo = sectionRepo;
            _imageFileManager = imageFileManager;
        }
        public IEnumerable<SectionWithImagesDTO> GetSections()
        {
            IEnumerable<Section> sectionsDB = _sectionRepo.GetSections();
            return sectionsDB.Select(u => new SectionWithImagesDTO
            {
                sectionId = u.sectionId,
                sectionDescription = u.sectionDescription,
                sectionSubtitle = u.sectionSubtitle,
                sectionTitle = u.sectionTitle,
                styleCode = u.styleCode,
                images = _imageFileManager.GetSectionImages(u.sectionId),
            });
        }
        public string Add(SectionAddDTO u)
        {
            Section section = new Section
            {
                sectionId = Guid.NewGuid().ToString(),
                sectionTitle = u.sectionTitle,
                sectionSubtitle = u.sectionSubtitle,
                sectionDescription = u.sectionDescription,
                styleCode = u.styleCode,
            };
            _sectionRepo.Add(section);
            _sectionRepo.SaveChanges();
            return section.sectionId;
        }

        public bool Delete(string id)
        {
            Section? section = _sectionRepo.GetById(id);
            if (section == null)
            {
                return false;
            }
            _sectionRepo.Delete(section);
            _sectionRepo.SaveChanges();
            return true;
        }

        public SectionWithImagesDTO GetSection(string id)
        {
            Section? section = _sectionRepo.GetById(id);
            if (section is null)
            {
                return null;
            }

            return new SectionWithImagesDTO
            {
                sectionId = id,
                sectionTitle = section.sectionTitle,
                sectionDescription = section.sectionDescription,
                sectionSubtitle = section.sectionSubtitle,
                images=_imageFileManager.GetSectionImages(section.sectionId),
            };
        }
        public Section GetSectionById(string id)
        {
            Section? section = _sectionRepo.GetById(id) as Section;
            if (section == null)
            {
                return null;
            }
            return section;
        }

        public bool Update(SectionUpdateDTO sectionRequest)
        {
            if (sectionRequest.sectionId == null)
            {
                return false;
            }
            Section? section = _sectionRepo.GetById(sectionRequest.sectionId);
            if (section is null)
            {
                return false;
            }
            section.sectionDescription = sectionRequest.sectionDescription;
            section.sectionTitle = sectionRequest.sectionTitle;
            section.sectionSubtitle = sectionRequest.sectionSubtitle;
            section.styleCode = sectionRequest.styleCode;
            _sectionRepo.Update(section);
            _sectionRepo.SaveChanges();
            return true;

        }


    }
}
