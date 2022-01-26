using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using TrincaChurras.Models;

namespace TrincaChurras.Interfaces
{
    public interface IBaseMiddleware
    {
        [Get("/bbq/?paginated=false")]
        Task<OperationDataResult<List<Barbecue>>> GetBarbecue([Header("Authorization")] string authorization);
    }
}
