using LocamailApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LocamailApp.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;
        private readonly DataBaseSettings _settings;

        public MongoDBContext(IOptions<DataBaseSettings> options)
        {
            _settings = options.Value;
            var client = new MongoClient(_settings.ConnectionURI);
            _database = client.GetDatabase(_settings.DatabaseName);
        }

        public IMongoCollection<UsuarioModel> Usuario =>
            _database.GetCollection<UsuarioModel>(_settings.UsuarioCollectionName);

        public IMongoCollection<EmailEnviadoModel> EmailEnviado =>
            _database.GetCollection<EmailEnviadoModel>(_settings.EmailEnviadoCollectionName);

        public IMongoCollection<EmailRecebidoModel> EmailRecebido =>
            _database.GetCollection<EmailRecebidoModel>(_settings.EmailRecebidoCollectionName);

    }
}
