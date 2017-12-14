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
