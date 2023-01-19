using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Arcadian.Domain.Entities;
using Arcadian.Application.Dtos.Transaction;

namespace Arcadian.Application.Common.Mappings
{
    public class DefaultMappingProfile : Profile
    {
        public DefaultMappingProfile() 
        {
            CreateMap<Transaction, TransactionDto>();
        }
    }
}
