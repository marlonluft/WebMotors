using System.Collections.Generic;
using TesteBackEndWebMotors.Library.Model;

namespace TesteBackEndWebMotors.Library.Services
{
    public interface IAnuncioService
    {
        int Incluir(AnuncioModel anuncio);
        void Atualizar(AnuncioModel anuncio);
        void Remover(int anuncioId);
        List<AnuncioModel> Consultar();
    }
}
