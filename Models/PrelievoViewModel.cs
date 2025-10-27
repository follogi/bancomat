using System.ComponentModel.DataAnnotations;

namespace BancomatApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Il codice PIN è obbligatorio")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Il PIN deve essere di 4 cifre")]
        [Display(Name = "Codice PIN")]
        public string Pin { get; set; } = string.Empty;
    }

    public class PrelievoViewModel
    {
        public int ContoId { get; set; }
        public decimal SaldoAttuale { get; set; }
        public decimal? ImportoSelezionato { get; set; }

        [Range(10, 1000, ErrorMessage = "L'importo deve essere tra 10 e 1000 euro")]
        public decimal? ImportoCustom { get; set; }

        public List<decimal> ImportiDisponibili { get; set; } = new List<decimal> { 20, 50, 100, 200, 500 };
    }

    public class RisultatoPrelievoViewModel
    {
        public decimal ImportoPrelevato { get; set; }
        public decimal NuovoSaldo { get; set; }
        public bool Successo { get; set; }
        public string? Messaggio { get; set; }
    }
}
