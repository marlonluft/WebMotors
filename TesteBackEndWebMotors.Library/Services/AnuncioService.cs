using System.Collections.Generic;
using TesteBackEndWebMotors.Library.CustomException;
using TesteBackEndWebMotors.Library.Model;
using TesteBackEndWebMotors.Library.Repository;

namespace TesteBackEndWebMotors.Library.Services
{
    public class AnuncioService : IAnuncioService
    {
        private readonly IAnuncioRepository _anuncioRepository;

        public AnuncioService(IAnuncioRepository anuncioRepository)
        {
            _anuncioRepository = anuncioRepository;
        }

        public int Incluir(AnuncioModel anuncio)
        {
            if (anuncio == null)
            {
                throw new NossaException("Dados do anúncio estão nulos");
            }

            anuncio.Validar();

            var anuncioId = _anuncioRepository.Incluir(anuncio);

            return anuncioId;
        }

        public void Atualizar(AnuncioModel anuncio)
        {
            if (anuncio == null)
            {
                throw new NossaException("Dados do anúncio estão nulos");
            }

            anuncio.Validar();

            if (!_anuncioRepository.Existe(anuncio.Id))
            {
                throw new NossaException("Anúncio não encontrado");
            }

            _anuncioRepository.Atualizar(anuncio);
        }

        public void Remover(int anuncioId)
        {
            if (!_anuncioRepository.Existe(anuncioId))
            {
                throw new NossaException("Anúncio não encontrado");
            }

            _anuncioRepository.Remover(anuncioId);
        }

        public List<AnuncioModel> Consultar()
        {
            return _anuncioRepository.Consultar();
        }
    }
}
