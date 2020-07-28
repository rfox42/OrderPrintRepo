using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderValidation
{
    class Transaction
    {
        public string bin;
        public string part;
        public string user;
        public string date;

        public int qty;


        public Transaction()
        {
            bin = null;
            part = null;
            user = null;
            date = null;
            qty = 0;
        }

        ~Transaction()
        {

        }
    }
}
