using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Domain.AggregatesModel.CatalogAggregate
{
    public class Category : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public int Order { get; set; }
        public string Image { get; set; }

        protected Category()
        {

        }
        public Category(string name, string image, int? order = 0)
        {
            Name = name;
            Image = image;
            Order = order ?? 0;
        }
    }
}
