using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Text;
using System.Configuration;

namespace AprovVal.Controllers
{
  public class AprovadoresController : Controller
  {
    //
    // GET: /Aprovadores/

    public ActionResult AprovadoresAteh(string valor)
    {
      XmlDocument doc = new XmlDocument();
      doc.Load(ConfigurationManager.AppSettings["aprovval.xml"]);

      var n = doc.SelectNodes("//VALOR[@VALUE >=" + valor + "]/USERS/USER/@NAME");

      var sb = new StringBuilder();

      for (int i = 0; i < n.Count; i++)
      {
        string s = n.Item(i).Value + ';';

        if(!sb.ToString().Contains(s))
          sb.Append(s);
      }

      ViewBag.Usuarios = sb.ToString();

      return View();
    }

  }
}
