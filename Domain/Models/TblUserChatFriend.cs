using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class TblUserChatFriend
{
    public long Id { get; set; }

    public int UserId { get; set; }

    public int FriendId { get; set; }

    public short ApproveStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
