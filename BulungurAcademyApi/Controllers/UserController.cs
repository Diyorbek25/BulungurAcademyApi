﻿using BulungurAcademy.Application.DataTranferObjects.Users;
using BulungurAcademy.Application.Services.Users;
using BulungurAcademy.Domain.Entities.Subjects;
using Microsoft.AspNetCore.Mvc;

namespace BulungurAcademy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(
        IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost]
    public async ValueTask<ActionResult<UserDto>> PostUserAsync(
           UserForCreaterDto userForCreationDto)
    {
        var createdUser = await this.userService
            .CreateUserAsync(userForCreationDto);

        return Created("", createdUser);
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = this.userService
            .RetrieveUsers();

        return Ok(users);
    }


    [HttpGet("{userId:guid}")]
    public async ValueTask<ActionResult<UserDto>> GetUserByIdAsync(
            Guid userId)
    {
        var user = await this.userService
            .RetrieveUserByIdAsync(userId);

        return Ok(user);
    }


    [HttpDelete("{userId:guid}")]
    public async ValueTask<ActionResult<Subject>> DeleteUserAsync(Guid userId)
    {
        var removed = await this.userService
                .RemoveUserAsync(userId);

        return Ok(removed);
    }

}
