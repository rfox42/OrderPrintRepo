using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditProcessApp
{
    /* 
     * @CLASS: class Invoice
     * @PURPOSE: holds invoice data for credit processing
     * 
     * @NOTES: none
     */
    class Invoice
    {
        /// <summary>
        /// holds invoice number
        /// </summary>
        public int invoiceNumber;

        public int salesPerson;

        /// <summary>
        /// holds total cost of order
        /// </summary>
        public double total;

        /// <summary>
        /// holds account string
        /// </summary>
        public string account;

        /// <summary>
        /// holds date order was placed
        /// </summary>
        public string date;

        /// <summary>
        /// holds date/time account was charged/processed
        /// </summary>
        public string chargeTime;

        /// <summary>
        /// holds username of user processing invoices
        /// </summary>
        public string user;

        /// <summary>
        /// holds order notes
        /// </summary>
        public string notes;

        public string deliveryMethod;

        public char retail;

        public string location;



        /*
         * @FUNCTION:   public Invoice()
         * @PURPOSE:    class constructor
         *              
         * @PARAM:      none
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        public Invoice()
        {

        }

        ~Invoice()
        {

        }

        public void setLocation(string loc)
        {
            switch(loc)
            {
                case "FORT WORTH":
                    location = "FW";
                    break;

                case "RENO":
                    location = "R";
                    break;

                case "SPARKS1":
                    location = "S1";
                    break;

                case "SPARKS2":
                    location = "S2";
                    break;

                case "TX COSIGN":
                    location = "TXC";
                    break;

                case "PA":
                    location = loc;
                    break;

                case "FL":
                    location = loc;
                    break;
            }
        }
    }
}
