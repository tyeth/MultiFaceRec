using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiFaceRec
{
   class FacialCroppedMatch
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageBytes { get; set; }
        public string Person { get; set; }
        //      public BsonDocument Image { get; set; }

    }
}
