using LocamailApp.Models;
using MongoDB.Driver;

namespace LocamailApp.Data.Repository
{
    public class EmailEnviadoRepository : IEmailEnviadoRepository
    {
        private readonly IMongoCollection<EmailEnviadoModel> _emailsEnviados;

        public EmailEnviadoRepository(MongoDBContext context)
        {
            _emailsEnviados = context.EmailEnviado;
        }

        public async Task<IEnumerable<EmailEnviadoModel>> GetAllAsync()
        {
            return await _emailsEnviados.Find(email => true).ToListAsync();
        }

        public async Task<EmailEnviadoModel?> GetByIdAsync(string id)
        {
            return await _emailsEnviados.Find(email => email.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(EmailEnviadoModel emailEnviadoModel)
        {
            await _emailsEnviados.InsertOneAsync(emailEnviadoModel);
        }

        public async Task UpdateAsync(EmailEnviadoModel emailEnviadoModel)
        {
            await _emailsEnviados.ReplaceOneAsync(email => email.Id == emailEnviadoModel.Id, emailEnviadoModel);
        }

        public async Task DeleteAsync(string id)
        {
            await _emailsEnviados.DeleteOneAsync(email => email.Id == id);
        }

        public async Task<bool> ExistePorEmailAsync(string email)
        {
            var filter = Builders<EmailEnviadoModel>.Filter.Eq(e => e.Remetente, email) |
                         Builders<EmailEnviadoModel>.Filter.Eq(e => e.Destinatario, email);

            return await _emailsEnviados.Find(filter).AnyAsync();
        }

        public async Task<int> GetMaxIntIdAsync()
        {
            var filter = Builders<EmailEnviadoModel>.Filter.Empty;
            var sort = Builders<EmailEnviadoModel>.Sort.Descending(e => e.IntId);
            var options = new FindOptions<EmailEnviadoModel, EmailEnviadoModel>
            {
                Sort = sort,
                Limit = 1
            };

            var emailEnviado = await _emailsEnviados.FindAsync(filter, options);
            var maxIntIdEmail = await emailEnviado.FirstOrDefaultAsync();
            return maxIntIdEmail != null ? maxIntIdEmail.IntId : 0;
        }
    }

}

