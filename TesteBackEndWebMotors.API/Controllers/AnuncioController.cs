using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using TesteBackEndWebMotors.Library.Services;
using TesteBackEndWebMotors.ViewModel;

namespace TesteBackEndWebMotors.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnuncioController : ControllerBase
    {
        private readonly ILogger<AnuncioController> _logger;
        private readonly IAnuncioService _anuncioService;

        public AnuncioController(
            ILogger<AnuncioController> logger,
            IAnuncioService anuncioService)
        {
            _logger = logger;
            _anuncioService = anuncioService;
        }

        [HttpGet]
        public IActionResult Consultar()
        {
            var listaAnuncios = _anuncioService.Consultar()
                .Select(x => new AnuncioViewModel(x))
                .ToList();

            return Ok(listaAnuncios);
        }

        [HttpPost]
        public IActionResult Incluir(AnuncioViewModel anuncio)
        {
            if (ModelState.IsValid)
            {
                var anuncioId = _anuncioService.Incluir(anuncio.ToModel());

                return Ok(anuncioId);
            }

            return BadRequest(new ErroViewModel(ModelState));
        }

        [HttpPut]
        public IActionResult Atualizar(AnuncioViewModel anuncio)
        {
            if (ModelState.IsValid)
            {
                _anuncioService.Atualizar(anuncio.ToModel());

                return Ok();
            }

            return BadRequest(new ErroViewModel(ModelState));
        }

        [HttpDelete("{anuncioId}")]
        public IActionResult Remover(int anuncioId)
        {
            _anuncioService.Remover(anuncioId);

            return Ok(anuncioId);
        }
    }
}
