using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public interface ICategoriesService
    {
        Task<List<Category>> GetCategories(int? parentId = null);
    }
}
