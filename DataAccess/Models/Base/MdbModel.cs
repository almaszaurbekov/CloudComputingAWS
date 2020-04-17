using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
namespace DataAccess.Models.Base
{
    public abstract class MdbModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Пользователь-создатель
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }
    }
}
