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

        [Post("/bbq/auth")]
        Task<TokenResponse> PostUser([Body] object user);

        [Get("/bbq/{id}")]
        Task<Barbecue> GetBarbecueById(string id,[Header("Authorization")] string authorization);

        [Put("/bbq/participant")]
        Task<OperationResult> PutParticipant([Body] Person user, [Header("Authorization")] string authotization);
    }
}
