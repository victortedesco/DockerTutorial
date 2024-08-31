using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Users.API.Extensions;
using Users.API.Requests;
using Users.API.ViewModels;
using Users.Infrastructure.DTO;
using Users.Infrastructure.Services.Interfaces;

namespace Users.API.Controllers;

[Route("api/users")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<UserViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();

        if (!users.Any())
            return NoContent();

        return Ok(users.ToViewModel());
    }

    [HttpGet("id/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userService.GetById(id);

        if (user is null)
            return NotFound();

        return Ok(user.ToViewModel());
    }

    [HttpGet("name/{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<UserViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByName(string name)
    {
        var users = await _userService.GetByName(name);

        if (!users.Any())
            return NoContent();

        return Ok(users.ToViewModel());
    }

    [HttpGet("email/{email}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _userService.GetByEmail(email);

        if (user is null)
            return NotFound();

        return Ok(user.ToViewModel());
    }

    [HttpGet("document/{document:minlength(11):maxlength(14)}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByDocument(string document)
    {
        var user = await _userService.GetByDocument(document);

        if (user is null)
            return NotFound();

        return Ok(user.ToViewModel());
    }

    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> Add(AddUserRequest request)
    {
        var newUser = new UserDTO(Guid.Empty, request.Name, request.Email, request.Document);

        var result = await _userService.Add(newUser);

        if (result.IsFailed)
            return BadRequest(result.Errors.Select(e => e.Message));

        var createdUser = result.Value;

        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser.ToViewModel());
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(Guid id, UpdateUserRequest request)
    {
        var newUser = new UserDTO(Guid.Empty, request.Name, request.Email, string.Empty);

        var result = await _userService.Update(id, newUser);

        if (result.IsFailed)
            return BadRequest(result.Errors.Select(e => e.Message));

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isDeleted = await _userService.Delete(id);

        if (!isDeleted)
            return NotFound();

        return Ok();
    }
}
