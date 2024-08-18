using LocamailApp.Data.Repository;
using LocamailApp.Models;

namespace LocamailApp.Services
{
    public class EmailEnviadoService : IEmailEnviadoService
    {
        private readonly IEmailEnviadoRepository _emailEnviadoRepository;

        public EmailEnviadoService(IEmailEnviadoRepository emailEnviadoRepository)
        {
            _emailEnviadoRepository = emailEnviadoRepository;
        }

        public async Task<IEnumerable<EmailEnviadoModel>> GetAllAsync()
        {
            return await _emailEnviadoRepository.GetAllAsync();
        }

        public async Task<EmailEnviadoModel?> GetByIdAsync(string id)
        {
            return await _emailEnviadoRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(EmailEnviadoModel emailEnviadoModel)
        {
            emailEnviadoModel.IntId = await GetMaxIntIdAsync() + 1;
            await _emailEnviadoRepository.CreateAsync(emailEnviadoModel);
        }

        public async Task UpdateAsync(EmailEnviadoModel emailEnviadoModel)
        {
            await _emailEnviadoRepository.UpdateAsync(emailEnviadoModel);
        }

        public async Task DeleteAsync(string id)
        {
            await _emailEnviadoRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistePorEmailAsync(string email)
        {
            return await _emailEnviadoRepository.ExistePorEmailAsync(email);
        }

        public async Task<int> GetMaxIntIdAsync()
        {
            return await _emailEnviadoRepository.GetMaxIntIdAsync();
        }

    }

}
