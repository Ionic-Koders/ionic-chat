﻿namespace ionic_chat.Infrastructure.Models.Default.Response
{
    public class ContactResponse
    {
        public string FriendId { get; set; }
        public string UserId { get; set; }
        public virtual UserResponse User { get; set; }
        public virtual UserResponse Friend { get; set; }
    }
}