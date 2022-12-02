using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using WebUI.Models;

namespace WebUI.Services
{
    public class PublisherService : IPublisherService
    {
        string apiUrl = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"].ToString();

        public List<Publisher> GetAll()
        {
            List<Publisher> objResult = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + $"Publisher");
            request.Method = "GET";
            request.ContentType = "application/json";

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream);

                string response = streamReader.ReadToEnd();
                streamReader.Close();

                if (!string.IsNullOrEmpty(response))
                    objResult = JsonConvert.DeserializeObject<List<Publisher>>(response);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objResult;
        }

        public Publisher GetById(long id)
        {
            Publisher objResult = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + $"Publisher/{id}");
            request.Method = "GET";
            request.ContentType = "application/json";

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream);

                string response = streamReader.ReadToEnd();
                streamReader.Close();

                if (!string.IsNullOrEmpty(response))
                    objResult = JsonConvert.DeserializeObject<Publisher>(response);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objResult;
        }
    }
}