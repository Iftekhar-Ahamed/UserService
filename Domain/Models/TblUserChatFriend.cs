namespace Domain.Models;

public partial class TblUserChatFriend
{
    public long Id { get; set; }

    public long FriendShipStatusId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int UserId { get; set; }

    public virtual TblUserChatFriendShipStatus FriendShipStatus { get; set; } = null!;

    public virtual TblUserInformation User { get; set; } = null!;
}
