using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApp.Models.Entities
{
    public class GuestBookEntry
    {
        public virtual int Id { get; set; }
        public virtual string Text { get; set; }
    }
}
