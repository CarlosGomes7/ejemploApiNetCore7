using System.ComponentModel.DataAnnotations;

namespace Example_API_v1.Models
{
    public class Villa
    {
        [Key]
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string? Detalles { get; set; }

        [Required]
        public double Tarifa { get; set; }       
        
        public DateTime FechaDeCreacion { get; set; }
        public string? ImagenUrl { get; set; }
        public string? Amenidad { get; set; }
        public DateTime FechaDeActualizacion { get; set; }
    }
}
