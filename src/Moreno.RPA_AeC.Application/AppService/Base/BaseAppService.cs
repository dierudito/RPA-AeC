using DomainValidationCore.Validation;
using Moreno.RPA_AeC.Domain.Interfaces;
using System.Text;

namespace Moreno.RPA_AeC.Application.AppService.Base;

public abstract class BaseAppService
{
    private readonly IUnitOfWork _uow;

    public BaseAppService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    protected void AdicionarErrosValidacao(ValidationResult validationResult, string name, string erro)
    {
        validationResult.Add(new ValidationError(name, erro));
    }

    protected string ObterDetalhesDaException(Exception e)
    {
        var sbException = new StringBuilder();

        sbException.Append("---- Exception ----");
        sbException.Append("Message: " + e.Message);
        sbException.Append("StackTrace: " + e.StackTrace);
        sbException.Append('\n');

        if (e.InnerException != null)
            sbException = ObterInnerException(e.InnerException, sbException);

        return sbException.ToString();
    }

    private StringBuilder ObterInnerException(Exception e, StringBuilder sb)
    {
        if (e.InnerException != null)
            sb = ObterInnerException(e.InnerException, sb);

        sb.Append("---- Inner Exception ----");
        sb.Append("Message: " + e.Message);
        sb.Append("StackTrace: " + e.StackTrace);
        sb.Append('\n');
        return sb;
    }

    protected int Commit() => _uow.Commit();
    protected async Task<int> CommitAsync() => await _uow.CommitAsync().ConfigureAwait(false);
}
