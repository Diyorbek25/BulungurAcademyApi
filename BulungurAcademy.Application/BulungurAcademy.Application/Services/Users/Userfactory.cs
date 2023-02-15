﻿using BulungurAcademy.Application.DataTranferObjects.Users;
using BulungurAcademy.Application.Services.ExamApplicants;
using BulungurAcademy.Domain.Entities.Users;

namespace BulungurAcademy.Application.Services.Users;

public class Userfactory : IUserFactory
{
    private readonly IExamApplicantFatory fatory;

    public Userfactory(IExamApplicantFatory fatory)
    {
        this.fatory = fatory;
    }

    public User MapToUser(UserForCreaterDto userForCreationDto)
    {
        return new User(
            userForCreationDto.firstName,
            userForCreationDto.lastName,
            userForCreationDto.phoneNumber,
            Domain.Enum.UserRole.User,
            userForCreationDto.telegramId);
    }

    public void MapToUser(User storageUser, UserForModificationDto userForModificationDto)
    {
        storageUser.FirstName = userForModificationDto.firstName ?? storageUser.FirstName;
        storageUser.LastName = userForModificationDto.lastName ?? storageUser.LastName;
        storageUser.Phone = userForModificationDto.phoneNumber ?? storageUser.Phone;
    }

    public UserDto MapToUserDto(User user)
    {
        return new UserDto(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Phone,
            user.UserRole,
            user.ExamApplicants.Select(eas => fatory.MapToExamApplicantDto(eas))
            );
    }
}
