using System.Collections.Generic;

namespace WebMVC.ViewModels
{
    public class SearchResultBase<T>
    {
        public string Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Totle { get; set; }
        public IList<T> Results { get; set; }
    }
}
