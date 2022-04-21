using SnippetApi.Data.Repository.Generic;
using SnippetApi.Models;

namespace SnippetApi.Data.Repository.Interfaces
{
    public interface IGroupRepo : IGenericRepo<Group>
    {
        IEnumerable<Group> GetAll();
    }
}
