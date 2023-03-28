using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Orders
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public Employee Employee { get; set; }


        public Orders()
        {
            Employee = new Employee();
            Customer = new Customer();
            Product = new Product();
        }
    }
}
