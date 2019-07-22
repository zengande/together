using Microsoft.AspNetCore.Mvc;
using Nest;
using Together.Searching.API.Models;
using Together.Searching.API.ViewModels;

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
        public IActionResult Activities(ActivitySearchViewModel vm)
        {
            var activities = _elasticClient.Search<Activity>(s =>
                s.Index<Activity>()
                    .Query(q => (q.Match(m => m.Field(f => f.Detail).Query(vm.Keyword)) ||
                        q.Match(m => m.Field(f => f.Title).Query(vm.Keyword))) &&
                        q.GeoDistance(g => g
                            .Boost(1.1)
                            .Field(f => f.LocationPoint)
                            .Distance(vm.Distance)
                            .Location(vm.Location)
                            .ValidationMethod(GeoValidationMethod.IgnoreMalformed)
                        )
                    )
                    .From(vm.Offset)
                    .Size(vm.Limit)
                    .Sort(d => d.Descending(SortSpecialField.Score))
            .Sort(d => d.Descending(f => f.CreateTime))
            .Highlight(h => h.PreTags("<b style='color:red'>")
                .PostTags("</b>")
                .Fields(f1 => f1.Field(e => e.Title), f2 => f2.Field(e => e.Detail)))
            ).Documents;
            return Ok(activities);
        }

        [HttpPost]
        [Route("activities")]
        public IActionResult Activities([FromBody]Activity activity)
        {
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
