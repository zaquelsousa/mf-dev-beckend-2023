using mf_dev_beckend_2023.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mf_dev_beckend_2023.Controllers
{
    public class VeiculosController : Controller
    {   
        private readonly AppDbContext _context;
        public VeiculosController(AppDbContext context) { 
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dados = await _context.Veiculos.ToListAsync();

            return View(dados);
        }

        //quando nao coloca o tipo de requisição o padrao fica sendo get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Create(Veiculo veiculo)
        {
            if (ModelState.IsValid) {
                _context.Veiculos.Add(veiculo);//faz a inserção no banco de dados no caso a tabela Veiculos
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)//recebe o id pra fazer o reteamente e processamento dos dados
        {
            if (id == null) {
                return NotFound();
            }

            var dados = await _context.Veiculos.FindAsync(id);

            if (dados == null)
            {
                return NotFound();
            }
            return View(dados);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Veiculo veiculo)
        {
           if(id != veiculo.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Veiculos.Update(veiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            //recupera os dados do DB e joga pra dentro da Details.cshtml
            if (id == null)
            {
                return NotFound();
            }
            var dados = await _context.Veiculos.FindAsync(id);
            if (dados == null)
            {
                return NotFound();
            }
            return View(dados);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            //primeiro recupera os dados do DB ai se user confirma que quer apagar os treco joga pra func DeleteConfirmed, so entao que a mudança e efetuada no DB
            if (id == null)
            {
                return NotFound();
            }
            var dados = await _context.Veiculos.FindAsync(id);
            if (dados == null)
            {
                return NotFound();
            }
            return View(dados);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dados = await _context.Veiculos.FindAsync(id);
            if (dados == null)
            {
                return NotFound();
            }

            _context.Veiculos.Remove(dados);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
