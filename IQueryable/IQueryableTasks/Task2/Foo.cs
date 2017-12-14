using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQueryableTasks.Task2
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Beer BeerItem { get; set; }
        public int Count { get; set; }
    }
}
