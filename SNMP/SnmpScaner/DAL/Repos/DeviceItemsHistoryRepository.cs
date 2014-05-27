﻿using DAL.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DeviceItemsHistoryRepository : BaseRepository<DeviceItemHistory>
    {
        private SnmpDbContext _context;
        public DeviceItemsHistoryRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }
    }
}