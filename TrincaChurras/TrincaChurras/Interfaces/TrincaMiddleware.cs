using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrincaChurras.Models;

namespace TrincaChurras.Interfaces
{
    public class TrincaMiddleware : BaseMiddleware
    {

        public async Task<OperationDataResult<List<Barbecue>>> GetBarbecue(string token)
        {
            try
            {
                return await Api.GetBarbecue(token);
            }
            catch (Exception ex)
            {
                return new OperationDataResult<List<Barbecue>>
                {
                    Message = ex.Message,
                    Sucess = false
                };
            }
        }
    }
}
