using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public interface IImageFileManager
    {
        IEnumerable<ImageReadDTO> GetImageFiles();
        List<ImageReadDTO> GetSectionImages(string sectionId);
        ImageReadDTO GetImageFile(string id);
        string Add(ImageAddDTO user);
        bool Update(ImageReadDTO user);
        bool Delete(string id);
    }
}
