using DomainValidationCore.Validation;
using Moreno.RPA_AeC.Infra.CrossCutting.Filters.Extensions;
using Newtonsoft.Json;

namespace Moreno.RPA_AeC.Application.ViewModels
{
    public class RelatorioPesquisaViewModel
    {
        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy HH:mm:ss")]
        public DateTime DataInicio { get; private set; }
        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy HH:mm:ss")]
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
