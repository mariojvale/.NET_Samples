using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutenticacaoAspNet.ViewModels
{
    public class LoginViewModel
    {
        [HiddenInput]
        public string UrlRetorno { get; set; }

        [Required(ErrorMessage ="Informe seu Login!")]
        [MaxLength(50, ErrorMessage ="O Login deve ter ate 50 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage ="Digite sua senha!")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres")]
        public string Senha { get; set; }
    }
}