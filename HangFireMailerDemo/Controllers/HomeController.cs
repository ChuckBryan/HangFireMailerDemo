using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HangFireMailerDemo.Controllers
{
    using Models;

    public class HomeController : Controller
    {

        private readonly MailerDbContext _db = new MailerDbContext();

        public ActionResult Index()
        {
            var comments = _db.Comments.OrderBy(x => x.Id).ToList();

            return View(comments);
        }

        [HttpPost]
        public ActionResult Create(Comment model)
        {
            if (ModelState.IsValid)
            {
                _db.Comments.Add(model);
                _db.SaveChanges();


                var email = new NewCommentEmail
                {
                    To = "cbryan@marathonus.com",
                    UserName = model.UserName,
                    Comment = model.Text
                };

                email.Send();
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