using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderPrintForms
{
    class Order
    {
        /// <summary>
        /// list of items in the order
        /// </summary>
        public List<Item> items;

        /// <summary>
        /// customer billing address
        /// </summary>
        public Address billAddress;

        /// <summary>
        /// customer shipping address
        /// </summary>
        public Address shipAddress;

        /// <summary>
        /// order invoice number
        /// BKAR_INV_NUM
        /// </summary>
        public int invoiceNumber;

        /// <summary>
        /// date order was placed
        /// BKAR_INV_INVDTE
        /// </summary>
        public string date;

        /// <summary>
        /// holds customer code/company name
        /// BKAR_INV_CUSCOD
        /// </summary>
        public string customerCode;

        /// <summary>
        /// holds nearest location to ship address
        /// BKAR_INV_LOC
        /// </summary>
        public string location;

        /// <summary>
        /// BKAR_INV_CUSORD
        /// </summary>
        public string customerPO;

        /// <summary>
        /// holds order delivery method
        /// BKAR_INV_SHPVIA
        /// </summary>
        public string deliveryMethod;

        /// <summary>
        /// holds the payment method for the order
        /// BKAR_INV_TERMD
        /// </summary>
        public string paymentTerms;

        /// <summary>
        /// holds initials of employee that approved order
        /// BKAR_INV_ENTBY
        /// </summary>
        public string enteredBy;

        public Order(int invNum)
        {
            invoiceNumber = invNum;
            items = new List<Item>();
            shipAddress = new Address();
            billAddress = new Address();
        }

        ~Order()
        {
        }
    }

    public class Address
    {
        /// <summary>
        /// name of recipient
        /// </summary>
        public string name;

        /// <summary>
        /// address line 1
        /// </summary>
        public string line1;

        /// <summary>
        /// address line 2
        /// </summary>
        public string line2;

        /// <summary>
        /// recipient city
        /// </summary>
        public string city;

        /// <summary>
        /// recipient state
        /// </summary>
        public string state;

        /// <summary>
        /// recipient country
        /// </summary>
        public string country;

        /// <summary>
        /// recipient zipcode
        /// </summary>
        public string zipcode;


        public Address()
        {

        }

        ~Address()
        {

        }
    }

    public class Item
    {
        /// <summary>
        /// item part code
        /// BKAR_INVL_PCODE
        /// </summary>
        public string partCode;

        /// <summary>
        /// type of item being sent
        /// can also be a message "X" or non-stock item "N"
        /// BKAR_INVL_ITYPE
        /// </summary>
        public string itemType;

        /// <summary>
        /// part description
        /// BKAR_INVL_PDESC
        /// </summary>
        public string description;

        /// <summary>
        /// message regarding the part
        /// BKAR_INVL_MSG
        /// </summary>
        public string message;

        /// <summary>
        /// holds bin location of part
        /// wmsLocations.BIN_NAME
        /// </summary>
        public string location;

        /// <summary>
        /// location code of warehouse
        /// BKAR_INVL_LOC
        /// </summary>
        public string locationCode;

        /// <summary>
        /// custom part number
        /// BKIC_VND_PART
        /// </summary>
        public string vendorPart;

        /// <summary>
        /// number of parts for order
        /// BKAR_INVL_PQTY
        /// </summary>
        public int quantity;

        public Item()
        {

        }

        ~Item()
        {

        }
    }
}
