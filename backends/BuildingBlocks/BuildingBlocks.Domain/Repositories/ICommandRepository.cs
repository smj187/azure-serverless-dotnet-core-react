﻿using BuildingBlocks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.Repositories
{
    public interface ICommandRepository<T> where T : IAggregateBase
    {
        Task<T> AddAsync(T entity);
        Task<T> PatchAsync(T entity);

    }
}
