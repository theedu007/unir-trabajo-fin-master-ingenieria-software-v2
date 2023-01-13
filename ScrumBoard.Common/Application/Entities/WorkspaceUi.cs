using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ScrumBoard.Common.Application.Entities
{
    public class WorkspaceUi
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid PublicKey { get; set; } = Guid.Empty;
        public List<Guid> UsersPublicKeys { get; set; } = new List<Guid>();
    }
}
