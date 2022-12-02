using System.Collections.Generic;
using WebUI.Models;

namespace WebUI.Services
{
    public interface IPublisherService
    {
        List<Publisher> GetAll();
        Publisher GetById(long id);
    }
}