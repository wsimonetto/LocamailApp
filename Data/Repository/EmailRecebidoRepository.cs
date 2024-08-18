using LocamailApp.Data.Repository;
using LocamailApp.Models;
using MongoDB.Driver;

namespace LocamailApp.Data.Repositories
{
    public class EmailRecebidoRepository : IEmailRecebidoRepository
    {
        private readonly IMongoCollection<EmailRecebidoModel> _emailsRecebidos;

        public EmailRecebidoRepository(MongoDBContext context)
        {
            _emailsRecebidos = context.EmailRecebido;
        }

        public async Task<IEnumerable<EmailRecebidoModel>> GetAllAsync()
        {
            return await _emailsRecebidos.Find(email => true).ToListAsync();
        }

        public async Task<EmailRecebidoModel?> GetByIdAsync(string id)
        {
            return await _emailsRecebidos.Find(email => email.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(EmailRecebidoModel emailRecebidoModel)
        {
            await _emailsRecebidos.InsertOneAsync(emailRecebidoModel);
        }

        public async Task UpdateAsync(EmailRecebidoModel emailRecebidoModel)
        {
            await _emailsRecebidos.ReplaceOneAsync(email => email.Id == emailRecebidoModel.Id, emailRecebidoModel);
        }

        public async Task DeleteAsync(string id)
        {
            await _emailsRecebidos.DeleteOneAsync(email => email.Id == id);
        }

        public async Task<int> GetMaxIntIdAsync()
        {
            var filter = Builders<EmailRecebidoModel>.Filter.Empty;
            var sort = Builders<EmailRecebidoModel>.Sort.Descending(e => e.IntId);
            var options = new FindOptions<EmailRecebidoModel, EmailRecebidoModel>
            {
                Sort = sort,
                Limit = 1
            };

            var emailRecebido = await _emailsRecebidos.FindAsync(filter, options);
            var maxIntIdEmail = await emailRecebido.FirstOrDefaultAsync();
            return maxIntIdEmail != null ? maxIntIdEmail.IntId : 0;
        }
    }
}

