using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicioMVC.Business.Exceptions
{
    public class FileNullException : Exception
    {
        public string PropertyName { get; set; }
        public FileNullException(string name,string? message) : base(message)
        {
            PropertyName = name;
        }
    }
}
