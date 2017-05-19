using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IPaintingsRepository
    {
        List<Painting> GetAll();
        Painting Get(int Id);
        int Create(Painting Painting);
        Painting Update(Painting Painting);
        void Delete(int Id);
    }
}
