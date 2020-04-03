using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoAspNET.Models;
using TodoAspNET.Data;
using TodoAspNET.ViewModels;

namespace TodoAspNET.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tarefas = _context.Tarefas.ToList();
            
            var listaTarefas = new IndexViewModel(){
                Tarefas = tarefas
            };

            return View(listaTarefas);
        }
        
        [HttpPost]
        public IActionResult Index(IndexViewModel tarefa)
        {
            tarefa.TarefaToSend.Status = false;
            //Adicionamos em nosso banco a tarefa que existe no VeiwModel 
            _context.Tarefas.Add(tarefa.TarefaToSend);
           
            _context.SaveChanges();
            
            var tarefas = _context.Tarefas.ToList();
            
            var listaTarefas = new IndexViewModel(){
                Tarefas = tarefas
            };

            return View(listaTarefas);            

        }
        
        public IActionResult CompletaTarefa(int IdTarefa)
        {
            //A primeira tarefa que existir com o id encontrado é setado em tarefas e marcado como TRUE
            var tarefa = _context.Tarefas.Where(x => x.Id == IdTarefa).First();
            //Mudando o status da tarefa
            tarefa.Status = !tarefa.Status;
            //Salvando as configurações no banco
            _context.SaveChanges();

            var tarefas = _context.Tarefas.ToList();
            var listaTarefas = new IndexViewModel(){
                Tarefas = tarefas
            };

            return View("Index", listaTarefas);
        }

        public IActionResult DeletaTarefa(int IdTarefa)
        {
            var tarefa = _context.Tarefas.Where(x => x.Id == IdTarefa).First();
            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();

            var tarefas = _context.Tarefas.ToList();
            var listaTarefas = new IndexViewModel(){
                Tarefas = tarefas
            };

            return View("Index", listaTarefas);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
