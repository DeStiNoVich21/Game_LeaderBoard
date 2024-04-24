namespace Game_LeaderBoard.MongoRepository.GenericRepository
{
    public interface IMongoSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
