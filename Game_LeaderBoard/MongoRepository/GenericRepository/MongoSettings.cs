namespace Game_LeaderBoard.MongoRepository.GenericRepository
{
    public class MongoSettings : IMongoSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
