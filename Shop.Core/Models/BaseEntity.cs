using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Models
{
    public  class BaseEntity
    {
        public string Id { get; set; }
        public DateTimeOffset createdAt { get; set; }
        
        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.createdAt = DateTimeOffset.Now;
        }
     }
}
