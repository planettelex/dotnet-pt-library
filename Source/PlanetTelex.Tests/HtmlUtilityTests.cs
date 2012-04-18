/**
 * Copyright (c) 2012 Planet Telex Inc. all rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *         http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
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
