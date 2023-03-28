using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AppLibreria
{
    public class ConexionBd
    {
        private readonly IConfiguration _configuration;
        private readonly string? _stringConnection;

        public ConexionBd(IConfiguration configuration)
        {
            _configuration = configuration;
            _stringConnection = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection ConectarBd() => new SqlConnection(_stringConnection);
    }
}