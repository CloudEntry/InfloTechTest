using System;
using System.Linq;
using System.Text;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [BindProperty]
    public User newUser { get; set; } = default!;

    [HttpGet("/list")]
    public ViewResult List()
    {
        var items = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive,
            DateOfBirth = p.DateOfBirth
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpGet("/filterbyactive/{isActive}")]
    public ViewResult FilterByActive(bool isActive)
    {
        var items = _userService.FilterByActive(isActive).Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive,
            DateOfBirth = p.DateOfBirth
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpGet("/adduser")]
    public ViewResult AddUser()
    {
        return View();
    }

    [HttpPost("/adduser")]
    public ActionResult UserAdded()
    {
        newUser.IsActive = true;
        _userService.Create(newUser);
        return View();
    }

    [HttpGet("/user/{userId}")]
    public ViewResult ViewUser(int userId)
    {
        var user = _userService.GetUser(userId).Select(x => new UserListItemViewModel{
            Id = x.Id,
            Forename = x.Forename,
            Surname = x.Surname,
            Email = x.Email,
            IsActive = x.IsActive,
            DateOfBirth = x.DateOfBirth
        }).First();

        var logs = _userService.GetLogsForUser(userId).Select(x => new LogListItemViewModel{
            LogId = x.LogId,
            UserId = x.UserId,
            Info = x.Info,
            TimeStamp = x.TimeStamp
        });

        user.Logs = logs.ToList();

        return View(user);
    }

    [HttpGet("/deleteuser/{userId}")]
    public ViewResult DeleteUser(int userId)
    {
        var user = _userService.GetUser(userId).First();

        return View(user);
    }

    [HttpPost("/deleteuser/{userId}")]
    public ActionResult UserDeleted(int userId)
    {
        var user = _userService.GetUser(userId).First();
        _userService.Delete(user);

        return View();
    }

    [HttpGet("/edituser/{userId}")]
    public ViewResult EditUser(int userId)
    {
        var user = _userService.GetUser(userId).First();

        return View(user);
    }

    [HttpPost("/edituser/{userId}")]
    public ActionResult UserEdited(int userId)
    {
        var logEntry = new Log(userId, BuildLogEntry(userId), DateTime.Now);
        _userService.CreateLogEntry(logEntry);

        newUser.Id = userId;
        _userService.Update(newUser);

        return View();
    }

    private string BuildLogEntry(int userId) {
        var log = new StringBuilder();

        var oldUser = _userService.GetUser(userId).First();

        if(newUser.Forename != oldUser.Forename) {
            log.AppendLine($"Forename changed from {oldUser.Forename} to {newUser.Forename}");
        }
        if(newUser.Surname != oldUser.Surname) {
            log.AppendLine($"Surname changed from {oldUser.Surname} to {newUser.Surname}");
        }
        if(newUser.Email != oldUser.Email) {
            log.AppendLine($"Email changed from {oldUser.Email} to {newUser.Email}");
        }
        if(newUser.IsActive != oldUser.IsActive) {
            log.AppendLine($"Account Active changed from {(oldUser.IsActive ? "Yes" : "No")} to {(newUser.IsActive ? "Yes" : "No")}");
        }
        if(newUser.DateOfBirth != oldUser.DateOfBirth) {
            log.AppendLine($"Date of Birth changed from {oldUser.DateOfBirth.ToString("dd/MM/yyyy")} to {newUser.DateOfBirth.ToString("dd/MM/yyyy")}");
        }

        return log.ToString();
    }
}
