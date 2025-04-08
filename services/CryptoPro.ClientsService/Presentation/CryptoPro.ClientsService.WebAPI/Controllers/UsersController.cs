using CryptoPro.ClientsService.Application.Users.Commands.CreateUser;
using CryptoPro.ClientsService.Application.Users.Commands.DeleteUser;
using CryptoPro.ClientsService.Application.Users.Commands.UpdateUser;
using CryptoPro.ClientsService.Application.Users.Queries.GetAllUsers;
using CryptoPro.ClientsService.Application.Users.Queries.GetUserById;
using CryptoPro.ClientsService.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPro.ClientsService.WebAPI.Controllers;

[ApiController]
[Authorize] 
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllUsers()
    {
        var user = await _mediator.Send(new GetAllUsersQuery());

        return Ok(user);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserReadDto>> GetUser(int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));

        return Ok(user);
    }
    
    [HttpPost]
    public async Task<ActionResult<bool>> CreateUser(UserCreateDto user)
    {
        var isSuccessful = await _mediator.Send(new CreateUserCommand(user));

        return Ok(isSuccessful);
    }
    
    [HttpPost("update")]
    public async Task<ActionResult<bool>> UpdateUser(UserUpdateDto user)
    {
        var isSuccessful = await _mediator.Send(new UpdateUserCommand(user));

        return Ok(isSuccessful);
    }
    
    [HttpDelete("{userId:int}")]
    public async Task<ActionResult<bool>> DeleteUser(int userId)
    {
        var isSuccessful = await _mediator.Send(new DeleteUserCommand(userId));

        return Ok(isSuccessful);
    }
}