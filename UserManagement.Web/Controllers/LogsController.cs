﻿using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("logs")]
public class LogsController : Controller
{
    private readonly ILogService _logService;
    private readonly IUserService _userService;

    public LogsController(ILogService logService, IUserService userService) {
        _logService = logService;
        _userService = userService;
    }

    [HttpGet("/listlogs")]
    public ViewResult ListLogs()
    {
        var items = _logService.GetAll().Select(p => new LogListItemViewModel
        {
            LogId = p.LogId,
            UserId = p.UserId,
            Info = p.Info,
            Details = p.Details,
            TimeStamp = p.TimeStamp
        });

        var model = new LogListViewModel{
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpGet("/logentry/{logId}")]
    public ViewResult ViewLog(int logId)
    {
        var logEntry = _logService.GetLog(logId).Select(x => new LogListItemViewModel{
            LogId = x.LogId,
            UserId = x.UserId,
            Info = x.Info,
            Details = x.Details,
            TimeStamp = x.TimeStamp
        }).First();

        var userList = _userService.GetUser((int)logEntry.UserId).Select(x => new UserListItemViewModel{
            Id = x.Id,
            Forename = x.Forename,
            Surname = x.Surname,
            Email = x.Email,
            IsActive = x.IsActive,
            DateOfBirth = x.DateOfBirth
        });

        if (userList.Any()) {
            logEntry.User = userList.First();
        }

        return View(logEntry);
    }

    [HttpGet("/filterlogs/{criteria}")]
    public ViewResult FilterLogs(string criteria)
    {
        var items = _logService.FilterLogs(criteria).Select(p => new LogListItemViewModel
        {
            LogId = p.LogId,
            UserId = p.UserId,
            Info = p.Info,
            Details = p.Details,
            TimeStamp = p.TimeStamp
        });

        var model = new LogListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }
}
