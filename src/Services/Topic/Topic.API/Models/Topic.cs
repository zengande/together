using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Topic.API.Models
{
    public class Topic
    {
        public string Id { get; set; }
        public string TopicName { get; set; }
        public string ParentId { get; set; }
        public int Sort { get; set; }

        public static IEnumerable<Topic> MockList() => new List<Topic>
        {
            new Topic{ Id="arts-culture", ParentId=null, Sort=1, TopicName="艺术与文化"},
            new Topic{ Id="3d-artists", ParentId="arts-culture", Sort=1, TopicName="3D艺术"}
        };

    }
}
