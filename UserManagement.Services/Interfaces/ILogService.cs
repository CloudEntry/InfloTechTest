using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface ILogService
{
    // IEnumerable<User> FilterByActive(bool isActive);
    IEnumerable<Log> GetLog(int id);
    IEnumerable<Log> GetAll();
    void Create(Log log);
    void Delete(Log log);
    void Update(Log log);
    IEnumerable<Log> FilterLogs(string criteria);
}
