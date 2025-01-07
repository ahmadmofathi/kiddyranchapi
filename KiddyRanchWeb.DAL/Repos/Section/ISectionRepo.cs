using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public interface ISectionRepo
    {
        List<Section> GetSections();
        Section? GetById(string id);
        void Add(Section section);
        void Update(Section section);
        void Delete(Section section);
        int SaveChanges();
    }
}
