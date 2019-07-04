using System;
using System.Web;

namespace Together.Activity.API.Applications.IntegrationEvents.Events
{
    public class NewActivityCreatedIntegrationEvent
    {
        public NewActivityCreatedIntegrationEvent(int id, string title, string detail, DateTime createTime)
        {
            Id = id;
            Title = HttpUtility.HtmlEncode(title);
            Detail = HttpUtility.HtmlEncode(detail);
            CreateTime = createTime;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
