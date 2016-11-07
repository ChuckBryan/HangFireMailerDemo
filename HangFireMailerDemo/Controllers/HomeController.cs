using System.Linq;
using System.Web.Mvc;

namespace HangFireMailerDemo.Controllers
{
    using System;
    using System.Net.Mail;
    using Models;

    public class HomeController : Controller
    {

        private readonly MailerDbContext _db = new MailerDbContext();

        public ActionResult Index()
        {

            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            var comments = _db.MovieQuotes.OrderBy(x => x.Id).ToList();

            return View(comments);
        }

        [HttpPost]
        public ActionResult Create(MovieQuote model)
        {
            // MODEL STATE VALIDATION
            if (!ModelState.IsValid) return RedirectToAction("Index");

            // Save the Model to the Database using Entity Framework
            _db.MovieQuotes.Add(model);
            _db.SaveChanges();

            // Send the Email Using Postal...
            var email = new NewQuoteEmail
            {
                To = "admin@moviequotedb.com",
                CharacterName = model.CharacterName,
                Movie = model.Movie,
                Quote = model.Quote
            };

            try
            {
                email.Send();
            }
            catch (SmtpException)
            {
                TempData["ErrorMessage"] = "I'm sorry Dave, I'm afraid I can't do that - HAL 9000, 2001: A Space Odyssey";
            }
            

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}