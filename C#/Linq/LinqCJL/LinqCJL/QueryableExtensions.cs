using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace LinqCJL
{
    public static class QueryableExtensions
    {
        private const string ORDERBYDESCENDING = "OrderByDescending";
        private const string ORDERBY = "OrderBy";
        private const string THENBYDESCENDING = "ThenByDescending";
        private const string THENBY = "ThenBy";

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="paiXuZDMC"></param>
        /// <param name="daoXuBZ"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> OrderBy<TEntity>([NotNull] this IQueryable<TEntity> queryable, string paiXuZDMC, int? daoXuBZ) 
        {
            if (string.IsNullOrWhiteSpace(paiXuZDMC))
            {
                return queryable;
            }
            var paiXuZDList = paiXuZDMC.Split('|', StringSplitOptions.RemoveEmptyEntries);
            var propertyInfoList = typeof(TEntity).GetProperties();
            var firstBZ = true;
            foreach (var paiXuZD in paiXuZDList)
            {
                var sortProperty = propertyInfoList.FirstOrDefault(q => q.Name.ToUpper() == paiXuZD.ToUpper());
                var queryMethod = string.Empty;
                if (sortProperty == null)
                {
                    continue;
                }
                if (firstBZ)
                {
                    queryMethod = daoXuBZ == 1 ? ORDERBYDESCENDING : ORDERBY;
                    firstBZ = false;
                }
                else
                {
                    queryMethod = daoXuBZ == 1 ? THENBYDESCENDING : THENBY;
                }
                var orderedQueryable = queryable as IOrderedQueryable<TEntity>;
                ParameterExpression param = Expression.Parameter(typeof(TEntity));
                Expression body = Expression.Property(param, sortProperty.Name);
                LambdaExpression keySelectorLambda = Expression.Lambda(body, param);
                queryable = orderedQueryable.Provider.CreateQuery<TEntity>(Expression.Call(typeof(Queryable), queryMethod,
                                                                 new Type[] { typeof(TEntity), body.Type },
                                                                 queryable.Expression,
                                                                 Expression.Quote(keySelectorLambda)));
            }
            return queryable;
        }
        /// <summary>
        /// 字段是否为空值
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="kongZhiList"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> ZiDuanSFWK<TEntity>([NotNull] this IQueryable<TEntity> queryable, List<string> kongZhiList) 
        {
            if (kongZhiList.IsNullOrEmpty())
            {
                return queryable;
            }
            var propertyInfoList = typeof(TEntity).GetProperties();
            ParameterExpression param = Expression.Parameter(typeof(TEntity));
            Expression<Func<TEntity, bool>> orExpression = null;
            foreach (var item in kongZhiList)
            {
                var propertyInfo = propertyInfoList.FirstOrDefault(q => q.Name.ToUpper() == item.ToUpper());
                if (propertyInfo == null)
                {
                    continue;
                }
                var ziDuanExpreesion = Expression.Equal(Expression.Property(param, propertyInfo.Name), Expression.Constant(null, propertyInfo.PropertyType));
                if (orExpression == null)
                {
                    orExpression = Expression.Lambda<Func<TEntity, bool>>(ziDuanExpreesion, param);
                }
                else
                {
                    var secondExpression = Expression.Lambda<Func<TEntity, bool>>(ziDuanExpreesion, param);
                    orExpression = Expression.Lambda<Func<TEntity, bool>>(Expression.Or(orExpression.Body, secondExpression.Body), param);
                }
            }
            if (orExpression != null)
            {
                queryable = queryable.Where(orExpression);
            }
            return queryable;
        }
        //
        // 摘要:
        //     Checks whatever given collection object is null or has no item.
        public static bool IsNullOrEmpty<T>([AllowNull] this ICollection<T> source)
        {
            if (source != null)
            {
                return source.Count <= 0;
            }

            return true;
        }
    }
}
