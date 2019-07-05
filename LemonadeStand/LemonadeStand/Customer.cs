using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Customer
    {
        string customerName;

        public Customer()
        {
            customerName = SeededData.GenerateFullName();
        }
    }
}
