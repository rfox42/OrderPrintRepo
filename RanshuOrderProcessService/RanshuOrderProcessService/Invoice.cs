using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RanshuOrderProcessService
{
    class Invoice
    {
        public int invoiceNum;
        public string account;
        public string PONum;
        public string deliveryMethod;
        public Address billAddress, shipAddress, vendAddress;
        public List<Item> items;
        public bool reviewFlag;
        public string reviewMsg;
        public string fileName;
        public string attention;
        public string terms;
        public string taxKey;
        public int termNum;
        public string taxYN;
        public double subTotal, total;
        public double taxRate, tax, freight;

        public Invoice()
        {
            invoiceNum = 0;
            total = 0.0f;
            subTotal = 0.0f;
            tax = 0.0f;
            taxRate = 0.0f;
            freight = 0.0f;
            fileName = null;
            reviewMsg = null;
            reviewFlag = false;
            PONum = null;
            attention = "";
            terms = null;
            taxYN = "N";
            taxKey = "N";
            deliveryMethod = null;
            billAddress = null;
            shipAddress = null;
            vendAddress = null;
        }

        ~Invoice()
        {

        }

        public void calcTotals()
        {
            subTotal = 0.00f;
            foreach(Item item in items)
            {
                subTotal += item.ext;
            }

            if(taxYN == "Y")
                tax = (taxRate / 100) * subTotal;

            total = subTotal + tax + freight;
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
        public double price;

        /// <summary>
        /// number of parts for order
        /// BKAR_INVL_PQTY
        /// </summary>
        public int quantity;

        /// <summary>
        /// quantity x price value for item
        /// </summary>
        public double ext;

        public string location;

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

        public string print()
        {
            string data = quantity + " " + partCode + " " + price + " " + ext;

            return data;
        }

        /// <summary>
        /// destructor
        /// </summary>
        ~Item()
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

        public string getCondensedAddress()
        {
            string condensedAddress = name;

            if(line1 != "")
                condensedAddress += "\n" + line1;
            if (line2 != "")
                condensedAddress += "\n" + line2;
            if (city != "")
                condensedAddress += "\n" + city;
            if (state != "")
                condensedAddress += "\n" + state;
            if (zipcode != "")
                condensedAddress += "\n" + zipcode;
            return condensedAddress;
        }

        /// <summary>
        /// constructor
        /// </summary>
        public Address()
        {
            name = "";
            line1 = "";
            line2 = "";
            city = "";
            state = "";
            country = "";
            zipcode = "";
        }

        /// <summary>
        /// destructor
        /// </summary>
        ~Address()
        {

        }
    }
}
