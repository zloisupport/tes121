using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tes121
{
    public class Orders
    {  
        [Key]
        public int order_id { get; set; }


       // [ForeignKey("Sotrudnik")]
        public int manager_id { get; set; }

       // public Sotrudni Sotrudnik { get; set; }

        public int client_id { get; set; }


        [ForeignKey("Category")]
        public int cat_id { get; set; }
        public Category Category { get; set; }


        public DateTime date_orders { get; set; }
    }
}
