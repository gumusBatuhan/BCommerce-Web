using AutoMapper;
using BCommerce.Application.UOWAndRepo.Repositories;
using BCommerce.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application.Services.AfterRepoAndUOWService
{
    public class SpesificationService
    {
        private readonly ISpesificationRepository spesificationRepository;
        private readonly IMapper mapper;
        public SpesificationService(ISpesificationRepository spesificationRepository, IMapper mapper)
        {
            this.spesificationRepository = spesificationRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<SpesificationDTO>>> GetPosibleSpesification(int? categoryId = null)
        {

            var posibleINDb = await this.spesificationRepository.GetPosibleSpesification(categoryId);
            var posibleDto = mapper.Map<List<SpesificationDTO>>(posibleINDb);

            Result<List<SpesificationDTO>> result = new Result<List<SpesificationDTO>>();
            result.IsSuccess = true;
            result.Data = posibleDto;
            return result;
        }
    }
}
