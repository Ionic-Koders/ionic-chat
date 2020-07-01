﻿using ionic_chat.DAL.Entities;
using ionic_chat.DAL.Repositories;
using ionoc_chat.Infrastructure;
using ionoc_chat.Infrastructure.Repositories;

namespace ionic_chat.Infrastructure.Repositories
{
    public class RoomRepository : BaseRepository<Room>
    {
        public RoomRepository(ApplicationDBContext applicationDBContext)
            : base(applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
            _dbSet = _applicationDBContext.Set<Room>();
        }
    }
}