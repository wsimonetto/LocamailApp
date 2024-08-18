using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LocamailApp.Models
{
    public class EmailEnviadoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } // MongoDB ObjectId como string

        public int IntId { get; set; } // Adicionado para a lógica de navegação

        [BsonElement("remetente")]
        public string Remetente { get; set; }

        [BsonElement("destinatario")]
        public string Destinatario { get; set; }

        [BsonElement("copia_para")]
        public string? CopiaPara { get; set; }

        [BsonElement("assunto")]
        public string Assunto { get; set; }

        [BsonElement("mensagem")]
        public string Mensagem { get; set; }

        [BsonElement("data_envio")]
        public DateTime DataEnvio { get; set; }

        [BsonElement("hora_envio")]
        public DateTime HoraEnvio { get; set; }

        [BsonElement("prioridade")]
        public string Prioridade { get; set; }

        [BsonElement("estado_resposta")]
        public bool EstadoResposta { get; set; }

        [BsonElement("data_resposta")]
        public DateTime? DataResposta { get; set; } // Pode ser nulo se não houver resposta ainda

        [BsonElement("hora_resposta")]
        public DateTime? HoraResposta { get; set; } // Pode ser nulo se não houver resposta ainda

        [BsonElement("estado_leitura")]
        public bool Lido { get; set; }

        [BsonElement("na_lixeira")]
        public bool Lixeira { get; set; }

        [BsonElement("no_arquivo")]
        public bool Arquivo { get; set; }

        [BsonElement("no_spam")]
        public bool Spam { get; set; }

        [BsonElement("no_rascunho")]
        public bool Rascunho { get; set; }

        [BsonElement("importante")]
        public bool Importante { get; set; }
        [BsonElement("rotulos")]
        public List<string>? Rotulos { get; set; } // Lista de rótulos adicionais
    }
}
