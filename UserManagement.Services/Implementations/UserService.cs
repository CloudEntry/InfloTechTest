﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public IEnumerable<User> FilterByActive(bool isActive) => _dataAccess.GetAll<User>().Where(u => u.IsActive == isActive);

    public IEnumerable<User> GetUser(int id) => _dataAccess.GetAll<User>().AsNoTracking().Where(u => u.Id == id);

    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();

    public void Create(User user) => _dataAccess.Create(user);

    public void Delete(User user) => _dataAccess.Delete(user);
    public void Update(User user) => _dataAccess.Update(user);
    public IEnumerable<Log> GetLogsForUser(int userId) => _dataAccess.GetAll<Log>().Where(u => u.UserId == userId);
    public void CreateLogEntry(Log logEntry) => _dataAccess.Create(logEntry);
}
