using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Todolist
{

    interface ITodoList
    {
        void Print();
        void DeleteTask(int pos);
        void CreateTask(Tasks task);
        public List<Tasks> List { get; }

        public  string Name { get; }

    }

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
            get { return task; }
        }
        protected DateTime Date
        {
            get { return date; }
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
        virtual public void Print()=> Console.WriteLine($"Create:{date}   {task}\tStatus:{Finish}");
        
        
    }
    class Todolist:Tasks,ITodoList
    {
        
        private List<Tasks> todolists;
        private List<SubTask> sublist;
        private string name;
        public  string Name { get;}
        public List<Tasks> List
        {
            get =>todolists; 
        }

        public Todolist():base()
        {
            todolists = null;
            sublist = null;
            name = null;
        }
        public Todolist(string name,Tasks newtask)
        {
            this.name = name;
            todolists = new List<Tasks>
            {
                newtask
            };

        }
        public void CreateTask(Tasks task)=>todolists?.Add(task);

        public override void Print()
        {
            try
            {
                if (todolists is null)
                    throw new Exception( "List is null");
                int index = 1;
                Console.WriteLine($"============Todolist============\n\nName: { name}\nTasks:");
                foreach (var item in todolists)
                {
                  
                    if(item.finish == true)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{index}.");
                    index++;
                    item.Print();
                    Console.ResetColor(); //

                }
                if (sublist.Count > 0 && sublist != null)
                {
                    Console.WriteLine("\n============SubTask============\n");
                    foreach (var item in sublist)
                    {
                        if (item.finish == true)
                            Console.ForegroundColor = ConsoleColor.Green;
                        else
                            Console.ForegroundColor = ConsoleColor.Red;
                        item.Print();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteTask(int pos)
        {
            
            try 
            {
                pos--;
                todolists?.RemoveAt(pos);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void CreateSubtask(int number,string text)
        {
            if(number > todolists.Count)
                Console.WriteLine("Out of range");
            else 
            {
                sublist = new List<SubTask> { new SubTask(number, text) };
            }
        }
        public void SetAccept(int number)
        {
           if(number > todolists.Count || number <0)
           {
                Console.WriteLine("Out of range");
           }
            else 
            {
                number--;
                todolists[number].finish = true;
            }
        }
    }
    class SubTask:Tasks
    {
        private int numbertask;
        protected int NumberTask
        {
            get => numbertask; 
            set => numbertask = value; 
        }
        public SubTask():base()  {}
        public SubTask(int number,string Subtask):base(Subtask)=>numbertask = number;
        public override void Print() => Console.WriteLine($"{numbertask}.Create: {Date}  {Task} Status:{finish}");
       
    }
    class Program
    {
        static void Main(string[] args)
        {

            Todolist list = new Todolist("Eugene", new Tasks("Learn cpp"));
            list.CreateTask(new Tasks("Cook cookies"));
            list.CreateTask(new Tasks("Chill"));
            list.CreateSubtask(2, "need to buy flour ");
            list.SetAccept(1);
            list.SetAccept(2);
            list.Print();
            
            Console.ReadLine();
        }
    }
    
}
