using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ScrumBoard.Common.Application.Entities
{
    public class WorkspaceUi
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid PublicKey { get; set; } = Guid.Empty;
    }
}
