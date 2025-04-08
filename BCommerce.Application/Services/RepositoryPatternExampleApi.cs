using BCommerce.Application.Data;
using BCommerce.Application.UOWAndRepo;
using BCommerce.Core.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application.Services
{
    public class RepositoryPatternExampleApi
    {


        IUnitOfWork unitOfWork;
        private readonly IApplicationDbContext _context;

        public RepositoryPatternExampleApi(IUnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
        }

        public List<Product> GetProducts()
        {

            //productRepository.GetProducts();

            //solid
            _context.Products.Where(t => t.Id > 200).ToList();


            //unitOfWork.Commit(); savechanges

            // this.unitOfWork.Repository<Category>().GetAll(t => t.Id > 200).ToList();

            // this.unitOfWork.Repository<Product>().GetAll().Where(t => t.Id > 100).ToList();

            return this.unitOfWork.Repository<Product>().GetAll().ToList();



           
        }
    }
}
