using System.Linq;
using System.Web.Mvc;

namespace HangFireMailerDemo.Controllers
{
    using Models;

    public class HomeController : Controller
    {

        private readonly MailerDbContext _db = new MailerDbContext();

        public ActionResult Index()
        {
            var comments = _db.MovieQuotes.OrderBy(x => x.Id).ToList();

            return View(comments);
        }

        [HttpPost]
        public ActionResult Create(MovieQuote model)
        {
            // MODEL STATE VALIDATION
            if (!ModelState.IsValid) return RedirectToAction("Index");

            // SAVE THE MODEL
            _db.MovieQuotes.Add(model);
            _db.SaveChanges();

            // SEND THE EMAIL
            var email = new NewQuoteEmail
            {
                To = "admin@moviequotedb.com",
                CharacterName = model.CharacterName,
                Movie = model.Movie,
                Quote = model.Quote
            };

            email.Send();

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