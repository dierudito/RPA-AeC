using Moreno.RPA_AeC.Application.ViewModels;

namespace Moreno.RPA_AeC.Application.Interfaces;

public interface IPesquisaAppService : IDisposable
{
    Task<RelatorioPesquisaViewModel> PesquisarTermoAsync(string termo);
}
