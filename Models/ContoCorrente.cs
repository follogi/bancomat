using System.ComponentModel.DataAnnotations;

namespace BancomatApp.Models
{
    public class ContoCorrente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string CodicePin { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Saldo { get; set; }

        [Required]
        [StringLength(50)]
        public string NumeroContoCorrente { get; set; } = string.Empty;

        // Relazione con Movimento
        public virtual ICollection<Movimento> Movimenti { get; set; } = new List<Movimento>();
    }
}
