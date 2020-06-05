using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Domain.AggregatesModel.CatalogAggregate
{
    public class Catalog : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public int Order { get; set; }
        public int? ParentId { get; set; }
        public IReadOnlyCollection<Catalog> Children => _children?.AsReadOnly();
        private List<Catalog> _children;

        protected Catalog()
        {
            _children = new List<Catalog>();
        }
        public Catalog(string name, int? order = 0, int? parentId = null)
        {
            Name = name;
            Order = order ?? 0;
            ParentId = parentId;
        }

        public void AddChild(string name, int? order = null)
        {
            if (order.HasValue == false)
            {
                order = _children?.Max(c => c.Order) ?? 1;
            }

            var catalog = new Catalog(name, order, Id);
            _children.Add(catalog);
        }
    }
}
