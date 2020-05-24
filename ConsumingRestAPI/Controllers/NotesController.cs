using ConsumingRestAPI.Models.DTO;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ConsumingRestAPI.Controllers
{
    public class NotesController : Controller
    {

        HttpClient httpClient = new HttpClient();

        public NotesController() //controlador: instaciando a classe HttpClient para configuracao do objeto, 
                                //evitando que seja configurada em todas as actions
        {
            httpClient.BaseAddress = new Uri("http://www.deveup.com.br/notas/");//endereco base da API
            httpClient.DefaultRequestHeaders.Accept.Clear();//exclui os headers que vem como padrao
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//configura os header para 
                                                                                          //informar que envia/recebe dados em formato json
        }

        // GET: Notes
        public ActionResult Index()
        {
            List<Note> notes = new List<Note>();//instancia a lista para que caso retorne null nao gere erro
            HttpResponseMessage response = httpClient.GetAsync("api/notes").Result;//mensagem de retorno do servico com a lista de notes.
            if (response.IsSuccessStatusCode)//status 200 ok
            {
                notes = response.Content.ReadAsAsync<List<Note>>().Result;//lista de notas recebe o resultado da requisicao
            }

            return View(notes);
        }

        // GET: Notes/Details/5
        public ActionResult Details(int id)
        {

            HttpResponseMessage response = httpClient.GetAsync($"api/notes/{id}").Result;// $ {} = string interpolation, passando o id para retornar uma note especifica
            Note note = response.Content.ReadAsAsync<Note>().Result;// recebe o resultado da requisicao do id note.
            if (note != null)            
                return View(note);
            else
                return HttpNotFound();//status 404                        
        }

        // GET: Notes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        [HttpPost]
        public ActionResult Create(Note note)
        {
            try
            {
                HttpResponseMessage response = httpClient.PostAsJsonAsync<Note>("api/notes", note).Result; //envia em formato Json 
                if (response.StatusCode == System.Net.HttpStatusCode.OK) //status 200
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Erro na geracao do lembrete";
                    return View();
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Notes/Edit/5
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = httpClient.GetAsync($"api/notes/{id}").Result;// $ {} = string interpolation, passando o id para retornar uma note especifica
            Note note = response.Content.ReadAsAsync<Note>().Result;// recebe o resultado da requisicao do id note.
            if (note != null)
                return View(note);
            else
                return HttpNotFound();//status 404 
        }

        // POST: Notes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Note note)
        {
            try
            {
                HttpResponseMessage response = httpClient.PutAsJsonAsync<Note>($"api/notes/{id}", note).Result; //envia em formato Json 
                if (response.StatusCode == System.Net.HttpStatusCode.OK) //status 200
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Erro na edicao do lembrete";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Notes/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = httpClient.GetAsync($"api/notes/{id}").Result;// $ {} = string interpolation, passando o id para retornar uma note especifica
            Note note = response.Content.ReadAsAsync<Note>().Result;// recebe o resultado da requisicao do id note.
            if (note != null)
                return View(note);
            else
                return HttpNotFound();//status 404
        }

        // POST: Notes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                HttpResponseMessage response = httpClient.DeleteAsync($"api/notes/{id}").Result; //envia em formato Json 
                if (response.StatusCode == System.Net.HttpStatusCode.OK) //status 200
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Erro na exclusao do lembrete";
                    return View();
                }                                
            }
            catch
            {
                return View();
            }
        }
    }
}
