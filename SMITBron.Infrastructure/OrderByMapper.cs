//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace SMITBron.Infrastructure
//{
//    public static class OrderByMapper
//    {
//        public static IQueryable<TResult> OrderQuery<T, TResult>(this IQueryable<TResult> query, string field, bool desc)
//        {
//            var property = typeof(T).GetProperties().Where(x => x.GetAccessors().Any(y => y.IsPublic) && x.Name.ToLowerInvariant() ==
//               field.ToLowerInvariant()).FirstOrDefault();

//            if (property != null)
//            {
//                return desc ? query.OrderByDescending();
//            }


//        }
//    }
//}
