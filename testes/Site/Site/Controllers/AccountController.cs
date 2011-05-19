using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Xml;
using System.Web.Security;

namespace Site.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigurationManager.AppSettings["AuthXML"]);

            try
            {
                var x = doc.SelectNodes("//user[@login='" + Request.Form["username"] +
                                        "' and password/text() = '" + Request.Form["password"] +
                                        "']/name/text()");

                if (x.Count == 0)
                {
                    ViewBag.Message = "Usuário ou senha inválidos";
                    return RedirectToAction("Index");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(Request.Form["username"], false);
                    Session["Nome"] = x.Item(0).Value;
                    return RedirectToAction("Details");
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "Usuário ou senha inválidos";
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        public ActionResult Details()
        {
            ViewBag.Usuario = Session["Nome"];
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Cotacao(string numcot)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigurationManager.AppSettings["AprovCotXML"]);

            var n = doc.SelectNodes("//cotacao[@id='" + numcot + "' and usuarios/usuario/text() = '" + User.Identity.Name + "']");
            if(n.Count > 0)
                return View();
            else
                return RedirectToAction("Details");
        }
    }
}
