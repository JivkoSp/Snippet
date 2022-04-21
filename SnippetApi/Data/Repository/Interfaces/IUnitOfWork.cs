namespace SnippetApi.Data.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IGroupRepo GroupRepo { get; }
        ICommandRepo CommandRepo { get; }
        Task SaveChanges();
    }
}
