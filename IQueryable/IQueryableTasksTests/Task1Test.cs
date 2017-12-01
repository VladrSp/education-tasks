using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using IQueryableTasks.Task1;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IQueryableTasksTests
{
    //public class Parameters : ExpressionVisitor
    //{
    //    Dictionary<string, object> parameters;
    //    public Parameters(Dictionary<string,object> parameters) : base()
    //    {
    //        this.parameters = parameters;
    //    }
    //    protected override Expression VisitBinary(BinaryExpression node)
    //    {
    //        if (node.NodeType == ExpressionType.Subtract)
    //        {
    //            ParameterExpression param = null;
    //            ConstantExpression constant = null;
    //            if (node.Left.NodeType == ExpressionType.Parameter)
    //            {
    //                param = (ParameterExpression)node.Left;
    //            }
    //            else if (node.Left.NodeType == ExpressionType.Constant)
    //            {
    //                constant = (ConstantExpression)node.Left;
    //            }

    //            if (node.Right.NodeType == ExpressionType.Parameter)
    //            {
    //                param = (ParameterExpression)node.Right;
    //            }
    //            else if (node.Right.NodeType == ExpressionType.Constant)
    //            {
    //                constant = (ConstantExpression)node.Right;
    //            }

    //            if (param != null && constant != null && constant.Type == typeof(int) && parameters.ContainsKey(param.Name))
    //            {
    //                var exp3 = Expression.Lambda(
    //                    Expression.New(
    //                        obj.GetType().GetConstructors()[0],
    //                        Expression.Constant("Alex"), Expression.Constant(17)
    //                        ));
                
    //            }
    //            return base.VisitBinary(node);
    //        }
    //    }

    //    private string VisitBinary(BinaryExpression binary, string @operator, LambdaExpression expression)
    //    {
    //        return $"{@operator}({this.VisitNode(binary.Left, expression)}, {this.VisitNode(binary.Right, expression)})";            
    //    }        
    //}

     
    [TestClass]
    public class Task1Test
    {
        [TestMethod]
        public void TestIncrement()
        {
            Expression<Func<int, int>> source_exp = (a) => a + (a + 1) * (a + 5) * (a + 1);
            var result = new AddToIncrementTransform().VisitAndConvert(source_exp,"");
            var result_value = result.Compile().Invoke(2);
            Assert.AreEqual(65, result_value);
            
        }
        [TestMethod]
        public void TestDecrement()
        {
            Expression<Func<int, int>> source_exp = (a) => a + (a - 1) * (a - 5) * (a - 1);
            var result = new AddToDecrementTransform().VisitAndConvert(source_exp, "");
            var result_value = result.Compile().Invoke(2);
            Assert.AreEqual(-1, result_value);
        }
        
        [TestMethod]
        public void TestParameters()
        {
            Expression<Func<int,int,int>> source_exp = (a,b) => a + (a - 1) * (b - 5) * (a - 1);
            var param = new Dictionary<string, string> { {"a","c" }, { "b", "d" } };
            var result = new ReplaceParametersTypeVisitor().VisitReplaceParameters(source_exp,param);
            var result_text = result.ToString();
            Assert.AreEqual("(c, d) => (c + (((c - 1) * (d - 5)) * (c - 1)))", result_text);
        }
    }
    
}
