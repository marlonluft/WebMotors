using System.ComponentModel.DataAnnotations;
using TesteBackEndWebMotors.Library.Model;

namespace TesteBackEndWebMotors.ViewModel
{
    public class AnuncioViewModel
    {
        public AnuncioViewModel()
        {

        }

        public AnuncioViewModel(AnuncioModel model)
        {
            Id = model.Id;
            Marca = model.Marca;
            Modelo = model.Modelo;
            Versao = model.Versao;
            Ano = model.Ano;
            Quilometragem = model.Quilometragem;
            Observacao = model.Observacao;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'Marca' é obrigatório")]
        [StringLength(45, MinimumLength = 4, ErrorMessage = "O campo 'Marca' deve ser preenchido entre 4 a 45 caracteres")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O campo 'Modelo' é obrigatório")]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "O campo 'Modelo' deve ser preenchido entre 3 a 45 caracteres")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O campo 'Versão' é obrigatório")]
        [StringLength(45, ErrorMessage = "O campo 'Versão' não deve ultrapassar 45 caracteres")]
        public string Versao { get; set; }

        [Required(ErrorMessage = "O campo 'Ano' é obrigatório")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "O campo 'Quilometragem' é obrigatório")]
        public int Quilometragem { get; set; }

        public string Observacao { get; set; }

        public AnuncioModel ToModel()
        {
            return new AnuncioModel(Id, Marca, Modelo, Versao, Ano, Quilometragem, Observacao);
        }
    }
}
