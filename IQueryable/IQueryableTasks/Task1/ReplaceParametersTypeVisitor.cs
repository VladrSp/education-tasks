using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IQueryableTasks.Task1
{
    public class ReplaceParametersTypeVisitor : ExpressionVisitor
    {
        private Dictionary<string, string> parameters;

        public Expression VisitReplaceParameters(Expression expression, Dictionary<string, string> parameters)
        {
            this.parameters = parameters;
            return base.Visit(expression);
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            string value = null;
            if (parameters.TryGetValue(node.Name, out value))
            {
                return Expression.Parameter(node.Type, value);
            }
            else
            {
                return base.VisitParameter(node);
            }
        }
    }
}
