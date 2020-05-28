using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutenticacaoAspNet.ViewModels
{
    public class CadastroUsuarioViewModel
    {
        [Required(ErrorMessage = "Informe seu Nome")]
        [MaxLength(100, ErrorMessage ="O nome deve ter ate 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Informe seu Login")]
        [MaxLength(50, ErrorMessage ="O login deve ter ate 50 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage ="Informe sua senha")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="A senha deve ter no minimo 6 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage ="Confirme sua senha!")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirmar Senha")]
        [MinLength(6,ErrorMessage = "A senha deve ter no minimo 6 caracteres")]
        [Compare(nameof(Senha),ErrorMessage ="A senha nao confere com a confirmacao")]
        public string ConfirmacaoSenha { get; set; }
    }
}