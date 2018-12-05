using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Together.Notice.Models
{
    public class NoticeRecord
    {
        public NoticeRecord()
        {
            SendingTime = DateTime.Now;
        }

        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 接收者
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendingTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Required()]
        [DefaultValue((int)NotificationStatus.Unknown)]
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        //public string Title { get; set; }
        //public string Content { get; set; }
    }
}
