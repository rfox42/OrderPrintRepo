using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditProcessApp
{
    class Payment
    {
        public string cardNum;
        public string expiration;
        public string cardNotes;

        public Payment(string inNum = "", string inExp = "", string inNotes = "")
        {
            cardNum = inNum;
            expiration = inExp;
            cardNotes = inNotes;
        }

        ~Payment()
        {

        }
    }

    class Account
    {
        List<Payment> paymentMethods;
        int paymentIndex;
        int count;

        public Account(List<Payment> payments)
        {
            paymentMethods = payments;
            paymentIndex = 0;
        }

        ~Account()
        {

        }

        public Payment getPayment()
        {
            return paymentMethods[paymentIndex];
        }

        public Payment nextPayment()
        {
            if(paymentMethods.Count > paymentIndex + 1)
            {
                paymentIndex++;
                return paymentMethods[paymentIndex];
            }
            else
            {
                return null;
            }
        }

        public Payment previousPayment()
        {
            if (paymentMethods.Count > paymentIndex - 1)
            {
                paymentIndex--;
                return paymentMethods[paymentIndex];
            }
            else
            {
                return null;
            }
        }
    }
}
