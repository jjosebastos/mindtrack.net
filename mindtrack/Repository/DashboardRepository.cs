using mindtrack.Connection;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace mindtrack.Repository
{
    public interface IDashboardRepository
    {
        Task<string> GetUserJsonAsync(int idUser);
    }

    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetUserJsonAsync(int idUser)
        {
            // Obtém a conexão do Entity Framework
            var connection = _context.Database.GetDbConnection();

            try
            {
                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT mindtrack_pkg.FN_USER_JSON(:id) FROM DUAL";

                    var param = command.CreateParameter();
                    param.ParameterName = "id";
                    param.Value = idUser;
                    command.Parameters.Add(param);
                    var result = await command.ExecuteScalarAsync();

                    return result != null ? result.ToString() : null;
                }
            }
            finally
            {
                // Boas práticas: fechar conexão se foi aberta manualmente, 
                // embora o EF gerencie, em comandos raw é bom garantir.
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }
    }
}
