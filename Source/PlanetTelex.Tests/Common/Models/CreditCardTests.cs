using System;
using NUnit.Framework;
using PlanetTelex.Common.Models;

namespace PlanetTelex.UnitTests.Common.Models
{
    /// <summary>
    /// Unit tests for <see cref="CreditCard"/>.
    /// </summary>
    [TestFixture]
    public class CreditCardTests
    {
        const string VISA = "4388543049475174"; //length 15
        const string MASTERCARD = "5121075033109849"; //length 16
        const string AMEX = "372371574901901"; //length 15
        const string AMEX2 = "342371574901981"; //length 15
        const string DISCOVER = "6011298630901118"; //length 16
        const string DINERS = "36345678901288"; //length 14
        const string DINERS2 = "38345678901245";
        const string JCB = "213156789012344";  //length 15

        /// <summary>
        /// Test for IsValid to check if valid credit card number checksum for VI MA AMEX DISC DINERS JCB
        /// </summary>
        [Test]
        public void IsValidTest()
        {
            CreditCard cd = new CreditCard(VISA);
            Assert.That(cd.IsValid);
            CreditCard cd1 = new CreditCard(MASTERCARD);
            Assert.That(cd1.IsValid);
            CreditCard cd2 = new CreditCard(AMEX);
            Assert.That(cd2.IsValid);
            CreditCard cd2A = new CreditCard(AMEX2);
            Assert.That(cd2A.IsValid);
            CreditCard cd3 = new CreditCard(DISCOVER);
            Assert.That(cd3.IsValid);
            CreditCard cd4 = new CreditCard(DINERS);
            Assert.That(cd4.IsValid);
            CreditCard cd4A = new CreditCard(DINERS2);
            Assert.That(cd4A.IsValid);
            CreditCard cd5 = new CreditCard(JCB);
            Assert.That(cd5.IsValid);
        }

        /// <summary>
        /// Test for LastFourDigits
        /// </summary>
        [Test]
        public void LastFourDigitsTest()
        {
            CreditCard cd = new CreditCard(DISCOVER);
            Assert.That(cd.LastFourDigits == Int32.Parse("1118"));
        }

        /// <summary>
        /// Test for CardType Mastercard Visa Discover Diners_club AmEx Jcb
        /// </summary>
        [Test]
        public void CardTypeTest()
        {
            CreditCard cd = new CreditCard(MASTERCARD);
            CreditCard cd1 = new CreditCard(VISA);
            CreditCard cd2 = new CreditCard(DISCOVER);
            CreditCard cd3 = new CreditCard(DINERS);
            CreditCard cd4 = new CreditCard(JCB);
            CreditCard cd5 = new CreditCard(AMEX);

            Assert.That((cd5.CardType != CreditCardType.Mastercard) && (cd4.CardType != CreditCardType.Mastercard) &&
                (cd3.CardType != CreditCardType.Mastercard) && (cd2.CardType != CreditCardType.Mastercard) &&
                (cd1.CardType != CreditCardType.Mastercard) && (cd.CardType == CreditCardType.Mastercard));

            Assert.That((cd5.CardType != CreditCardType.Visa) && (cd4.CardType != CreditCardType.Visa) &&
                (cd3.CardType != CreditCardType.Visa) && (cd2.CardType != CreditCardType.Visa) &&
                (cd1.CardType == CreditCardType.Visa) && (cd.CardType != CreditCardType.Visa));

            Assert.That((cd5.CardType != CreditCardType.Discover) && (cd4.CardType != CreditCardType.Discover) &&
                (cd3.CardType != CreditCardType.Discover) && (cd2.CardType == CreditCardType.Discover) &&
                (cd1.CardType != CreditCardType.Discover) && (cd.CardType != CreditCardType.Discover));

            Assert.That((cd5.CardType != CreditCardType.Diners_Club) && (cd4.CardType != CreditCardType.Diners_Club) &&
                (cd3.CardType == CreditCardType.Diners_Club) && (cd2.CardType != CreditCardType.Diners_Club) &&
                (cd1.CardType != CreditCardType.Diners_Club) && (cd.CardType != CreditCardType.Diners_Club));

            Assert.That((cd5.CardType != CreditCardType.Jcb) && (cd4.CardType == CreditCardType.Jcb) &&
                (cd3.CardType != CreditCardType.Jcb) && (cd2.CardType != CreditCardType.Jcb) &&
                (cd1.CardType != CreditCardType.Jcb) && (cd.CardType != CreditCardType.Jcb));

            Assert.That((cd5.CardType == CreditCardType.American_Express) && (cd4.CardType != CreditCardType.American_Express) &&
                (cd3.CardType != CreditCardType.American_Express) && (cd2.CardType != CreditCardType.American_Express) &&
                (cd1.CardType != CreditCardType.American_Express) && (cd.CardType != CreditCardType.American_Express));
        }
    }
}