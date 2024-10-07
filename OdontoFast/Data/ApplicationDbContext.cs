using Microsoft.EntityFrameworkCore; // Importa o namespace para trabalhar com Entity Framework Core
using OdontoFast.Models; // Importa os modelos utilizados na aplicação

namespace OdontoFast.Data
{
    // Classe que representa o contexto da aplicação para o Entity Framework
    public class ApplicationDbContext : DbContext
    {
        // Construtor que aceita opções de configuração para o DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet para a entidade Dentista, representa a tabela de dentistas no banco de dados
        public DbSet<Dentista> Dentistas { get; set; }

        // Método chamado para configurar o modelo de dados usando Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da entidade Dentista
            modelBuilder.Entity<Dentista>(entity =>
            {
                // Define o nome da tabela no banco de dados, excluindo-a das migrações
                entity.ToTable("C_OP_DENTISTA", t => t.ExcludeFromMigrations());

                // Define a chave primária da entidade
                entity.HasKey(e => e.IdDentista);

                // Configuração da propriedade IdDentista
                entity.Property(e => e.IdDentista)
                    .HasColumnName("ID_DENTISTA") // Nome da coluna no banco de dados
                    .ValueGeneratedOnAdd() // Indica que o valor será gerado automaticamente ao adicionar um novo registro
                    .HasDefaultValueSql("DENTISTA_SEQ.NEXTVAL"); // Usa a sequência DENTISTA_SEQ para gerar o ID

                // Configuração da propriedade NomeDentista
                entity.Property(e => e.NomeDentista)
                    .HasColumnName("NOME_DENTISTA") // Nome da coluna no banco de dados
                    .IsRequired() // Indica que o campo é obrigatório
                    .HasMaxLength(255); // Define o comprimento máximo da string

                // Configuração da propriedade SenhaDentista
                entity.Property(e => e.SenhaDentista)
                    .HasColumnName("SENHA_DENTISTA") // Nome da coluna no banco de dados
                    .IsRequired() // Indica que o campo é obrigatório
                    .HasMaxLength(255); // Define o comprimento máximo da string

                // Configuração da propriedade Especialidade
                entity.Property(e => e.Especialidade)
                    .HasColumnName("ESPECIALIDADE") // Nome da coluna no banco de dados
                    .IsRequired() // Indica que o campo é obrigatório
                    .HasMaxLength(255); // Define o comprimento máximo da string

                // Configuração da propriedade Cro
                entity.Property(e => e.Cro)
                    .HasColumnName("CRO") // Nome da coluna no banco de dados
                    .IsRequired() // Indica que o campo é obrigatório
                    .HasMaxLength(255); // Define o comprimento máximo da string
            });
        }
    }
}
