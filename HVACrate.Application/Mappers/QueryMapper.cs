using HVACrate.Application.Models;
using HVACrate.Domain.Repositories;

namespace HVACrate.Application.Mappers
{
    public static class QueryMapper
    {
        public static BaseQuery ToQuery(this BaseQueryModel queryModel)
           => new()
           {
               SearchParam = queryModel.SearchParam,
               QueryParam = queryModel.QueryParam,
               Pagination = queryModel.Pagination,
           };
    }
}
