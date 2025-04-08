using BCommerce.Core.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application.UOWAndRepo.Repositories
{
    public interface ISpesificationRepository : IRepository<Spesification>
    {
        Task<List<Spesification>> GetPosibleSpesification(int? categoryId);

    }
}
