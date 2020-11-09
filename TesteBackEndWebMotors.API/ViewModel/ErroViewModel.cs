using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace TesteBackEndWebMotors.ViewModel
{
    public class ErroViewModel
    {
        private ErroViewModel()
        {
            Erros = new List<string>();
        }

        public ErroViewModel(string mensagem) : this()
        {
            Erros.Add(mensagem);
        }

        public ErroViewModel(ModelStateDictionary modelStateDictionary) : this()
        {
            foreach (var modelState in modelStateDictionary.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    Erros.Add(error.ErrorMessage);
                }
            }
        }

        public List<string> Erros { get; set; }
    }
}
