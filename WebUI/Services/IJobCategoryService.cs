using System.Collections.Generic;
using WebUI.Models;

namespace WebUI.Services
{
    public interface IJobCategoryService
    {
        List<JobCategory> GetAll();
    }
}