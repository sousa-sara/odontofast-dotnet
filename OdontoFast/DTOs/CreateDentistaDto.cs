using System.ComponentModel.DataAnnotations; // Importa as anotações de validação

// Classe Data Transfer Object (DTO) para criar um novo dentista
public class CreateDentistaDto
{
    // Propriedade que representa o nome do dentista
    [Required] // Indica que o campo é obrigatório
    public string NomeDentista { get; set; } = string.Empty; // Define um valor padrão como uma string vazia

    // Propriedade que representa a senha do dentista
    [Required] // Indica que o campo é obrigatório
    public string SenhaDentista { get; set; } = string.Empty; // Define um valor padrão como uma string vazia

    // Propriedade que representa a especialidade do dentista
    [Required] // Indica que o campo é obrigatório
    public string Especialidade { get; set; } = string.Empty; // Define um valor padrão como uma string vazia

    // Propriedade que representa o CRO (Cadastro de Registro de Odontologia) do dentista
    [Required] // Indica que o campo é obrigatório
    public string Cro { get; set; } = string.Empty; // Define um valor padrão como uma string vazia
}
