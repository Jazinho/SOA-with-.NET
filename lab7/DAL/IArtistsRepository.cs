using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IArtistsRepository
    {
        List<Artist> GetAll();
        Artist Get(int Id);
        int Create(Artist Artist);
        Artist Update(Artist Artist);
        void Delete(int Id);
    }
}
