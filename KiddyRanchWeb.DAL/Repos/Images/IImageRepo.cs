using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public interface IImageRepo
    {
        List<ImageFile> GetImages();
        ImageFile? GetById(string id);
        void Add(ImageFile image);
        void Update(ImageFile image);
        void Delete(ImageFile image);
        int SaveChanges();
    }
}
