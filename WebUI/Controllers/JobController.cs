using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Controllers
{
    public class JobController : Controller
    {
        string apiUrl = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"].ToString();

        IPublisherService publisherService = null;
        IJobCategoryService categoryService = null;
        IJobService jobService = null;

        // GET: Job
        public ActionResult Index()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + "Job");
            request.Method = "GET";
            request.ContentType = "application/json";

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream);
                string response = streamReader.ReadToEnd();

                ViewBag.ListJobs = JsonConvert.DeserializeObject<List<Job>>(response);

                streamReader.Close();
            }
            catch (Exception ex)
            {
                ViewBag.ListJobs = null;
                throw ex;
            }

            return View();
        }

        public ActionResult Add()
        {
            Job model = new Job();
            model.Category = new JobCategory();
            model.Publisher = new Publisher();

            GetDropDownJobs();

            return View(model);
        }

        private void GetDropDownJobs()
        {
            if (publisherService == null)
                publisherService = new PublisherService();

            if (categoryService == null)
                categoryService = new JobCategoryService();

            ViewBag.listCategory = categoryService.GetAll();
            ViewBag.listPublisher = publisherService.GetAll();
        }

        [HttpPost]
        public ActionResult Add(Job model)
        {
            GetDropDownJobs();

            if (ModelState.IsValid)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + "Job");
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
                        return RedirectToAction("Index", "Job");

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
            Job model = null;

            GetDropDownJobs();

            if (id != 0)
            {
                if (jobService == null)
                    jobService = new JobService();

                model = jobService.GetById(id);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Job model)
        {
            GetDropDownJobs();

            if (ModelState.IsValid)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + $"Job/{model.Id}");
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

                    return RedirectToAction("Index", "Job");

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