using OdontoFast.DTOs; // Importa os Data Transfer Objects (DTOs)
using OdontoFast.Exceptions; // Importa as exceções personalizadas
using OdontoFast.Repository.Interfaces; // Importa as interfaces de repositório
using AutoMapper; // Importa a biblioteca AutoMapper para mapeamento de objetos
using OdontoFast.Models; // Importa os modelos (entidades)

namespace OdontoFast.Services
{
    // Classe que implementa a lógica de negócios para a entidade Dentista
    public class DentistaService : IDentistaService
    {
        private readonly IDentistaRepository _dentistaRepository; // Repositório de dentistas
        private readonly IMapper _mapper; // Mapeador para converter entre entidades e DTOs

        // Construtor que injeta as dependências
        public DentistaService(IDentistaRepository dentistaRepository, IMapper mapper)
        {
            _dentistaRepository = dentistaRepository;
            _mapper = mapper;
        }

        // Método para criar um novo dentista
        public async Task<DentistaDto> CreateDentistaAsync(CreateDentistaDto dto)
        {
            // Verifica se já existe um dentista com o mesmo CRO
            if (await _dentistaRepository.GetByCroAsync(dto.Cro) != null)
                throw new BusinessException("Dentista já cadastrado com esse CRO.");

            // Mapeia o DTO para a entidade Dentista
            var dentista = _mapper.Map<Dentista>(dto);
            // Adiciona o dentista ao repositório
            await _dentistaRepository.AddAsync(dentista);

            // Retorna o DTO do dentista criado
            return _mapper.Map<DentistaDto>(dentista);
        }

        // Método para obter um dentista pelo ID
        public async Task<DentistaDto> GetDentistaByIdAsync(int id)
        {
            var dentista = await _dentistaRepository.GetByIdAsync(id);
            if (dentista == null)
                throw new NotFoundException("Dentista não encontrado.");

            // Retorna o DTO do dentista encontrado
            return _mapper.Map<DentistaDto>(dentista);
        }

        // Método para realizar o login do dentista
        public async Task<DentistaDto> LoginAsync(DentistaLoginDto loginDto)
        {
            // Verifica se o dentista existe pelo CRO
            var dentista = await _dentistaRepository.GetByCroAsync(loginDto.Cro);
            if (dentista == null || dentista.SenhaDentista != loginDto.SenhaDentista)
            {
                throw new BusinessException("Credenciais inválidas.");
            }

            // Retorna o DTO do dentista para uso no frontend
            return new DentistaDto
            {
                IdDentista = dentista.IdDentista,
                Cro = dentista.Cro,
                Especialidade = dentista.Especialidade,
                NomeDentista = dentista.NomeDentista // Adicione outros campos conforme necessário
            };
        }

        // Método para obter todos os dentistas com paginação
        public async Task<IEnumerable<DentistaDto>> GetAllDentistasAsync(int pageNumber, int pageSize)
        {
            var dentistas = await _dentistaRepository.GetAllAsync();
            // Implementa a paginação
            return dentistas.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                            .Select(d => _mapper.Map<DentistaDto>(d));
        }

        // Método para atualizar um dentista
        public async Task<DentistaDto> UpdateDentistaAsync(int id, UpdateDentistaDto dto)
        {
            var dentista = await _dentistaRepository.GetByIdAsync(id);
            if (dentista == null)
                throw new NotFoundException("Dentista não encontrado.");

            // Atualiza os dados do dentista com os dados do DTO
            _mapper.Map(dto, dentista);
            await _dentistaRepository.UpdateAsync(dentista); // Atualiza o dentista no repositório

            // Retorna o DTO atualizado
            return _mapper.Map<DentistaDto>(dentista);
        }

        // Método para deletar um dentista
        public async Task DeleteDentistaAsync(int idDentista)
        {
            // Verifica se o dentista existe antes de tentar deletá-lo
            var dentista = await _dentistaRepository.GetByIdAsync(idDentista);
            if (dentista == null)
                throw new NotFoundException("Dentista não encontrado.");

            // Deleta o dentista usando apenas o ID
            await _dentistaRepository.DeleteAsync(idDentista);
        }
    }
}
