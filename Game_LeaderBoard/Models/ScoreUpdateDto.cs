using Game_LeaderBoard.MongoRepository.GenericRepository;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Game_LeaderBoard.Models
{
    public class ScoreUpdateDto : IDocument
    {
        
        public string Id { get; set; }


        public int score { get; set; }


    }
    
}
