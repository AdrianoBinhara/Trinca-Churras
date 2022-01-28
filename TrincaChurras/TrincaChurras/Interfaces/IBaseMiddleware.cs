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

        [Post("/bbq")]
        Task<OperationResult> PostBarbecue([Body] Barbecue barbecue, [Header("Authorization")] string authotization);

        [Post("/bbq/participant")]
        Task<OperationResult> PostParticipant([Body] Person person, [Header("Authorization")] string authotization);

        [Get("/bbq/{id}")]
        Task<Barbecue> GetBarbecueById(string id,[Header("Authorization")] string authorization);

        [Delete("/bbq/participant/{id}")]
        Task<OperationResult> DeleteParticipant(int id, [Header("Authorization")] string authorization);

        [Delete("/bbq/{id}")]
        Task<OperationResult> DeleteBarbecue(int id, [Header("Authorization")] string authorization);

        [Put("/bbq/participant")]
        Task<OperationResult> PutParticipant([Body] Person user, [Header("Authorization")] string authotization);

        [Put("/bbq")]
        Task<OperationResult> PutBarbcure([Body] Barbecue user, [Header("Authorization")] string authotization);
    }
}
