using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace GoogleCalendarTest.Web.Controllers
{
    public class CalendarController : Controller
    {
        public ActionResult Index()
        {
            Authorize();
            return View();
        }

        public ActionResult Authorize()
        {
            string keyFilePath = Server.MapPath("App_Data/API/GoogleCalendarAPITest-b17e14135c8d.p12");
            string serviceAccountEmail = "143639822205-pj0h4obsehn32an5ch6c7perf9cshmb6@developer.gserviceaccount.com";
            string[] scopes = new string[] 
            {
 	            CalendarService.Scope.Calendar, // Manage your calendars
 	            CalendarService.Scope.CalendarReadonly // View your Calendars
            };

            var certificate = new X509Certificate2(keyFilePath, "notasecret", X509KeyStorageFlags.Exportable);

            ServiceAccountCredential credential = new ServiceAccountCredential(
            new ServiceAccountCredential.Initializer(serviceAccountEmail)
            {
                Scopes = scopes
            }.FromCertificate(certificate));

            CalendarService service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleCalendarAPITest",
            });


            var t = 10;
            return RedirectToAction("Index");
        }

    }
}
