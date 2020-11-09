using System.Collections.Generic;
using TesteBackEndWebMotors.Library.Model;

namespace TesteBackEndWebMotors.Library.Repository
{
    public interface IAnuncioRepository
    {
        int Incluir(AnuncioModel anuncio);
        void Atualizar(AnuncioModel anuncio);
        void Remover(int anuncioId);
        List<AnuncioModel> Consultar();
        bool Existe(int anuncioId);
    }
}
