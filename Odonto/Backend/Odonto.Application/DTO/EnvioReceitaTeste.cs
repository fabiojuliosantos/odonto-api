namespace Odonto.API.DTOs.Documentos;

public class EnvioEmail
{
    public string Assunto { get; set; }
    public string ConteudoEmail { get; set; }
    public string EmailDestinatario { get; set; }
    public byte[] Anexo { get; set; }
}