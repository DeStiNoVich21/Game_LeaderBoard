using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection.Metadata;
using Game_LeaderBoard.MongoRepository.GenericRepository;

namespace Game_LeaderBoard.Models
{
    public class Users : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        [BsonElement]
        public string username { get; set; } = string.Empty;

        [BsonElement]
        public int score { get; set; }

        [BsonElement]
        public string DeviceName { get; set; } = string.Empty;
    

    }
}