using System.ComponentModel.DataAnnotations;

namespace OdontoFast.DTOs
{
    public class UpdateDentistaDto
    {
        public int IdDentista { get; set; } 
        public string NomeDentista { get; set; }
        public string Especialidade { get; set; }
        public string Cro { get; set; }
    }
}