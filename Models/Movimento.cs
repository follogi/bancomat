using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancomatApp.Models
{
    public class Movimento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Importo { get; set; }

        [Required]
        public DateTime DataPrelievo { get; set; } = DateTime.Now;

        [Required]
        public int ContoCorrenteId { get; set; }

        // Relazione con ContoCorrente
        [ForeignKey("ContoCorrenteId")]
        public virtual ContoCorrente? ContoCorrente { get; set; }
    }
}
