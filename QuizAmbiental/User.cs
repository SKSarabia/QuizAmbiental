using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAmbiental.Models
{
    public class User
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Username => $"{Name}{Age}";
        public override string ToString() => Username;
    }
}