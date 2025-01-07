using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public interface ISectionManager
    {
        IEnumerable<SectionWithImagesDTO> GetSections();
        SectionWithImagesDTO GetSection(string id);
        string Add(SectionAddDTO section);
        bool Update(SectionUpdateDTO section);
        bool Delete(string id);
    }
}
