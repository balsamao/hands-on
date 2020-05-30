using LiteDB;

namespace GitCopy.Infra.Data.Context
{
    public interface IContextDB
    {
        ILiteCollection<Log> GetCollection<Log>(string name);
    }
}
