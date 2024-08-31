using FluentResults;
using System.Text.RegularExpressions;
using Users.Domain.Models;
using Users.Domain.Repositories;
using Users.Infrastructure.DTO;
using Users.Infrastructure.Extensions;
using Users.Infrastructure.Services.Interfaces;

namespace Users.Infrastructure.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<IEnumerable<UserDTO>> GetAll()
    {
        var users = await _userRepository.GetAll();

        return users.ToDTO();
    }

    public async Task<UserDTO?> GetById(Guid id)
    {
        var user = await _userRepository.GetById(id);

        return user?.ToDTO();
    }

    public async Task<IEnumerable<UserDTO>> GetByName(string name)
    {
        var users = await _userRepository.GetByName(name);

        return users.ToDTO();
    }

    public async Task<UserDTO?> GetByEmail(string email)
    {
        var user = await _userRepository.GetByEmail(email);

        return user?.ToDTO();
    }

    public async Task<UserDTO?> GetByDocument(string document)
    {
        var user = await _userRepository.GetByDocument(document);

        return user?.ToDTO();
    }

    public async Task<Result<UserDTO>> Add(UserDTO request)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(request.Name))
            errors.Add(new Error("Name must not be empty or whitespace."));

        if (request.Name.Length > 32)
            errors.Add(new Error("Name length must not have more than 32."));

        if (string.IsNullOrWhiteSpace(request.Email))
            errors.Add(new Error("Email must not be empty or whitespace."));

        if (request.Email.Length > 50 && !string.IsNullOrWhiteSpace(request.Email))
            errors.Add(new Error("Email length must not have more than 50."));

        if (!Regex.IsMatch(request.Email, "^\\S+@\\S+\\.\\S+$") && request.Email.Length < 50 && !string.IsNullOrWhiteSpace(request.Email))
            errors.Add(new Error("Email provided is invalid."));

        if (request.Document.Length != 11 && request.Document.Length != 14)
            errors.Add(new Error("Document length must be 11 or 14."));

        if (await GetByEmail(request.Email) is not null)
            errors.Add(new Error("This email is beign used by another user."));

        if (await GetByDocument(request.Document) is not null)
            errors.Add(new Error("This document is beign used by another user."));

        if (errors.Count != 0)
            return Result.Fail(errors);

        var newUser = new User(request.Name, request.Email, request.Document);

        var createdUser = await _userRepository.Add(newUser);

        return Result.Ok(createdUser!.ToDTO());
    }

    public async Task<Result> Update(Guid id, UserDTO request)
    {
        var errors = new List<Error>();
        var currentUser = await GetById(id);

        if (currentUser is null)
            return Result.Fail("This user does not exists.");

        if (string.IsNullOrWhiteSpace(request.Name))
            errors.Add(new Error("Name must not be empty or whitespace."));

        if (request.Name.Length > 32)
            errors.Add(new Error("Name length must not have more than 32."));

        if (string.IsNullOrWhiteSpace(request.Email))
            errors.Add(new Error("Email must not be empty or whitespace."));

        if (request.Email.Length > 50 && !string.IsNullOrWhiteSpace(request.Email))
            errors.Add(new Error("Email length must not have more than 50."));

        if (!Regex.IsMatch(request.Email, "^\\S+@\\S+\\.\\S+$") && request.Email.Length < 50 && !string.IsNullOrWhiteSpace(request.Email))
            errors.Add(new Error("Email provided is invalid."));

        if (await GetByEmail(request.Email) is not null && !currentUser.Email.Equals(request.Email, StringComparison.CurrentCultureIgnoreCase))
            errors.Add(new Error("This email is beign used by another user."));

        if (errors.Count != 0)
            return Result.Fail(errors);

        var userRequest = new User(request.Name, request.Email, string.Empty);

        await _userRepository.Update(id, userRequest);

        return Result.Ok();
    }

    public async Task<bool> Delete(Guid id)
    {
        return await _userRepository.Delete(id);
    }
}
