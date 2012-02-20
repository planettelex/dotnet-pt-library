using System;
using System.Globalization;
using System.Xml.Serialization;
using PlanetTelex.Utilities;

namespace PlanetTelex.Common.Models
{
    /// <summary>
    /// A credit card.
    /// </summary>
    [Serializable]
    public class CreditCard
    {
        private readonly StringUtility _stringUtility;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCard"/> class.
        /// </summary>
        public CreditCard()
        {
            _stringUtility = new StringUtility();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCard"/> class.
        /// </summary>
        /// <param name="creditCardNumber">The credit card number.</param>
        public CreditCard(string creditCardNumber)
        {
            _stringUtility = new StringUtility();
            CreditCardNumber = creditCardNumber;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the credit card number.
        /// </summary>
        /// <value>
        /// The full credit card number, ignored in XML serialization for security purposes.
        /// </value>
        [XmlIgnore]
        public string CreditCardNumber { get; set; }

        /// <summary>
        /// Gets or sets the name on the credit card used in this transaction.
        /// </summary>
        /// <value>
        /// The name of the credit card's owner.
        /// </value>
        public string CreditCardName { get; set; }

        /// <summary>
        /// Gets or sets the card verification number (CVN).
        /// </summary>
        /// <value>
        /// The credit card CVN number, the 3 or 4 digit security number that cannot be imprinted, often on the back of a card.
        /// </value>
        public string CreditCardCvn { get; set; }

        /// <summary>
        /// Gets or sets the credit card expiration date.
        /// </summary>
        /// <value>
        /// The expiration date on the card.
        /// </value>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Gets the type of credit card used in this transaction, derived from the CreditCardNumber.
        /// </summary>
        /// <value>
        /// The type of the card.
        /// </value>
        public CreditCardType CardType
        {
            get
            {
                return DetermineCardType();
            }
        }

        /// <summary>
        /// Gets the last four digits of the credit card number as an integer, returning zero if there isn't a number.
        /// </summary>
        public int LastFourDigits
        {
            get
            {
                string lastFour = (CreditCardNumber == null || CreditCardNumber.Length < 4) ? "0" : CreditCardNumber.Substring(CreditCardNumber.Length - 4);
                return Convert.ToInt32(lastFour);
            }
        }

        /// <summary>
        /// Gets the string format specified by Settings.CreditCardDateFormatString, which defaults to "MMyy".
        /// </summary>
        public string ExpirationDateString
        {
            get
            {
                return ExpirationDate.ToString(Settings.Current.CreditCardDateFormatString, CultureInfo.CurrentUICulture.DateTimeFormat);
            }
        }

        /// <summary>
        /// Runs the CheckSum algorithm on this instance's credit card number, returning true if it passes, false otherwise.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this credit card passes the CheckSum test; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get
            {
                return Checksum();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines if this instance's is the same type as the one specified.
        /// </summary>
        /// <param name="cardType">Type of the card.</param>
        /// <returns><c>true</c> if this credit card is of the supplied type; otherwise, <c>false</c>.</returns>
        public bool ValidateCardType(CreditCardType cardType)
        {
            return (cardType == DetermineCardType());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The CheckSum algorithm for validating credit card numbers.
        /// </summary>
        private bool Checksum()
        {
            // Verify it is numeric.
            if (!_stringUtility.IsNumeric(CreditCardNumber))
                return false;

            int cardSize = CreditCardNumber.Length;

            // Number length must be between 13 and 16.
            if (cardSize >= 13 && cardSize <= 16)
            {
                int odd = 0;
                int even = 0;

                // Read the credit card number into an array.
                char[] cardNumberArray = CreditCardNumber.ToCharArray();

                // Reverse the array
                Array.Reverse(cardNumberArray, 0, cardSize);

                // Multiply every second number by two and get the sum. 
                // Get the sum of the rest of the numbers. 
                for (int i = 0; i < cardSize; i++)
                {
                    if (i % 2 == 0)
                    {
                        // GetValue() returns the ASCII value, the ASCII value for "0" is 48 and "1" is 49 and so on.
                        odd += (Convert.ToInt32(cardNumberArray.GetValue(i)) - 48);
                    }
                    else
                    {
                        int temp = (Convert.ToInt32(cardNumberArray[i]) - 48) * 2;
                        // If the value is greater than 9, substract 9 from the value.
                        if (temp > 9)
                            temp = temp - 9;

                        even += temp;
                    }
                }
                return (odd + even) % 10 == 0;
            }
            return false;
        }

        /// <summary>
        /// Determines if this instance's credit card type based on the number.
        /// </summary>
        private CreditCardType DetermineCardType()
        {
            if (string.IsNullOrEmpty(CreditCardNumber))
                return CreditCardType.None;

            if (IsVisa())
                return CreditCardType.Visa;
            if (IsMastercard())
                return CreditCardType.Mastercard;
            if (IsAmericanExpress())
                return CreditCardType.American_Express;
            if (IsDiscover())
                return CreditCardType.Discover;
            if (IsDinersClub())
                return CreditCardType.Diners_Club;
            if (IsJcb())
                return CreditCardType.Jcb;
            
            return CreditCardType.Unknown;
        }

        private int FirstNumber { get { return Convert.ToInt32(CreditCardNumber.Substring(0, 1)); } }
        private int SecondNumber { get { return Convert.ToInt32(CreditCardNumber.Substring(1, 1)); } }
        private int FirstThreeDigits { get { return Convert.ToInt32(CreditCardNumber.Substring(0, 3)); } }

        private bool IsVisa()
        {
            return (FirstNumber == 4 && (CreditCardNumber.Length == 13 || CreditCardNumber.Length == 16));
        }

        private bool IsMastercard()
        {
            return (FirstNumber == 5 && (SecondNumber > 0 && SecondNumber < 6) && CreditCardNumber.Length == 16);
        }

        private bool IsAmericanExpress()
        {
            return ((CreditCardNumber.StartsWith("34") || CreditCardNumber.StartsWith("37")) && CreditCardNumber.Length == 15);
        }

        private bool IsDiscover()
        {
            return (CreditCardNumber.StartsWith("6011") && CreditCardNumber.Length == 16);
        }

        private bool IsDinersClub()
        {
            return ((CreditCardNumber.StartsWith("36") || CreditCardNumber.StartsWith("38") || (FirstThreeDigits >= 300 && FirstThreeDigits <= 305)) && CreditCardNumber.Length == 14);
        }

        private bool IsJcb()
        {
            return (((CreditCardNumber.StartsWith("2131") || CreditCardNumber.StartsWith("1800")) && CreditCardNumber.Length == 15) || (CreditCardNumber.StartsWith("3") && CreditCardNumber.Length == 16));
        }

        #endregion
    }
}