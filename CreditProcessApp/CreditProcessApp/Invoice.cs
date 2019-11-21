﻿using System;
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
    }
}
