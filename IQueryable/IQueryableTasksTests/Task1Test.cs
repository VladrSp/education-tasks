using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using IQueryableTasks.Task1;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IQueryableTasksTests
{
    [TestClass]
    public class Task1Test
    {
        [TestMethod]
        public void TestIncrement()
        {
            Expression<Func<int, int>> source_exp = (a) => a + (a + 1) * (a + 5) * (a + 1);
            var result = new ParameterTransformVisitor().VisitAndConvert(source_exp,"");
            var result_value = result.Compile().Invoke(2);
            Assert.AreEqual(65, result_value);            
        }
        [TestMethod]
        public void TestDecrement()
        {
            Expression<Func<int, int>> source_exp = (a) => (a + 1) * (a - 5) * (a - 1);
            var result = new ParameterTransformVisitor().VisitAndConvert(source_exp, "");
            var result_value = result.Compile().Invoke(2);
            Assert.AreEqual(-9, result_value);
        }
        
        [TestMethod]
        public void TestParameters()
        {
            Expression<Func<int,int,int>> source_exp = (a,b) => a + (a - 1) * (b - 5) * (a + 1);
            var param = new Dictionary<string, string> { {"a","c" } };
            var result = new ParameterTransformVisitor(param).VisitAndConvert(source_exp,"");
            var result_text = result.ToString();
            Assert.AreEqual("(c, b) => (c + ((Decrement(c) * (b - 5)) * Increment(c)))", result_text);

            var arg = Expression.Parameter(typeof(int), "c");
            var exp = Expression.Add(arg, Expression.Constant(7));
            Func<int, int> result_comp = Expression.Lambda<Func<int, int>>(exp, arg).Compile();

            var result_value = result_comp.Invoke(3);
            Assert.AreEqual(10, result_value);
        }
    }
    
}
