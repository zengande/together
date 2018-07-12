using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Together.UserGroup.API.Infrastructure.Models
{
    public class Group
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 组名
        /// </summary>
        public string GroupName { get; set; }
        
        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        
        /// <summary>
        /// 创始人
        /// </summary>
        public string Founder { get; set; }

        public virtual ICollection<User> Members { get; set; }
    }
}
