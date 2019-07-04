using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels;

namespace WebMVC.Infrastructure.API
{
    public interface ICategoryAPI
    {
        [Get("/api/categories?parentId={parentId}")]
        Task<List<Category>> GetCategories(int? parentId = null);
    }
}
