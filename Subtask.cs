using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todolist
{
   public class SubTask : Tasks
    {
        private static int countsub;
     
        public SubTask() : base() { }
        public SubTask(string Subtask) : base(Subtask) 
        {
            countsub++;
        }
        public override void Print() { Console.Write("\t"); base.Print(); }
        

    }
}
