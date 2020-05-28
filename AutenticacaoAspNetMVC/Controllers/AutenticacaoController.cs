using AutenticacaoAspNet.Models;
using AutenticacaoAspNet.Utils;
using AutenticacaoAspNet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AutenticacaoAspNet.Controllers
{
    public class AutenticacaoController : Controller
    {

        private UsuariosContext db = new UsuariosContext();

        // GET: Autenticacao
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastroUsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if(db.Usuarios.Count(u => u.Login == viewModel.Login) > 0)
            {
                ModelState.AddModelError("Login", "Estes login ja esta em uso!");
                return View(viewModel);
            }

            Usuario usuario = new Usuario
            {
                Nome = viewModel.Nome,
                Login = viewModel.Login,
                Senha = Hash.GerarHash(viewModel.Senha)
            };

            db.Usuarios.Add(usuario);
            db.SaveChanges();

            TempData["Mensagem"] = "Cadastro realizado com sucesso, Efetue o Login!";
            return RedirectToAction("Login");
        }

        public ActionResult Login(string ReturnUrl)
        {

            var viewModel = new LoginViewModel { UrlRetorno = ReturnUrl };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var usuario = db.Usuarios.FirstOrDefault(u => u.Login == viewModel.Login);

            if (usuario == null)
            {
                ModelState.AddModelError("Login", "Login Incorreto!");
                return View(viewModel);
            }

            if (usuario.Senha != Hash.GerarHash(viewModel.Senha))
            {
                ModelState.AddModelError("Login", "Senha Incorreta!");
                return View(viewModel);
            }

            var identity = new ClaimsIdentity(new[] //classe que define informacoes e/ou caracteristicas de um usuario
            {
                new Claim(ClaimTypes.Name, usuario.Nome),//informacoes passadas como array do tipo Claim
                new Claim("Login", usuario.Login)},
                "ApplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(identity); // funcao que realiza o login no owin criando um cookie com as informacoes do usuario

            if (!String.IsNullOrWhiteSpace(viewModel.UrlRetorno) || Url.IsLocalUrl(viewModel.UrlRetorno))// se UrlRetorno nao for vazia  ou for uma Url local
                return Redirect(viewModel.UrlRetorno);
            else
                return RedirectToAction("Index", "Painel");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }
    }
}