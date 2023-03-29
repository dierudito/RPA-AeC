using DomainValidationCore.Validation;

namespace Moreno.RPA_AeC.Application.ViewModels
{
    public class RelatorioPesquisaViewModel
    {
        public DateTime DataInicio { get; private set; }
        public DateTime DataTermino { get; set; }
        public string TermoPesquisado { get; set; }
        public int QuantidadeRegistrosEncontrados { get; set; }
        public int QuantidadeRegistrosGravadosComSucesso { get; set; }
        public int QuantidadeRegistrosGravadosComRessalvas { get; set; }
        public int QuantidadeRegistrosNaoGravados { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public RelatorioPesquisaViewModel()
        {
            DataInicio = DateTime.Now;
            ValidationResult = new ValidationResult();
        }
    }
}
