using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todolist
{
    class SubTask : Tasks
    {
        private int numbertask;
        protected int NumberTask
        {
            get => numbertask;
            set => numbertask = value;
        }
        public SubTask() : base() { }
        public SubTask(int number, string Subtask) : base(Subtask) => numbertask = number;
        public override void Print() => Console.WriteLine($"{numbertask}.Create: {Date}  {Task} Status:{finish}");

    }
}
