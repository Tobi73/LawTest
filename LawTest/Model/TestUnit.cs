using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawTest.Model
{
    public class TestUnit
    {
        public string TestName { get; set; }
        public List<Task> Tasks { get; set; }

        public TestUnit(string testName)
        {
            TestName = testName;
            Tasks = new List<Task>();
        }

        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }

        public override string ToString()
        {
            return TestName;
        }
    }
}
