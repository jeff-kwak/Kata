using Marten;

namespace Kata.Web.Adapter.Database.Marten;

public sealed partial class Database(IDocumentStore store)
{
    private readonly IDocumentStore _store = store; 
}
