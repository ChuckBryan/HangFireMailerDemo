using System.Linq;
using System.Web.Mvc;

namespace HangFireMailerDemo.Controllers
{
    using System.IO;
    using System.Web.Hosting;
    using Hangfire;
    using Models;
    using Postal;

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
            BackgroundJob.Enqueue(() => NotifyNewComment(model.Id));

            return RedirectToAction("Index");
        }

        public static void NotifyNewComment(int quoteId)
        {
            // REMEMBER: THIS WILL BE RUNNING IN THE HANGFIRE SERVER...NOT ASP.NET
            // Prepare Postal Classes to work outside of ASP.NET
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));

            var emailService = new EmailService(engines);

            // Get Comment and send out a notification
            using (var db = new MailerDbContext())
            {
                var savedQuote = db.MovieQuotes.Find(quoteId);
                var email = new NewQuoteEmail
                {
                    To = "admin@moviequotedb.com",
                    CharacterName = savedQuote.CharacterName,
                    Movie = savedQuote.Movie,
                    Quote = savedQuote.Quote
                };

                emailService.Send(email);
            }
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