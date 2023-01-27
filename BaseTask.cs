using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todolist
{
    class BaseTask
    {
        private DateTime date;
        private bool finish;
        private string task;

        public BaseTask()
        {
            finish = false;
            task = null;
            date = DateTime.Now;
        }
        public BaseTask(string task):this()
        {
           this.task = task;
        }
        public bool Finish
        {
            get => finish;
            set
            {
                if (this.GetType() == typeof(Todolist))
                    Console.WriteLine("Error");
                else
                    finish = value;
            }
        }
        public string Task
        {
            get => task;
            set => task = value;
        }
        public DateTime Date { get => date; set => date = value; }
    }
}
