using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ScrumBoard.Common.Application.Entities;

namespace ScrumBoard.Common.Application
{
    public class ApplicationDbContext : MongoClient
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public ApplicationDbContext(IOptions<ApplicationDbSettings> settings)
        {
            _client = new MongoClient(settings.Value.ConnectionString);
            _database = _client.GetDatabase(settings.Value.Database);

        }

        public IMongoCollection<WorkspaceUi> Workspaces => _database.GetCollection<WorkspaceUi>("workspaceUi");
    }
}
