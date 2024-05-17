using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicioMVC.Business.Exceptions
{
    public class ImageFileNotFoundException : Exception
    {
        public string PropertyName { get; set; }
        public ImageFileNotFoundException(string Name,string? message) : base(message)
        {
            PropertyName = Name;
        }
    }
}
