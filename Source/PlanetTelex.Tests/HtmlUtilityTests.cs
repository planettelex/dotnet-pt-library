using NUnit.Framework;

namespace PlanetTelex.UnitTests
{
    [TestFixture]
    public class HtmlUtilityTests
    {
        readonly string withHtml = "<p>The rain in <b>Spain</b> falls mainly on the <a href='http://www.google.com'>plains</a>.</p>";
        readonly string noHtml = "The rain in Spain falls mainly on the plains.";

        [Test]
        public void StripTagsTest()
        {
            Assert.That(string.Compare(HtmlUtility.StripTags(withHtml, false), noHtml) == 0);
        }

        [Test]
        public void ContainsHtmlTest()
        {
            Assert.That(HtmlUtility.ContainsHtml(withHtml));
            Assert.That(HtmlUtility.ContainsHtml(noHtml) == false);
        }
    }
}
