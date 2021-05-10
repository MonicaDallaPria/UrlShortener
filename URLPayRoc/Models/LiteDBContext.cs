using LiteDB;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLPayRoc.Models
{
    public class LiteDbContext
    {
        public LiteDatabase Database { get; }
        public LiteDatabase DatabaseLocation { get; private set; }

        public LiteDbContext(IOptions<LiteDbOptions> options)
        { 

            using (var db = new LiteDatabase(@"Filename=Data/Urls.db; Connection=shared"))
            {
                DatabaseLocation = db;
                db.GetCollection<LiteDbContext>();
            }

        }

    }
}
