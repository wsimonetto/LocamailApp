using LocamailApp.Data.Repository;
using LocamailApp.Models;

namespace LocamailApp.Services
{
    public class EmailRecebidoService : IEmailRecebidoService
    {
        private readonly IEmailRecebidoRepository _repository;

        public EmailRecebidoService(IEmailRecebidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmailRecebidoModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<EmailRecebidoModel?> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(EmailRecebidoModel emailRecebidoModel)
        {
            emailRecebidoModel.IntId = await GetMaxIntIdAsync() + 1;
            await _repository.CreateAsync(emailRecebidoModel);
        }

        public async Task UpdateAsync(EmailRecebidoModel emailRecebidoModel)
        {
            await _repository.UpdateAsync(emailRecebidoModel);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<int> GetMaxIntIdAsync()
        {
            return await _repository.GetMaxIntIdAsync();
        }
    }
}
