using Chat.Core.DTOs.UserChatFriendDTOs;
using FluentValidation;

namespace Chat.Core.Validations.ChatFriendManageValidations;

public class AddNewChatFriendRequestValidator: AbstractValidator<AddNewChatFriendRequestDto>
{
    public AddNewChatFriendRequestValidator()
    {
        RuleFor(x => x.SelfUserId)
            .GreaterThan(0).WithMessage("SelfUserId Must be greater than 0.");
        RuleFor(x => x.RequestedUserId)
            .GreaterThan(0).WithMessage("RequestedUserId Must be greater than 0.");
    }
}