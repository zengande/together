using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Activity.Application.Elasticsearch.Models;

namespace Together.Activity.Application.Elasticsearch
{
    public class ActivityIndexService : IIndexService
    {
        private readonly ILogger<ActivityIndexService> _logger;
        private readonly IElasticClient _elasticClient;
        public ActivityIndexService(IConfiguration configuration,
            ILogger<ActivityIndexService> logger)
        {
            _logger = logger;

            var uri = configuration.GetValue<string>("ElasticSearch:Endpoint");
            var settings = new ConnectionSettings(new Uri(uri))
                .DefaultIndex("activity");
            _elasticClient = new ElasticClient(settings);
        }

        public bool CreateIndex(ActivityIndex index)
        {

            var response = _elasticClient.CreateDocument(index);
            var result = response.IsValid;
            if (result)
            {
                _logger.LogInformation($"活动 {index.Title} 索引创建成功");
            }

            return result;
        }

        public async Task<ISearchResponse<ActivityIndex>> Search(string query)
        {
            var response = await _elasticClient.SearchAsync<ActivityIndex>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(a => a.Title)
                        .Field(a => a.Content)
                        .Query(query)
                    )
                )
                .Highlight(h => h
                    .PreTags("<highlight>")
                    .PostTags("</highlight>")
                    .Encoder(HighlighterEncoder.Html)
                    .Fields(
                        fs => fs
                            .Field(a => a.Title)
                            .Type(HighlighterType.Plain)
                            .FragmentSize(150)
                            .Fragmenter(HighlighterFragmenter.Span)
                            .NoMatchSize(150),
                        fs => fs
                            .Field(a => a.Content)
                            .Type(HighlighterType.Plain)
                            .FragmentSize(150)
                            .Fragmenter(HighlighterFragmenter.Span)
                            .NoMatchSize(150)
                    )
                )
            );
            return response;
        }
    }
}
