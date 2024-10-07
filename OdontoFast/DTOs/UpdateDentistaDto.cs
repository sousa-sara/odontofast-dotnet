using System.ComponentModel.DataAnnotations;

namespace OdontoFast.DTOs
{
    public class UpdateDentistaDto
    {
        public string NomeDentista { get; set; }
        public string Especialidade { get; set; }
        public string Cro { get; set; }
    }
}