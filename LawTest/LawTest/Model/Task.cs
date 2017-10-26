using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawTest.Model
{
    public class Task
    {
        public string TaskDescription { get; set; }
        public int CorrectAnswer { get; set; }
        public Dictionary<int, string> Choices { get; set; }

    }
}
