using Domain.Entities;
using Domain.Interface.Repositories;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class SessionNoteRepository : GenericRepository<SessionNote, Guid>, ISessionNoteRepository
{
    public SessionNoteRepository(PsicoContext db) : base(db)
    {
    }
}