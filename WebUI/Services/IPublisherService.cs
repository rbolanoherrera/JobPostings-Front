using WebUI.Models;

namespace WebUI.Services
{
    internal interface IPublisherService
    {
        Publisher GetById(long id);
    }
}