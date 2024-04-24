using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Game_LeaderBoard.MongoRepository.GenericRepository
{
    public interface IDocument
    {
        string Id { get; set; }
    }
}
