using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Topic.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Text { get; set; }
        public int? ParentId { get; set; }
        public int Sort { get; set; }

        public static IEnumerable<Category> MockList() => new List<Category>
        {
            new Category{ Id=1, Key="music", ParentId=null, Sort=1, Text="音乐"},
            new Category{ Id=2, Key="music", ParentId=null, Sort=2, Text="戏剧"},
            new Category{ Id=3, Key="music", ParentId=null, Sort=3, Text="讲座"},
            new Category{ Id=4, Key="music", ParentId=null, Sort=4, Text="聚会"},
            new Category{ Id=5, Key="music", ParentId=null, Sort=5, Text="电影"},
            new Category{ Id=6, Key="music", ParentId=null, Sort=6, Text="运动"},
            new Category{ Id=7, Key="music", ParentId=null, Sort=7, Text="公益"},
            new Category{ Id=8, Key="music", ParentId=null, Sort=8, Text="旅行"},
            new Category{ Id=9, Key="music", ParentId=null, Sort=9, Text="赛事"},
            new Category{ Id=10, Key="music", ParentId=null, Sort=10, Text="课程"},
            new Category{ Id=11, Key="music", ParentId=null, Sort=11, Text="亲子"},
            new Category{ Id=12, Key="music", ParentId=null, Sort=12, Text="其他"},
            new Category{ Id=1001, Key="3d-artists", ParentId=1, Sort=1, Text="小型演唱会"},
            new Category{ Id=1002, Key="3d-artists", ParentId=1, Sort=2, Text="音乐会"},
            new Category{ Id=1003, Key="3d-artists", ParentId=1, Sort=3, Text="演唱会"},
            new Category{ Id=1004, Key="3d-artists", ParentId=1, Sort=4, Text="音乐节"},
            new Category{ Id=2001, Key="3d-artists", ParentId=2, Sort=1, Text="话剧"},
            new Category{ Id=2002, Key="3d-artists", ParentId=2, Sort=2, Text="音乐剧"},
            new Category{ Id=2003, Key="3d-artists", ParentId=2, Sort=3, Text="舞剧"},
        };

    }
}
