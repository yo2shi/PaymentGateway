using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentWebAPI.Models
{
    public class BankSimulator
    {

        public BankResponse ProcessCardPayment(CardDetails card, double amount)
        {
            BankResponse bankResponse = new BankResponse();

            Boolean isSuccessful = false;

            // Code to connect securely to bank's database or API should be here.
            string dbCardNumber;
            DateTime dbExpiryDate;
            //string Currency { get; set; }
            int dbCVV;

            //It is assumed that the customer has the following card details.
            dbCardNumber = "3000111122223333";
            dbExpiryDate = DateTime.Parse("31/12/2024");
            dbCVV = 177;

            // Retrieve customer's card balance or card limit. 
            // For the sake of simplicity card limit has been set to $250.
            double cardLimit = 250;

            // Check if card details are valid. 
            if (card.CardNumber == dbCardNumber
                && dbExpiryDate.Month == card.ExpiryDate.Month
                && dbExpiryDate.Year == card.ExpiryDate.Year
                && dbCVV == card.CVV
                && amount < cardLimit
                )
            {
                isSuccessful = true;
            } else { isSuccessful = false; }

            bankResponse.status = isSuccessful;
            bankResponse.transactionID = GenerateTransactionId();

            return bankResponse;
        }

        public string GenerateTransactionId()
        {
            string transactionId = "";
            Random rnd = new Random();
            string cardType = "VISA";
            transactionId = "MCB" + rnd.Next(99) + cardType + rnd.Next(99);

            // There should also be a code to check if the transaction ID is unique.

            return transactionId;
        }


    }

    public class BankResponse
    {
        public Boolean status { get; set; }
        public string transactionID { get; set; }

    }

}