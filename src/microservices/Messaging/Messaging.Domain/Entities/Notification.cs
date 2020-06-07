using System;
using System.Collections.Generic;
using System.Text;
using Together.BuildingBlocks.Domain;

namespace Together.Messaging.Domain.Entities
{
    public class Notification : Entity, IAggregateRoot
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; private set; }
        /// <summary>
        /// 未读
        /// </summary>
        public bool Unread { get; private set; }
        /// <summary>
        /// 通知时间
        /// </summary>
        public DateTime NoticeTimeUtc { get; private set; }
        /// <summary>
        /// 接收者
        /// </summary>
        public string Receiver { get; private set; }
        /// <summary>
        /// 发送者id
        /// </summary>
        public string SenderId { get; private set; }
        /// <summary>
        /// 发送者名称
        /// </summary>
        public string SenderName { get; private set; }

        protected Notification()
        {
            NoticeTimeUtc = DateTime.UtcNow;
        }
        public Notification(string content, string receiver, string senderId, string senderName, bool unread = true) : this()
        {
            Content = content;
            Receiver = receiver;
            SenderId = senderId;
            SenderName = senderName;
            Unread = unread;
        }

        public void Read()
        {
            Unread = false;
        }
    }
}
