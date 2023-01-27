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
        /*void Delete(int IndexTask, byte type, int IndexSubTask = 0);*/
        void Add(in Itask task,int pos=0);
        public List<Itask> List { get; }
        public string Name { get; }

    }


     class Todolist :ITodoList
    {

        private List<Itask> todolists;

        private string name;
        private int count;
        public int Count { get => count; }
        public string Name { get; }
        public List<Itask> List { get => todolists; }

        public Todolist() : base()
        {
            todolists = null;
            name = null;
            count = 0;
        }
        public Todolist(string name, in Tasks newtask)
        {
            this.name = name;
            todolists = new List<Itask>
                {
                    newtask
                };
            count++;
        }
        public void Add(in Itask task,int pos=0)
        {
            if (task is not null)
                task.Add(todolists, pos);
        }
   
        public void Delete(int TaskIndex, int SubIndex = -1)
        {
            if (SubIndex < -1 || TaskIndex == 0 || TaskIndex < 0)
                Console.WriteLine("Incorrect pos");
            else if (TaskIndex > 0 && SubIndex == -1)
            { TaskIndex--; todolists.RemoveAt(TaskIndex); }
            else if (TaskIndex > 0 && SubIndex > -1 && SubIndex != 0)
            { TaskIndex--;SubIndex--; ((Tasks)todolists[TaskIndex]).Subtasklist.RemoveAt(SubIndex); }
                
        }
        private static void ChangeColorConsole(Itask tasks)
        {
            if (tasks.Finish == true)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;
        }
        public  void Print()
        {
            try
            {
                if (todolists is null)
                    Console.WriteLine("List is null");
                else if (count == 0)
                    Console.WriteLine("List is empty\n");
                else
                {
                    int index = 1;
                    Console.WriteLine($"============Todolist============\n\nName: {name}\nTasks:");
                    foreach (var item in todolists)
                    {
                        ChangeColorConsole(item);
                        Console.Write($"{index}.");
                        index++;
                        item.Print();
                        if (((Tasks)item).Subtasklist.Count > 0)
                        {
                            Console.ResetColor();
                            Console.WriteLine("\t\t\t\tSubTasks");
                            ChangeColorConsole(item);
                            foreach (var value in ((Tasks)item).Subtasklist)
                                value.Print();
                        }
                        Console.WriteLine();
                        Console.ResetColor();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public void SetAccept(int number)
        {
            if (todolists == null || number > todolists?.Count || number < 0)
                Console.WriteLine("Out of range");
            else
            {
                number--;
                todolists[number].Finish = true;
                if (((Tasks)todolists[number]).Subtasklist.Count > 0)
                    foreach (var item in ((Tasks)todolists[number]).Subtasklist)
                        item.Finish = true;
            }
        }


    }

    class Program
        {
            static void Main(string[] args)
            {
            Todolist todolist = new Todolist("Eugene", new Tasks("Learn c++"));
            Itask sub = new SubTask("Buy book");
       
            todolist.Add(sub, 0);
            todolist.Add(new Tasks("Learn python"));
 
            todolist.SetAccept(1);
            todolist.Print();
            }
        }
    }


