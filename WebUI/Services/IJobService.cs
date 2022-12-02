using WebUI.Models;

namespace WebUI.Services
{
    public interface IJobService
    {
        Job GetById(long id);
    }
}