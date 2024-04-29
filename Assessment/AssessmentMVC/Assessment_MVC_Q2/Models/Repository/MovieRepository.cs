using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assessment_MVC_Q2.Controllers;
using System.Data.Entity;
using Assessment_MVC_Q2.Models;

namespace Assessment_MVC_Q2.Models.Repository
{
    public class MovieRepository<T>: IMovieRepo<T> where T:class
    {
        MovieDbContext MV;
        DbSet<T> dbset;

        public MovieRepository()
        {
            MV = new MovieDbContext();
            dbset = MV.Set<T>();
        }

        public void Delete(int Id)
        {
            T movie = dbset.Find(Id);
            if (movie != null)
                dbset.Remove(movie);
        }

        public IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public T GetById(int Id)
        {
            return dbset.Find(Id);
        }

        public void Insert(T movie)
        {
           dbset .Add(movie);
        }

        public void Save()
        {
            MV.SaveChanges();
        }

        public void Update(T movie)
        {
            MV.Entry(movie).State = EntityState.Modified;
        }
    }
}