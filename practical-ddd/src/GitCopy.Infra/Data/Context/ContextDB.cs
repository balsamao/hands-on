using LiteDB;
using Microsoft.Extensions.Options;
using System.IO;

namespace GitCopy.Infra.Data.Context
{
    public class ContextDB : IContextDB
    {
        private ILiteDatabase Db { get; set; }
        public ContextDB(IOptions<SettingsDB> configuration)
        {
            Db = new LiteDatabase(Path.Combine(configuration.Value.Connection, $"{configuration.Value.DatabaseName}.db"));
        }

        public ILiteCollection<Log> GetCollection<Log>(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            return Db.GetCollection<Log>(name);
        }
    }
}
