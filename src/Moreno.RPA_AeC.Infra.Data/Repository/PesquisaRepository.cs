using Moreno.RPA_AeC.Domain.Entities;
using Moreno.RPA_AeC.Domain.Interfaces;
using Moreno.RPA_AeC.Infra.Data.Context.Entity;
using Moreno.RPA_AeC.Infra.Data.Repository.Base;

namespace Moreno.RPA_AeC.Infra.Data.Repository;

[ExcludeFromCodeCoverage]
public class PesquisaRepository : BaseRepository<Pesquisa>, IPesquisaRepository
{
    public PesquisaRepository(RPA_AeCDbContext db) : base(db)
    {
    }
}
