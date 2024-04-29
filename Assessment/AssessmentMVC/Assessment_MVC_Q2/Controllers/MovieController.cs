using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assessment_MVC_Q2.Models;
using Assessment_MVC_Q2.Models.Repository;

namespace MVC_CodeFirst.Controllers
{
    public class MoviesController : Controller
    {
        IMovieRepo<Movie> _movRepo = null;

        public MoviesController()
        {
            _movRepo = new MovieRepository<Movie>();
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = _movRepo.GetAll();
            return View(movies);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _movRepo.Insert(movie);
                _movRepo.Save();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public ActionResult Edit(int id)
        {
            var movie = _movRepo.GetById(id);
            return View(movie);
        }

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _movRepo.Update(movie);
                _movRepo.Save();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public ActionResult Details(int id)
        {
            var movie = _movRepo.GetById(id);
            return View(movie);
        }

        public ActionResult Delete(int id)
        {
            var movie = _movRepo.GetById(id);
            return View(movie);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _movRepo.Delete(id);
            _movRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
