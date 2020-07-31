using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace RanshuPrintService
{
    /// <summary>
    /// holds data for each invoice
    /// </summary>
    public class Order
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

        /// <summary>
        /// order subtotal
        /// </summary>
        public double subTotal;

        /// <summary>
        /// freight cost of order
        /// </summary>
        public double freight;

        /// <summary>
        /// order total
        /// </summary>
        public double total;

        /// <summary>
        /// order taxes
        /// </summary>
        public double tax;

        /// <summary>
        /// order salesperson
        /// </summary>
        public int salesPerson;

        /// <summary>
        /// order posted flag
        /// </summary>
        public bool posted;

        /// <summary>
        /// order credit flag
        /// </summary>
        public bool postCred;

        /// <summary>
        /// order reprint flag
        /// </summary>
        public bool reprint;
        internal string reprintLoc;

        /// <summary>
        /// class constructor
        /// </summary>
        /// <param name="invNum"></param>
        public Order(int invNum)
        {
            posted = false;
            postCred = false;
            reprint = false;
            reprintLoc = null;
            invoiceNumber = invNum;
            items = new List<Item>();
            shipAddress = new Address();
            billAddress = new Address();
        }

        /// <summary>
        /// class destructor
        /// </summary>
        ~Order()
        {
        }
    }

    /// <summary>
    /// holds address data
    /// </summary>
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

        /// <summary>
        /// equals operator overload
        /// </summary>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <returns></returns>
        public static bool operator ==(Address address1, Address address2)
        {
            return ((address1.name.Contains(address2.name) || address2.name.Contains(address1.name))
                || address1.line1 == address2.line1);
        }

        /// <summary>
        /// not eqauls operator overload
        /// </summary>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <returns></returns>
        public static bool operator !=(Address address1, Address address2)
        {
            return !((address1.name.Contains(address2.name) || address2.name.Contains(address1.name))
                || address1.line1 == address2.line1);
        }

        /// <summary>
        /// equals operator overload
        /// </summary>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Address address2 = obj as Address;
            return ((name.Contains(address2.name) || address2.name.Contains(name))
                || line1 == address2.line1);
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// constructor
        /// </summary>
        public Address()
        {

        }

        /// <summary>
        /// destructor
        /// </summary>
        ~Address()
        {

        }
    }

    /// <summary>
    /// holds item details
    /// </summary>
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
        public Location location;

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
        /// price of part
        /// BKAR_INVL_PPRCE
        /// </summary>
        public string price;

        /// <summary>
        /// number of parts for order
        /// BKAR_INVL_PQTY
        /// </summary>
        public int quantity;

        /// <summary>
        /// constructor
        /// </summary>
        public Item()
        {

        }

        /// <summary>
        /// copy constructor
        /// </summary>
        /// <param name="item"></param>
        public Item(Item item)
        {
            partCode = item.partCode;
            itemType = item.itemType;
            description = item.description;
            message = item.message;
            locationCode = item.locationCode;
            vendorPart = item.vendorPart;
            price = item.price;
            quantity = item.quantity;
        }

        /// <summary>
        /// destructor
        /// </summary>
        ~Item()
        {

        }
    }

    /// <summary>
    /// holds inventory location details
    /// </summary>
    public class Location
    {
        /// <summary>
        /// location name
        /// </summary>
        public string name;

        /// <summary>
        /// type OS, PF, or NP
        /// </summary>
        public string type;

        /// <summary>
        /// total quantity at location
        /// </summary>
        public int quantity;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="inName"></param>
        /// <param name="inType"></param>
        /// <param name="inQty"></param>
        public Location(string inName, string inType, int inQty)
        {
            name = inName;
            type = inType;
            quantity = inQty;
        }

        /// <summary>
        /// destructor
        /// </summary>
        ~Location()
        {

        }
    }

    /// <summary>
    /// holds default printer
    /// </summary>
    public static class printers
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Name);
    }
}
