using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todolist
{
    interface Itask
    {
         DateTime Date { get; set; }
         bool Finish { get; set; }
         string Task { get; set; }
         void Print();
         void Add(List<Itask> itasks, int pos = 0);
        void Delete(List<Itask> itasks, int TaskIndex, int SubIndex = 0);

    }
}
