using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Core
{
    public class Entity
    {
        public long ID { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}
