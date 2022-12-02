using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using WebUI.Models;

namespace WebUI.Services
{
    public class JobService : IJobService
    {
        string apiUrl = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"].ToString();

        public Job GetById(long id)
        {
            Job objResult = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + $"Job/{id}");
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
                    objResult = JsonConvert.DeserializeObject<Job>(response);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objResult;
        }
    }
}