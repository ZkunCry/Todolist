using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todolist
{
    class Tasks
    {
        private DateTime date;
        private bool Finish;
        private string task;

        public bool finish
        {
            get => Finish;
            set
            {
                if (this.GetType() == typeof(Todolist))
                    Console.WriteLine("Error");
                else
                    Finish = value;
            }
        }
        protected string Task
        {
            get => task;
        }
        protected DateTime Date
        {
            get => date;
        }

        public Tasks()
        {
            Finish = false;
            task = null;
        }

        public Tasks(string task)
        {
            this.task = task ?? null;
            Finish = false;
            date = DateTime.Now;
        }
        virtual public void Print() => Console.WriteLine($"Create:{date}   {task}\tStatus:{Finish}");
    }
}
