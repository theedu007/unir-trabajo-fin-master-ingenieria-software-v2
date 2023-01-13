using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ScrumBoard.Common.Application.Entities
{
    public class WokspaceUiSettings
    {
        public int DefaultSprintLengthInDays { get; set; } = 10;
        public Guid PublicKey { get; set; }
        public ObjectId WorkspaceObjectId { get; set; }
    }
}
