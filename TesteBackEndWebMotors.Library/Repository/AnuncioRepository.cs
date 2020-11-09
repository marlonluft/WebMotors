using Dapper;
using System.Collections.Generic;
using System.Linq;
using TesteBackEndWebMotors.Library.CustomException;
using TesteBackEndWebMotors.Library.Model;

namespace TesteBackEndWebMotors.Library.Repository
{
    public class AnuncioRepository : IAnuncioRepository
    {
        private readonly IDbContext _dbContext;
        public AnuncioRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Atualizar(AnuncioModel anuncio)
        {
            using (var connection = _dbContext.Get())
            {
                var parametros = new Dictionary<string, object>
                {
                    { "@Ano", anuncio.Ano },
                    { "@Id", anuncio.Id },
                    { "@Marca", anuncio.Marca },
                    { "@Modelo", anuncio.Modelo },
                    { "@Observacao", anuncio.Observacao ?? string.Empty },
                    { "@Quilometragem", anuncio.Quilometragem },
                    { "@Versao", anuncio.Versao },
                };

                var linhas = connection.Execute(@"
                UPDATE 
                    teste_webmotors..tb_AnuncioWebmotors 
                SET
                    Ano = @Ano
                    , Marca = @Marca
                    , Modelo = @Modelo
                    , Observacao = @Observacao
                    , Quilometragem = @Quilometragem
                    , Versao = @Versao
                WHERE
                    Id = @Id", parametros);

                if (linhas == 0)
                {
                    throw new NossaException("Não foi possível atualizar o anúncio");
                }
            }
        }

        public List<AnuncioModel> Consultar()
        {
            using (var connection = _dbContext.Get())
            {
                return connection.Query<AnuncioModel>(@"
                SELECT
                    Id
                    , Ano
                    , Marca
                    , Modelo
                    , Observacao
                    , Quilometragem
                    , Versao
                FROM 
                    teste_webmotors..tb_AnuncioWebmotors").ToList();
            }
        }

        public bool Existe(int anuncioId)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@Id", anuncioId },
            };

            using (var connection = _dbContext.Get())
            {
                return connection.ExecuteScalar<int?>(@"
                SELECT
                    TOP 1 Id
                FROM 
                    teste_webmotors..tb_AnuncioWebmotors
                WHERE 
                    Id = @Id", parametros) > 0;
            }
        }

        public int Incluir(AnuncioModel anuncio)
        {
            using (var connection = _dbContext.Get())
            {
                var parametros = new Dictionary<string, object>
                {
                    { "@Ano", anuncio.Ano },
                    { "@Marca", anuncio.Marca },
                    { "@Modelo", anuncio.Modelo },
                    { "@Observacao", anuncio.Observacao ?? string.Empty },
                    { "@Quilometragem", anuncio.Quilometragem },
                    { "@Versao", anuncio.Versao },
                };

                var id = connection.ExecuteScalar<int?>(@"
                INSERT INTO teste_webmotors..tb_AnuncioWebmotors ( 
                    Ano
                    , Marca
                    , Modelo
                    , Observacao
                    , Quilometragem
                    , Versao
                ) VALUES (
                    @Ano
                    , @Marca
                    , @Modelo
                    , @Observacao
                    , @Quilometragem
                    , @Versao
                );
                SELECT SCOPE_IDENTITY();", parametros);

                if (!id.HasValue || id == 0)
                {
                    throw new NossaException("Não foi possível incluir o anúncio");
                }

                return id.Value;
            }
        }

        public void Remover(int anuncioId)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@Id", anuncioId },
            };

            using (var connection = _dbContext.Get())
            {
                var linhas = connection.Execute(@"
                DELETE 
                    teste_webmotors..tb_AnuncioWebmotors
                WHERE 
                    Id = @Id", parametros);

                if (linhas == 0)
                {
                    throw new NossaException("Anúncio não encontrado para remoção");
                }
            }
        }
    }
}
