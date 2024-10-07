using System.ComponentModel.DataAnnotations; // Importa para usar anotações de validação
using System.ComponentModel.DataAnnotations.Schema; // Importa para usar anotações de mapeamento de banco de dados

namespace OdontoFast.Models
{
    // Atributo que especifica a tabela correspondente no banco de dados
    [Table("C_OP_DENTISTA")]
    public class Dentista
    {
        // Propriedade que representa a chave primária da tabela
        [Key] // Indica que esta propriedade é a chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // O valor será gerado pelo banco de dados
        [Column("ID_DENTISTA")] // Mapeia a propriedade para a coluna correspondente
        public int IdDentista { get; set; } // ID do dentista

        // Propriedade que armazena o nome do dentista
        [Column("NOME_DENTISTA")] // Mapeia a propriedade para a coluna correspondente
        [Required] // Torna o campo obrigatório no banco de dados
        public string NomeDentista { get; set; } = string.Empty; // Inicializa com valor padrão

        // Propriedade que armazena a senha do dentista
        [Column("SENHA_DENTISTA")] // Mapeia a propriedade para a coluna correspondente
        [Required] // Torna o campo obrigatório no banco de dados
        public string SenhaDentista { get; set; } = string.Empty; // Inicializa com valor padrão

        // Propriedade que armazena a especialidade do dentista
        [Column("ESPECIALIDADE")] // Mapeia a propriedade para a coluna correspondente
        [Required] // Torna o campo obrigatório no banco de dados
        public string Especialidade { get; set; } = string.Empty; // Inicializa com valor padrão

        // Propriedade que armazena o número do CRO (Conselho Regional de Odontologia)
        [Column("CRO")] // Mapeia a propriedade para a coluna correspondente
        [Required] // Torna o campo obrigatório no banco de dados
        public string Cro { get; set; } = string.Empty; // Inicializa com valor padrão
    }
}
