using DetailsProject.Data;
using Microsoft.AspNetCore.Mvc;
using DetailsProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DetailsProject.Controllers
{
    [Authorize]
    public class UserDetailsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserDetailsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userDetails = _db.UserDetails.Include(u => u.User).FirstOrDefault(u => u.UserId == claim.Value);

            return View(userDetails);
         
        }

        //GET
        public IActionResult Create()
        {

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserDetail obj)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            obj.UserId = claim.Value;

            _db.UserDetails.Add(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Delete(string? userId)
        {
            if (!_db.UserDetails.Any(x => x.UserId == userId))
            {
                return NotFound();
            }

            var user = _db.UserDetails.FirstOrDefault(u => u.UserId == userId);

            return View(user);
        }

        //POST
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(Guid id)
        {
            var userDetails = _db.UserDetails.Find(id);

            if (userDetails == null)
            {
                return NotFound();
            }

            _db.UserDetails.Remove(userDetails);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
