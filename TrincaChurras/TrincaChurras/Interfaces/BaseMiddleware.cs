using System;
using Refit;
using TrincaChurras.Helpers.Util;

namespace TrincaChurras.Interfaces
{
    public class BaseMiddleware
    {
        private IBaseMiddleware _api;

        protected IBaseMiddleware Api => _api ?? (_api = RestService.For<IBaseMiddleware>(BaseUri.Uri));
    }
}
