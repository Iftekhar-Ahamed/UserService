using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class TblUserChatFriendShipStatus
{
    public long Id { get; set; }

    public int ActionBy { get; set; }

    public short ApproveStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<TblUserChatFriend> TblUserChatFriends { get; set; } = new List<TblUserChatFriend>();
}
