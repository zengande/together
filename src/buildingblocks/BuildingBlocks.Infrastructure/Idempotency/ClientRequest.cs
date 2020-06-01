using System;
using System.Collections.Generic;
using System.Text;

namespace Together.BuildingBlocks.Infrastructure.Idempotency
{
    public class ClientRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }
}
