using Microsoft.EntityFrameworkCore;
using SnippetApi.Data.Context;
using SnippetApi.Data.Repository.Generic;
using SnippetApi.Data.Repository.Interfaces;
using SnippetApi.Models;

namespace SnippetApi.Data.Repository.Repositories
{
    public class CommandRepo : GenericRepo<Command>, ICommandRepo
    {
        private AppDbContext AppDbContext => DbContext as AppDbContext;

        public CommandRepo(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public IEnumerable<Command> GetAll(int groupId)
        {
            return AppDbContext.Groups.Where(prop => prop.GroupId == groupId)
                .Include(prop => prop.Commands)
                .Select(prop => prop.Commands).FirstOrDefault();
        }

        public async Task<Command> FindByName(string commandName)
        {
            return await AppDbContext.Commands.FirstOrDefaultAsync(prop => prop.Name == commandName);
        }
    }
}
