using Together.Activity.Domain.SeedWork;

namespace Together.Activity.Domain.AggregatesModel.CategoryAggregate
{
    public class Category
        : Entity, IAggregateRoot
    {
        public string Key { get; private set; }
        public string Text { get; private set; }
        public int? ParentId { get; private set; }
        public int Sort { get; private set; }
        public bool Enabled { get; private set; }
        public string CoverImage { get; private set; }

        public Category(string key, string text, int? parentId, int sort, string coverImage, bool enabled = true)
        {
            Key = key;
            Text = text;
            ParentId = parentId;
            Sort = sort;
            Enabled = enabled;
            CoverImage = coverImage;
        }

        public void ChangeDisabled()
        {
            Enabled = false;
        }

        public void ChangeEnabled()
        {
            Enabled = true;
        }

        public void SetSort(int sort)
        {
            Sort = sort;
        }

    }
}
