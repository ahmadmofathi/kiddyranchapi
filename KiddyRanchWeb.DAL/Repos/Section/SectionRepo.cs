using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public class SectionRepo : ISectionRepo
    {
        private readonly AppDbContext _context;

        public SectionRepo(AppDbContext context)
        {
            _context = context;
        }
        public List<Section> GetSections()
        {
            return _context.Set<Section>().ToList();
        }
        public Section? GetById(string id)
        {
            return _context.Set<Section>().Find(id);
        }
        public void Add(Section section)
        {
            _context.Set<Section>().Add(section);
        }

        public void Delete(Section section)
        {
            _context.Set<Section>().Remove(section);
        }
        public void Update(Section section)
        {
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
