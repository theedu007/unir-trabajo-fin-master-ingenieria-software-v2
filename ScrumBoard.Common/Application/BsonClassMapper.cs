using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using ScrumBoard.Common.Application.Entities;

namespace ScrumBoard.Common.Application
{
    public class BsonClassMapper
    {
        public static void MapClasses()
        {
            BsonClassMap.RegisterClassMap<WorkspaceUi>(mapper =>
            {
                mapper.MapIdProperty(x => x.Id)
                    .SetIdGenerator(new ObjectIdGenerator());
                mapper.MapProperty(x => x.Name)
                    .SetIsRequired(true)
                    .SetElementName("name");
                mapper.MapProperty(x => x.PublicKey)
                    .SetSerializer(new GuidSerializer(BsonType.String))
                    .SetElementName("publicKey")
                    .SetIsRequired(true);
                mapper.MapProperty(x => x.Description)
                    .SetElementName("description");
            });
        }
    }
}
