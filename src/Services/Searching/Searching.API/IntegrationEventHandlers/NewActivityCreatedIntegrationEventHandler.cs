using DotNetCore.CAP;
using Nest;
using Together.Searching.API.Models;

namespace Together.Searching.API.IntegrationEventHandlers
{
    public class NewActivityCreatedIntegrationEventHandler
        : ICapSubscribe
    {
        private readonly ElasticClient _esClient;
        public NewActivityCreatedIntegrationEventHandler(ElasticClient esClient)
        {
            _esClient = esClient;
        }

        [CapSubscribe("Together.Searching.NewActivityCreated")]
        public void CreateActivityDocument(Activity activity)
        {
            _esClient.Index(activity, d => d.Index(IndexName.From<Activity>()));
        }
    }
}
