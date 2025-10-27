using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BancomatApp.Data;
using BancomatApp.Models;

namespace BancomatApp.Controllers
{
    public class BancomatController : Controller
    {
        private readonly BancomatDbContext _context;

        public BancomatController(BancomatDbContext context)
        {
            _context = context;
        }

        // GET: Bancomat/Login
        [HttpGet]
        public IActionResult Login()
        {
            // Pulisci la sessione
            HttpContext.Session.Clear();
            return View();
        }

        // POST: Bancomat/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Verifica il PIN
            var conto = await _context.ContiCorrenti
                .FirstOrDefaultAsync(c => c.CodicePin == model.Pin);

            if (conto == null)
            {
                ModelState.AddModelError("", "Codice PIN non valido");
                return View(model);
            }

            // Salva l'ID del conto in sessione
            HttpContext.Session.SetInt32("ContoId", conto.Id);

            return RedirectToAction(nameof(SelezionaImporto));
        }

        // GET: Bancomat/SelezionaImporto
        [HttpGet]
        public async Task<IActionResult> SelezionaImporto()
        {
            var contoId = HttpContext.Session.GetInt32("ContoId");
            if (contoId == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var conto = await _context.ContiCorrenti.FindAsync(contoId.Value);
            if (conto == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var viewModel = new PrelievoViewModel
            {
                ContoId = conto.Id,
                SaldoAttuale = conto.Saldo,
                ImportiDisponibili = new List<decimal> { 20, 50, 100, 200, 500 }
            };

            return View(viewModel);
        }

        // POST: Bancomat/Preleva
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Preleva(decimal? importoSelezionato, decimal? importoCustom)
        {
            var contoId = HttpContext.Session.GetInt32("ContoId");
            if (contoId == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var conto = await _context.ContiCorrenti.FindAsync(contoId.Value);
            if (conto == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Determina l'importo da prelevare
            decimal importo = importoCustom ?? importoSelezionato ?? 0;

            if (importo <= 0)
            {
                TempData["Errore"] = "Seleziona un importo valido";
                return RedirectToAction(nameof(SelezionaImporto));
            }

            if (importo > conto.Saldo)
            {
                TempData["Errore"] = "Saldo insufficiente";
                return RedirectToAction(nameof(SelezionaImporto));
            }

            // Effettua il prelievo
            conto.Saldo -= importo;

            // Crea il movimento
            var movimento = new Movimento
            {
                ContoCorrenteId = conto.Id,
                Importo = importo,
                DataPrelievo = DateTime.Now
            };

            _context.Movimenti.Add(movimento);
            await _context.SaveChangesAsync();

            // Prepara il risultato
            var risultato = new RisultatoPrelievoViewModel
            {
                ImportoPrelevato = importo,
                NuovoSaldo = conto.Saldo,
                Successo = true,
                Messaggio = "Prelievo effettuato con successo!"
            };

            return View("Conferma", risultato);
        }

        // GET: Bancomat/Conferma (per mostrare la conferma dopo il prelievo)
        public IActionResult Conferma()
        {
            // Questa action viene chiamata automaticamente dopo Preleva
            return View();
        }
    }
}
