using Microsoft.AspNetCore.Mvc;
using Nest;
using Together.Searching.API.Models;

namespace Together.Searching.API.Controllers
{
    [Route("api/v1/search")]
    [ApiController]
    public class SearchController
        : Controller
    {
        private readonly ElasticClient _elasticClient;
        public SearchController(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        [HttpGet]
        [Route("activities")]
        public IActionResult Activities(string keyword, int offset = 0, int limit = 10)
        {
            var activities = _elasticClient.Search<Activity>(s =>
                s.Index<Activity>()
                    .From(offset)
                    .Size(limit)
                    .Query(q => q.Match(m => m.Field(f => f.Detail).Query(keyword)) ||
                        q.Match(m => m.Field(f => f.Title).Query(keyword))
                    )
                    .Sort(d => d.Descending(SortSpecialField.Score))
            //.Sort(d => d.Descending(f => f.CreateTime))
            //.Highlight(h => h.PreTags("<b style='color:red'>")
            //    .PostTags("</b>")
            //    .Fields(f1 => f1.Field(e => e.Title), f2 => f2.Field(e => e.Detail)))
            ).Documents;
            return Ok(activities);
        }

        [HttpPost]
        [Route("activities")]
        public IActionResult Activities([FromBody]Activity activity)
        {
            if (_elasticClient.IndexExists(Indices.Index(IndexName.From<Activity>())).Exists == false)
            {
                _elasticClient.CreateIndex(IndexName.From<Activity>());
            }
            var response = _elasticClient.IndexDocument(activity);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            _elasticClient.DeleteByQuery<Activity>(s => s.Query(d => d.Match(m => m.Field(f => f.Id).Query(id.ToString()))));
            return Ok();
        }
    }

}
