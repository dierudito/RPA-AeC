namespace Moreno.RPA_AeC.Infra.Rpa.Base;

public interface IBaseRpa : IDisposable
{
    Task NavegarParaUrl(string url);
    /// <summary>
    /// Atribuir Valor a um/vários elementos da página
    /// </summary>
    /// <param name="tipoElemento"></param>
    /// <param name="chaveElemento"></param>
    /// <param name="valor"></param>
    /// <param name="indexArray">Index da posição do array onde deverá ser atribuído o valor. Caso não informado, será atribuído em todos</param>
    /// <returns></returns>
    Task<bool> AtribuirValor(TipoElementoEnum tipoElemento, string chaveElemento, string valor, int indexArray=-1);

    /// <summary>
    /// Envia um comando a um/vários elementos da página
    /// </summary>
    /// <param name="tipoElemento"></param>
    /// <param name="chaveElemento"></param>
    /// <param name="valor"></param>
    /// <param name="indexArray">Index da posição do array onde deverá ser enviado o comando. Caso não informado, será atribuído em todos</param>
    /// <returns></returns>
    Task<bool> EnviarComando(TipoElementoEnum tipoElemento, string chaveElemento, TipoComandoEnum tipoComando, int indexArray = -1);

    Task<string> ObterConteudoUnicoDoAtributo(TipoElementoEnum tipoElemento, string chaveElemento, TipoAtributoEnum tipoAtributo);
}
