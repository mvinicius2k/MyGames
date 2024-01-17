using Microsoft.Extensions.Logging;

namespace Api;

public class DbInit
{
    private readonly Context _context;
    private readonly ILogger<DbInit> _logger;

    public DbInit(Context context, ILoggerFactory factory)
    {
        _context = context;
        _logger = factory.CreateLogger<DbInit>(); ;
    }

    public void Prepare(bool restart = false)
    {
        try
        {

            if (restart)
            {
#if DEBUG
                var result = _context.Database.EnsureDeleted();
                _logger.LogInformation(result ? "Banco de dados deletado" : "Banco de dados inexistente, nada para deletar");
#else
                _logger.LogInformation("Não é permitido resetar o DB fora de depuração");
#endif

            }
            _context.Database.EnsureCreated();

        }
        catch (OperationCanceledException e)
        {
            _logger.LogError(e, "Erro ao preparar DB");
            throw;
        }


    }

    public void Seed()
    {
        _logger.LogInformation("Seed feito");
    }
}
