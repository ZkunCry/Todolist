using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todolist
{
    public class Tasks
    {
        private DateTime date;
        private bool finish;
        private string task;
        private List<SubTask> subtasklist = new List<SubTask>();
        public List<SubTask> Subtasklist { get=>subtasklist; set =>subtasklist =value; }

        public static int _countTask;
        
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
        protected DateTime Date
        {
            get => date;
        }

        public Tasks()
        {
            finish = false;
            task = null;
        }

        public Tasks(string task)
        {
            this.task = task;
            Finish = false;
            date = DateTime.Now;
           
        }
        virtual public void Print() => Console.WriteLine($"Create:{date}   {task}");
        public void AddSubTask(in string SubTaskString) =>subtasklist.Add(new SubTask(SubTaskString)); 
    }
}
