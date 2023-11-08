using System.Linq;
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
        var user = _userService.GetUser(userId).First();

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
        newUser.Id = userId;
        _userService.Update(newUser);

        return View();
    }
}
