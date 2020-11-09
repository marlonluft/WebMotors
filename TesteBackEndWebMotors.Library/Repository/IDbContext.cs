using Microsoft.Data.SqlClient;

namespace TesteBackEndWebMotors.Library.Repository
{
    public interface IDbContext
    {
        SqlConnection Get();
    }
}
