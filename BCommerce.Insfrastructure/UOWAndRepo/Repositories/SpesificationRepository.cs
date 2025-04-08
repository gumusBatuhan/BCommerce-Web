using BCommerce.Application.Data;
using BCommerce.Application.UOWAndRepo.Repositories;
using BCommerce.Core.DomainClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BCommerce.Insfrastructure.UOWAndRepo.Repositories
{
    public class SpesificationRepository : Repository<Spesification>, ISpesificationRepository
    {

        private readonly IApplicationDbContext dbContext;
        public SpesificationRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<List<Spesification>> GetPosibleSpesification(int? categoryId = null)
        {

            if (categoryId == null)
            {
                return await dbContext.Spesifications.Include(t => t.SpesificationItems).ToListAsync();
            }


            var categoryData = await dbContext.Categories.Include(t => t.Spesifications).ThenInclude(t => t.SpesificationItems).FirstOrDefaultAsync(t => t.Id == categoryId);
            return categoryData.Spesifications.ToList();

        }
    }
}
