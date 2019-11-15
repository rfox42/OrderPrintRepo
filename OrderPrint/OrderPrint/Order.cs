using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderPrint
{
    class Order
    {
        public List<Item> items;
        public Address billAddress;
        public Address shipAddress;

        public int invoiceNumber;

        public string date;
        public string customerCode;
        public string location;
        public string customerPO;
        public string deliveryMethod;
        public string paymentTerms;
        public string enteredBy;

        public bool isCOD;

        public Order(int invNum)
        {
            items = new List<Item>();
            shipAddress = new Address();
            billAddress = new Address();
            isCOD = false;
        }

        ~Order()
        {
        }
    }

    class Address
    {
        public string name;
        public string line1;
        public string line2;
        public string city;
        public string state;
        public string country;
        public int zipcode;


        public Address()
        {

        }

        ~Address()
        {

        }
    }

    class Item
    {
        public string partCode;
        public string itemType;
        public string description;
        public string message;
        public string location;
        public string locationCode;
        public string vendorPart;
        public int quantity;

        public Item()
        {

        }

        ~Item()
        {

        }
    }
}
