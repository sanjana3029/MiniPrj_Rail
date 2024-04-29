using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_MVC_Q2.Models.Repository
{
    public interface IMovieRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
        void Insert(T movie);
        void Update(T movie);
        void Delete(int Id);
        void Save();
    }
}

