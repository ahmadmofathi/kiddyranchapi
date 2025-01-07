using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public class ImageRepo : IImageRepo
    {
        private readonly AppDbContext _context;

        public ImageRepo(AppDbContext context)
        {
            _context = context;
        }
        public List<ImageFile> GetImages()
        {
            return _context.Set<ImageFile>().ToList();
        }
        public ImageFile? GetById(string id)
        {
            return _context.Set<ImageFile>().Find(id);
        }
        public void Add(ImageFile image)
        {
            _context.Set<ImageFile>().Add(image);
        }

        public void Delete(ImageFile image)
        {
            _context.Set<ImageFile>().Remove(image);
        }
        public void Update(ImageFile image)
        {
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
