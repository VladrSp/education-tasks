using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IQueryableTasks.Task1
{
    public class ParameterTransformVisitor : ExpressionVisitor
    {
        private Dictionary<string, string> parameters;
        public ParameterTransformVisitor(Dictionary<string, string> parameters)
        {
            this.parameters = parameters ?? new Dictionary<string, string>();

        }
        public ParameterTransformVisitor():base()
        {
            this.parameters = new Dictionary<string, string>();
        }
        
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.Subtract || node.NodeType == ExpressionType.Add)
            {
                ParameterExpression param = null;
                ConstantExpression constant = null;
                if (node.Left.NodeType == ExpressionType.Parameter)
                {
                    param = ReplaceParam((ParameterExpression)node.Left);

                }
                else if (node.Left.NodeType == ExpressionType.Constant)
                {
                    constant = (ConstantExpression)node.Left;
                }

                if (node.Right.NodeType == ExpressionType.Parameter)
                {
                    param = ReplaceParam((ParameterExpression)node.Right);

                }
                else if (node.Right.NodeType == ExpressionType.Constant)
                {
                    constant = (ConstantExpression)node.Right;
                }

                if (node.NodeType == ExpressionType.Subtract && param != null && constant != null && constant.Type == typeof(int) && (int)constant.Value == 1)
                {
                    return Expression.Decrement(param);
                }
                else if (node.NodeType == ExpressionType.Add && param != null && constant != null && constant.Type == typeof(int) && (int)constant.Value == 1)
                {
                    return Expression.Increment(param);
                }

            }
            return base.VisitBinary(node);
        }

        private ParameterExpression ReplaceParam(ParameterExpression param)
        {
            string value = null;
            if (parameters.Any() && parameters.TryGetValue(param.Name, out value))
            {
                return Expression.Parameter(param.Type, value);
            }
            else
            {
                return param;
            }
        }


        protected override Expression VisitParameter(ParameterExpression node)
        {
            ParameterExpression replacement = ReplaceParam(node);
            return base.VisitParameter(replacement);
        }

    }
}
