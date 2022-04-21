using SnippetApi.Data.Context;
using SnippetApi.Data.Repository.Generic;
using SnippetApi.Data.Repository.Interfaces;
using SnippetApi.Models;

namespace SnippetApi.Data.Repository.Repositories
{
    public class GroupRepo : GenericRepo<Group>, IGroupRepo
    {
        private AppDbContext AppDbContext => DbContext as AppDbContext;

        public GroupRepo(AppDbContext appDbContext) : base(appDbContext)
        {
                
        }

        public IEnumerable<Group> GetAll()
        {
            return AppDbContext.Groups;
        }
    }
}
