﻿using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    IEnumerable<User> FilterByActive(bool isActive);
    IEnumerable<User> GetUser(int id);
    IEnumerable<User> GetAll();
    void Create(User user);
    void Delete(User user);
    void Update(User user);
    IEnumerable<Log> GetLogsForUser(int userId);

    void CreateLogEntry(Log logEntry);
}
