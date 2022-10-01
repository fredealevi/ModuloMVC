using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModuloMVC.Context;
using ModuloMVC.Models;

namespace ModuloMVC.Controllers
{
    public class ContatoController : Controller
    {
        private readonly AgendaContext _context;
        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {   
            var contatos = _context.Contatos.ToList();
            return View(contatos);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Contato contato)
        {
            if (ModelState.IsValid)
            {
                _context.Contatos.Add(contato);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(contato);
        }

        [HttpGet]
        public IActionResult Editar (int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
                return NotFound();

            return View(contato);
        }
        [HttpPost]
        public IActionResult Editar (Contato contatoAtualizado)
        {
            var contato = _context.Contatos.Find(contatoAtualizado.Id);

            contato.Nome = contatoAtualizado.Nome;
            contato.Telefone = contatoAtualizado.Telefone;
            contato.Ativo = contatoAtualizado.Ativo;

            _context.Contatos.Update(contato);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}