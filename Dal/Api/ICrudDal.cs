using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface ICrudDal<T>
    {
        Task Create(T item);
        Task<bool> Delete(T item);
        Task<bool> Update(T item);
        Task<List<T>> Search(Expression<Func<T, bool>> predicate);

    }

}
