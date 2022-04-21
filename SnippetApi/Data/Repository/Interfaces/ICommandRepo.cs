using SnippetApi.Data.Repository.Generic;
using SnippetApi.Models;

namespace SnippetApi.Data.Repository.Interfaces
{
    public interface ICommandRepo : IGenericRepo<Command>
    {
        IEnumerable<Command> GetAll(int groupId);
        Task<Command> FindByName(string commandName);
    }
}
