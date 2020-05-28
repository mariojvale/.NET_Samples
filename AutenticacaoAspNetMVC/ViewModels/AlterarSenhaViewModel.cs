using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutenticacaoAspNet.ViewModels
{
    public class AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "Informe a senha atual!")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        [MinLength(6, ErrorMessage ="A senha deve ter pelo menos 6 caracteres")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage ="Informe a nova senha!")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        [MinLength(6, ErrorMessage ="A senha deve ter pelo menos 6 caracteres")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirme a nova senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmacao de Senha")]
        [MinLength(6, ErrorMessage =" A senha deve conter pelo menos 6 caracteres")]
        [Compare(nameof(NovaSenha), ErrorMessage ="A senha e a confirmacao nao estao iguais!")]
        public string ConfirmacaoSenha { get; set; }

    }
}