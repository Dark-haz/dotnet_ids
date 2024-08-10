using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Data;
using Dotnetids.Models;
using Org.BouncyCastle.Crypto.Signers;

namespace dotnet_ids.Repository
{

    public class GuideRepository : Repository<Guide> 
    {
        private readonly AppDbContext _db;

        public GuideRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }


    }
}

