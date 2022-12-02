using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using WebUI.Models;

namespace WebUI.Services
{
    public class JobCategoryService : IJobCategoryService
    {
        string apiUrl = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"].ToString();

        public List<JobCategory> GetAll()
        {
            List<JobCategory> objResult = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + $"JobCategory");
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
                    objResult = JsonConvert.DeserializeObject<List<JobCategory>>(response);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objResult;
        }
    }
}