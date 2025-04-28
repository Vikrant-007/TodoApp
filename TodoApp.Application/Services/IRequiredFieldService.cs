using System.Collections.Generic;

namespace TodoApp.Application.Services
{
    public interface IRequiredFieldService
    {
        List<string> GetFieldsByEntityType(string entityType);
    }
}
