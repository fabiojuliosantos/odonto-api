using MediatR;
using Odonto.API.DTOs.Documentos;
using Odonto.Application.Documentos;
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

    Image marcaDagua = LoadImageWithTransparency(@"C:\odonto-api\Odonto\Backend\Odonto.Application\Assets\Img\logo-peb.png", 0.25f);
    string logo = @"C:\odonto-api\Odonto\Backend\Odonto.Application\Assets\Img\logo.png";

    public async Task<byte[]> GerarAtestado(AtestadoDTO atestado, string email)
    {
        BuscarPacientePorIdCommand query = new BuscarPacientePorIdCommand() { PacienteId = atestado.pacienteId };
        BuscarDentistasEmailQuery dentistaQuery = new BuscarDentistasEmailQuery() { DentistaEmail = email };

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
                    text.Span($"foi atendido(a) em consulta odontológica nesta data, e encontra-se inapto(a) para exercer suas atividades laborativas por um período de {atestado.quantidadeDias} dias.");
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
                .Text(text =>
                {
                    text.Span($"Recife/PE");
                });

            });
        });

        byte[] pdfBytes = atestadoPdf.GeneratePdf();
        string diretorio = $"C:\\Documentos\\Atestados\\{anoAtual}\\{mesEnum}";
        string caminho = Path.Combine(diretorio, $"{infoPaciente.Nome}_{DateTime.Now:ddMMyyyy}_{DateTime.Now:HHmmss}.pdf");

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

        return pdfBytes;
    }

    public async Task<byte[]> GerarReceita(ReceitaDTO receita, string email)
    {
        try
        {
            BuscarPacientePorIdCommand query = new BuscarPacientePorIdCommand() { PacienteId = receita.PacienteId };
            BuscarDentistasEmailQuery dentistaQuery = new BuscarDentistasEmailQuery() { DentistaEmail = email };

            Paciente infoPaciente = new Paciente();
            Dentista infoDentista = new Dentista();

            int mesAtual = DateTime.Now.Month;
            MesesEnum mesEnum = (MesesEnum)mesAtual;
            int anoAtual = DateTime.Now.Year;

            infoPaciente = await _service.BuscarPacientePorIdAsync(query);
            infoDentista = await _dentistasService.BuscarDentistaEmail(dentistaQuery);

            Document receitaPdf = Document.Create(container =>
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
                            text.Span($"{medicamento.Nome} {medicamento.Dose}____________{medicamento.Posologia}");
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
                    .Text(text =>
                    {
                        text.Span($"Recife/PE");
                    });
                });
            });
            byte[] pdfBytes = receitaPdf.GeneratePdf();

            string diretorio = $"C:\\Documentos\\Receitas\\{anoAtual}\\{mesEnum}";
            string caminho = Path.Combine(diretorio, $"{infoPaciente.Nome}_{DateTime.Now:ddMMyyyy}_{DateTime.Now:HHmmss}.pdf");

            CadastrarDocumentosCommand atestadoDocumento = new CadastrarDocumentosCommand()
            {
                DataEmissao = DateTime.Now,
                DentistaId = infoDentista.DentistaId,
                PacienteId = infoPaciente.PacienteId,
                TipoDocumento = "Receita",
                NomeArquivo = caminho
            };

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
}
