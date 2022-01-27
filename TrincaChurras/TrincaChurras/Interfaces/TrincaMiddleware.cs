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

        public async Task<OperationDataResult<Barbecue>> GetBarbecueById(string id, string token)
        {
            try
            {
                var response = await Api.GetBarbecueById(id, token);
                return new OperationDataResult<Barbecue>
                {
                    Data = response,
                    Sucess = true
                };
            }
            catch (Exception ex)
            {
                return new OperationDataResult<Barbecue>
                {
                    Message = ex.Message,
                    Sucess = false
                };
            }
        }

        public async Task<OperationDataResult<TokenResponse>> PostUser(object user)
        {
            try
            {
                var response = await Api.PostUser(user);
                return new OperationDataResult<TokenResponse>
                {
                    Message = response.Token,
                    Sucess = true
                };
            }
            catch (Exception ex)
            {
                return new OperationDataResult<TokenResponse>
                {
                    Message = ex.Message,
                    Sucess = false
                };
            }
        }

        public async Task<OperationDataResult<OperationResult>> PutParticipant(Person user, string token)
        {
            try
            {
                var response = await Api.PutParticipant(user, token);
                return new OperationDataResult<OperationResult>
                {
                    Message = response.Message,
                    Sucess = true
                };
            }
            catch (Exception ex)
            {
                return new OperationDataResult<OperationResult>
                {
                    Message = ex.Message,
                    Sucess = false
                };
            }
        }
    }
}
