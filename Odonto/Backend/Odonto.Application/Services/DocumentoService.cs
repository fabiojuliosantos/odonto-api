using System.Net;
using System.Net.Mail;
using System.Text;
using MediatR;
using Odonto.API.DTOs.Documentos;
using Odonto.Application.Enum;
using Odonto.Application.Interfaces;
using Odonto.Application.Mediator.Dentistas.Queries;
using Odonto.Application.Mediator.Documentos.Commands;
using Odonto.Application.Mediator.Pacientes.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using static Odonto.Application.Services.Util.DocumentosUtil;

namespace Odonto.Application.Services;

public class DocumentoService : IDocumentosService
{
    private readonly IPacienteService _service;
    private readonly IDentistaService _dentistasService;
    private readonly IDocumentosRepository _documentosRepository;
    private readonly IMediator _mediator;

    public DocumentoService(IPacienteService service,
        IDentistaService dentistasService,
        IDocumentosRepository documentosRepository,
        IMediator mediator)
    {
        _service = service;
        _dentistasService = dentistasService;
        _documentosRepository = documentosRepository;
        _mediator = mediator;
    }

    Image marcaDagua =
        LoadImageWithTransparency(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Img", "logo-peb.png"),
            0.25f);

    string logo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Img", "logo.png");

    public async Task<byte[]> GerarAtestado(Atestado atestado)
    {
        BuscarPacientePorIdCommand query = new BuscarPacientePorIdCommand() { PacienteId = atestado.pacienteId };
        BuscarDentistasEmailQuery dentistaQuery = new BuscarDentistasEmailQuery() { DentistaEmail = atestado.Usuario };

        int mesAtual = DateTime.Now.Month;
        MesesEnum mesEnum = (MesesEnum)mesAtual;
        int anoAtual = DateTime.Now.Year;

        Paciente infoPaciente = new Paciente();
        Dentista infoDentista = new Dentista();

        infoPaciente = await _service.BuscarPacientePorIdAsync(query);
        infoDentista = await _dentistasService.BuscarDentistaEmail(dentistaQuery);

        Document atestadoPdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(45);
                page.Size(PageSizes.A4);

                page.Content()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("Centro de Odontologia Especializada").FontSize(18).Bold();
                        text.Span("\n");
                        text.Span("Rua X, nº XX");
                        text.Span("\n");
                        text.Span("Recife, Pernambuco");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("Atestado Odontológico").FontSize(14).Bold();
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("Atesto para os devidos fins, que o(a) paciente ").FontSize(14);
                        text.Span($"{infoPaciente.Nome}, portador(a) do CPF {infoPaciente.Cpf}, ").Bold();
                        text.Span(
                            $"foi atendido(a) em consulta odontológica nesta data, e encontra-se inapto(a) para exercer suas atividades laborativas por um período de {atestado.quantidadeDias} dias.");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span($"CID10: {atestado.cid10}");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span($"{infoDentista.Nome}");
                        text.Span("\n");
                        text.Span($"CRO: {infoDentista.Cro}");
                        text.Span("\n");
                        text.Span("\n");
                        text.Span($"Emitido em: {DateTime.Now.ToString("dd/MM/yyyy")}");
                    });

                page.Background()
                    .AlignCenter()
                    .AlignMiddle()
                    .Image(marcaDagua);

                page.Footer()
                    .AlignCenter()
                    .Text(text => { text.Span($"Recife/PE"); });
            });
        });

        byte[] pdfBytes = atestadoPdf.GeneratePdf();
        string diretorio = $"C:\\Documentos\\Atestados\\{anoAtual}\\{mesEnum}";
        string caminho = Path.Combine(diretorio,
            $"{infoPaciente.Nome}_{DateTime.Now:ddMMyyyy}_{DateTime.Now:HHmmss}.pdf");

        CadastrarDocumentosCommand atestadoDocumento = new CadastrarDocumentosCommand()
        {
            DataEmissao = DateTime.Now,
            DentistaId = infoDentista.DentistaId,
            PacienteId = infoPaciente.PacienteId,
            TipoDocumento = "Atestado",
            NomeArquivo = caminho
        };

        await _mediator.Send(atestadoDocumento);

        // Cria o diretório, se ele não existir
        Directory.CreateDirectory(diretorio);

        // Salva o arquivo PDF
        await File.WriteAllBytesAsync(caminho, pdfBytes);

        var conteudoEmail = string.Empty;
        
        StringBuilder body = new StringBuilder();
        body.Append($"<h1 style='color: #01a3a4;'>Olá, {infoPaciente.Nome}!<h1>");
        body.Append("<h2 style='color: #222f3e;'>Por favor, não responda esse e-mail.</h2>");
        body.Append($@"<p style='color': #222f3e;>Segue em anexo seu atestado referente à consulta do dia {DateTime.Now:dd/MM/yyyy}</p>");
        body.Append("<p style='color': #222f3e>Atenciosamente: </p>");
        body.Append("<p style='color': #222f3e>Clínica Especializada em Odontologia</p>");
        conteudoEmail = body.ToString();

        var atestadoEmail = new EnvioEmail()
        {
            Anexo = pdfBytes,
            Assunto = "Atestado",
            ConteudoEmail = conteudoEmail,
            EmailDestinatario = infoPaciente.Email
        };
        
        await EnviarEmail(atestadoEmail);
        
        return pdfBytes;
    }

    public async Task<byte[]> GerarReceita(Receita receita)
    {
        try
        {
            BuscarPacientePorIdCommand query = new BuscarPacientePorIdCommand() { PacienteId = receita.PacienteId };
            BuscarDentistasEmailQuery dentistaQuery = new BuscarDentistasEmailQuery()
                { DentistaEmail = receita.Usuario };

            var infoPaciente = new Paciente();
            var infoDentista = new Dentista();

            var mesAtual = DateTime.Now.Month;
            var mesEnum = (MesesEnum)mesAtual;
            var anoAtual = DateTime.Now.Year;

            infoPaciente = await _service.BuscarPacientePorIdAsync(query);
            infoDentista = await _dentistasService.BuscarDentistaEmail(dentistaQuery);

            var receitaPdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(45);
                    page.Size(PageSizes.A4);

                    page.Header()
                        .Height(250)
                        .AlignCenter()
                        .Row(row =>
                        {
                            row.RelativeColumn(4)
                                .AlignCenter()
                                .Text(text =>
                                {
                                    text.Span("\n");
                                    text.Span("\n");
                                    text.Span("\n");
                                    text.Span("Centro de Odontologia Especializada").FontSize(18).Bold();
                                    text.Span("\n");
                                    text.Span("Rua X, nº XX");
                                    text.Span("\n");
                                    text.Span("Recife, Pernambuco");
                                    text.Span("\n");
                                    text.Span("\n");
                                    text.Span("Receituário").FontSize(14).Bold();
                                });
                        });

                    page.Content()
                        .AlignLeft()
                        .Text(text =>
                        {
                            foreach (var medicamento in receita.Medicamentos)
                            {
                                text.Span("\n");
                                text.Span(
                                    $"{medicamento.Nome} {medicamento.Dose}________________{medicamento.Posologia}");
                                text.Span("\n");
                                text.Span($"{medicamento.Observacoes}");
                                text.Span("\n");
                            }

                            text.Span("\n");
                            text.Span($"{infoDentista.Nome}");
                            text.Span("\n");
                            text.Span($"CRO: {infoDentista.Cro}");
                            text.Span("\n");
                            text.Span($"Emitido em: {DateTime.Now.ToString("dd/MM/yyyy")}");
                        });

                    page.Background()
                        .AlignCenter()
                        .AlignMiddle()
                        .Image(marcaDagua);

                    page.Footer()
                        .AlignCenter()
                        .Text(text => { text.Span($"Recife/PE"); });
                });
            });
            byte[] pdfBytes = receitaPdf.GeneratePdf();

            var diretorio = $"C:\\Documentos\\Receitas\\{anoAtual}\\{mesEnum}";
            var caminho = Path.Combine(diretorio,
                $"{infoPaciente.Nome}_{DateTime.Now:ddMMyyyy}_{DateTime.Now:HHmmss}.pdf");

            var atestadoDocumento = new CadastrarDocumentosCommand()
            {
                DataEmissao = DateTime.Now,
                DentistaId = infoDentista.DentistaId,
                PacienteId = infoPaciente.PacienteId,
                TipoDocumento = "Receita",
                NomeArquivo = caminho
            };
            
            var conteudoEmail = string.Empty;
            
            StringBuilder body = new StringBuilder();
            body.Append($"<h1 style='color: #01a3a4;'>Olá, {infoPaciente.Nome}!<h1>");
            body.Append("<h2 style='color: #222f3e;'>Por favor, não responda esse e-mail.</h2>");
            body.Append($@"<p style='color': #222f3e;>Segue em anexo sua receita referente à consulta do dia {DateTime.Now:dd/MM/yyyy}</p>");
            body.Append("<p style='color': #222f3e>Atenciosamente: </p>");
            body.Append("<p style='color': #222f3e>Clínica Especializada em Odontologia</p>");
            conteudoEmail = body.ToString();
            
            EnvioEmail receitaObjeto = new EnvioEmail();
            receitaObjeto.Assunto = "Receita";
            receitaObjeto.EmailDestinatario = infoPaciente.Email;
            receitaObjeto.Anexo = pdfBytes;
            receitaObjeto.ConteudoEmail = conteudoEmail;
            await EnviarEmail(receitaObjeto);

            await _mediator.Send(atestadoDocumento);

            Directory.CreateDirectory(diretorio);

            await File.WriteAllBytesAsync(caminho, pdfBytes);

            return pdfBytes;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task EnviarEmail(EnvioEmail email)
    {
        try
        {
            MailMessage mail = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("clinicaespecializadaodonto@gmail.com", "yhcqkqjcnpwfjexb");
            mail.From = new MailAddress("clinicaespecializadaodonto@gmail.com","Clínica Especializada em Odontologia");
            mail.Body = email.ConteudoEmail;
            mail.Subject = email.Assunto;
            mail.IsBodyHtml = true;

            mail.To.Add(email.EmailDestinatario);
            
            using (MemoryStream memoryStream = new MemoryStream(email.Anexo))
            {
                Attachment anexoEnviado = new Attachment(memoryStream, "Anexo", "application/pdf");
                mail.Attachments.Add(anexoEnviado);
                smtpClient.Send(mail);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao enviar e-mail", ex);
        }
    }
}