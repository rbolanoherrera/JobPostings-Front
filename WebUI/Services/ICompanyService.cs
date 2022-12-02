using WebUI.Models;

namespace WebUI.Services
{
    internal interface ICompanyService
    {
        Company GetById(long id);
    }
}
