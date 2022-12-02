using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Controllers
{
    public class PublisherController : Controller
    {
        string apiUrl = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"].ToString();
        private IPublisherService publisherService;

        // GET: Publisher
        public ActionResult Index()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + "Publisher");
            request.Method = "GET";
            request.ContentType = "application/json";
            
            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream);
                string response = streamReader.ReadToEnd();

                ViewBag.ListPublishers = JsonConvert.DeserializeObject<List<Publisher>>(response);

                streamReader.Close();
            }
            catch (Exception ex)
            {
                ViewBag.ListPublishers = null;
                throw ex;
            }

            return View();
        }

        public ActionResult Add()
        {
            Publisher model = new Publisher();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Publisher model)
        {
            if (ModelState.IsValid)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + "Publisher");
                request.Method = "POST";
                request.ContentType = "application/json";
                StreamWriter streamWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                streamWriter.Write(JsonConvert.SerializeObject(model));
                streamWriter.Close();

                try
                {
                    WebResponse webResponse = request.GetResponse();
                    Stream stream = webResponse.GetResponseStream();
                    StreamReader streamReader = new StreamReader(stream);

                    string response = streamReader.ReadToEnd();

                    streamReader.Close();

                    int id = 0;

                    if (!string.IsNullOrEmpty(response))
                        id = JsonConvert.DeserializeObject<int>(response);
                    else
                        return RedirectToAction("Index", "Publisher");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("summaryErrors", ex.Message);
                }
            }

            return View();
        }

        public ActionResult Edit(long id = 0)
        {
            Publisher model = null;

            if (id != 0)
            {
                if (publisherService == null)
                    publisherService = new PublisherService();

                model = publisherService.GetById(id);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Company model)
        {
            if (ModelState.IsValid)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + $"Publisher/{model.Id}");
                request.Method = "PUT";
                request.ContentType = "application/json";
                StreamWriter streamWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                streamWriter.Write(JsonConvert.SerializeObject(model));
                streamWriter.Close();

                try
                {
                    WebResponse webResponse = request.GetResponse();
                    Stream stream = webResponse.GetResponseStream();
                    StreamReader streamReader = new StreamReader(stream);

                    string response = streamReader.ReadToEnd();
                    streamReader.Close();

                    return RedirectToAction("Index", "Publisher");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("summaryErrors", ex.Message);
                }

            }

            return View(model);
        }

    }
}