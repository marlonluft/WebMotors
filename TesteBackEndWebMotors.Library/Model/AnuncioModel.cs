using System;
using TesteBackEndWebMotors.Library.CustomException;

namespace TesteBackEndWebMotors.Library.Model
{
    public class AnuncioModel
    {
        public AnuncioModel()
        {

        }

        public AnuncioModel(int id, string marca, string modelo, string versao, int ano, int quilometragem, string observacao)
        {
            Id = id;
            Marca = marca;
            Modelo = modelo;
            Versao = versao;
            Ano = ano;
            Quilometragem = quilometragem;
            Observacao = observacao;
        }
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Versao { get; set; }
        public int Ano { get; set; }
        public int Quilometragem { get; set; }
        public string Observacao { get; set; }

        internal void Validar()
        {
            if (Id < 0)
            {
                throw new NossaException("A identificação do anúncio é inválida");
            }

            if (string.IsNullOrWhiteSpace(Marca))
            {
                throw new NossaException("A marca do veículo deve ser informada");
            }

            if (string.IsNullOrWhiteSpace(Modelo))
            {
                throw new NossaException("O modelo do veículo deve ser informado");
            }

            if (string.IsNullOrWhiteSpace(Versao))
            {
                throw new NossaException("A versão do veículo deve ser informado");
            }

            if (Ano < 1886 || Ano > DateTime.Now.Year + 1)
            {
                throw new NossaException("O ano informado do veículo é inválido");
            }

            if (Quilometragem < 0)
            {
                throw new NossaException("A quilometragem do veículo é inválida");
            }
        }
    }
}
