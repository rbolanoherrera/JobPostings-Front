using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Controllers
{
    public class CompanyController : Controller
    {
        string apiUrl = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"].ToString();

        private ICompanyService companyService;

        // GET: Company
        public ActionResult Index()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + "Company");
            request.Method = "GET";
            request.ContentType = "application/json";
            //StreamWriter streamWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
            //streamWriter.Write(JsonConvert.SerializeObject(data));
            //streamWriter.Close();

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream);
                string response = streamReader.ReadToEnd();

                ViewBag.ListCompanies = JsonConvert.DeserializeObject<List<Company>>(response);

                streamReader.Close();
            }
            catch (Exception ex)
            {
                ViewBag.ListCompanies = null;
                throw ex;
            }

            return View();
        }

        public ActionResult Add()
        {
            Company model = new Company();
            model.CompanyContact = new CompanyContact();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Company model)
        {
            if (ModelState.IsValid)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + "Company");
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
                        return RedirectToAction("Index", "Company");

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
            Company model = null;

            if (id != 0)
            {
                if (companyService == null)
                    companyService = new CompanyService();

                model = companyService.GetById(id);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Company model)
        {
            if (ModelState.IsValid)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + $"Company/{model.Id}");
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

                    return RedirectToAction("Index", "Company");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("summaryErrors", ex.Message);
                }

            }

            return View(model);
        }

        public ActionResult Delete(long id = 0)
        {
            if (id != 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + $"Company/{id}");
                request.Method = "DELETE";
                request.ContentType = "application/json";
                //StreamWriter streamWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                //streamWriter.Write(JsonConvert.SerializeObject(model));
                //streamWriter.Close();

                try
                {
                    WebResponse webResponse = request.GetResponse();
                    Stream stream = webResponse.GetResponseStream();
                    StreamReader streamReader = new StreamReader(stream);

                    string response = streamReader.ReadToEnd();

                    streamReader.Close();

                    return RedirectToAction("Index", "Company");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("summaryErrors", ex.Message);
                }
            }

            return View();
        }

    }
}