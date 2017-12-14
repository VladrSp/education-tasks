using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IQueryableTasks.Task2
{
    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            var sourceParam = Expression.Parameter(typeof(TSource), "source");
            var destinationProperties = typeof(TDestination).GetProperties().Select(x=>x.Name);

            var bindings = sourceParam.Type.GetProperties()
                .Where(x=> destinationProperties.Contains(x.Name))
                .Select(p => Expression.Bind(typeof(TDestination).GetProperty(p.Name), Expression.Property(sourceParam, p)));

            var body = Expression.MemberInit(Expression.New(typeof(TDestination)), bindings);

            var mapFunction = Expression.Lambda<Func<TSource, TDestination>>(body, sourceParam);

            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }
    }
}
