using SnippetApi.Data.Context;
using SnippetApi.Data.Repository.Interfaces;

namespace SnippetApi.Data.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext DbContext { get; }    

        public UnitOfWork(AppDbContext dbContext, IGroupRepo groupRepo, ICommandRepo commandRepo)
        {
            DbContext = dbContext;
            GroupRepo = groupRepo;
            CommandRepo = commandRepo;
        }

        public IGroupRepo GroupRepo { get; private set; }
        public ICommandRepo CommandRepo { get; private set; }

        public async Task SaveChanges()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
