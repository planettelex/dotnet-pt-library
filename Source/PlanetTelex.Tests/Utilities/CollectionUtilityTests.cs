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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using NUnit.Framework;
using PlanetTelex.Common;
using PlanetTelex.Utilities;

namespace PlanetTelex.UnitTests.Utilities
{
    /// <summary>
    /// Unit tests for CollectionUtility
    /// </summary>
    [TestFixture]
    public class CollectionUtilityTests
    {
        readonly DummyObject _object1 = new DummyObject("Apple", 5, (decimal)1.12);
        readonly DummyObject _object2 = new DummyObject("Lime", 4, (decimal)1.67);
        readonly DummyObject _object3 = new DummyObject("Orange", 3, (decimal)1.99);
        readonly DummyObject _object4 = new DummyObject("Peach", 2, (decimal)0.34);
        readonly DummyObject _object5 = new DummyObject("Mango", 1, (decimal)-0.55);
        ArrayList _testList;
        CollectionUtility _collectionUtility;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _collectionUtility = new CollectionUtility();
            _testList = new ArrayList(5) {_object1, _object2, _object3, _object4, _object5};
        }

        /// <summary>
        /// Test Group for returning Dictionary objects of String and Decimal property.
        /// </summary>
        [Test]
        public void GroupTest()
        {
            Dictionary<object, ArrayList> dictionary = _collectionUtility.Group(_testList, "StringProperty");
            Assert.That(dictionary.ContainsKey("Lime"));
            Assert.That(dictionary.ContainsKey("Mango"));

            dictionary.Clear();
            dictionary = _collectionUtility.Group(_testList, "DecimalProperty");
            Assert.That(dictionary.ContainsKey((decimal)1.99));
            Assert.That(dictionary.ContainsKey((decimal)-0.55));
        }

        /// <summary>
        /// Sorts the int ascending.
        /// </summary>
        [Test]
        public void SortIntAscending()
        {
            ArrayList sortedList = _collectionUtility.SortArrayList(_testList, "IntProperty", Order.Ascending);

            DummyObject sortedObject1 = (DummyObject)sortedList[0];
            DummyObject sortedObject2 = (DummyObject)sortedList[1];
            DummyObject sortedObject3 = (DummyObject)sortedList[2];
            DummyObject sortedObject4 = (DummyObject)sortedList[3];
            DummyObject sortedObject5 = (DummyObject)sortedList[4];

            Assert.That(sortedObject1.IntProperty, Is.LessThan(sortedObject2.IntProperty));
            Assert.That(sortedObject2.IntProperty, Is.LessThan(sortedObject3.IntProperty));
            Assert.That(sortedObject3.IntProperty, Is.LessThan(sortedObject4.IntProperty));
            Assert.That(sortedObject4.IntProperty, Is.LessThan(sortedObject5.IntProperty));
        }

        /// <summary>
        /// Sorts the string ascending.
        /// </summary>
        [Test]
        public void SortStringAscending()
        {
            ArrayList sortedList = _collectionUtility.SortArrayList(_testList, "StringProperty", Order.Ascending);

            DummyObject sortedObject1 = (DummyObject)sortedList[0];
            DummyObject sortedObject2 = (DummyObject)sortedList[1];
            DummyObject sortedObject3 = (DummyObject)sortedList[2];
            DummyObject sortedObject4 = (DummyObject)sortedList[3];
            DummyObject sortedObject5 = (DummyObject)sortedList[4];

            Assert.That(sortedObject1.StringProperty, Is.EqualTo("Apple"));
            Assert.That(sortedObject2.StringProperty, Is.EqualTo("Lime"));
            Assert.That(sortedObject3.StringProperty, Is.EqualTo("Mango"));
            Assert.That(sortedObject4.StringProperty, Is.EqualTo("Orange"));
            Assert.That(sortedObject5.StringProperty, Is.EqualTo("Peach"));
        }

        /// <summary>
        /// Sorts the decimal descending.
        /// </summary>
        [Test]
        public void SortDecimalDescending()
        {
            ArrayList sortedList = _collectionUtility.SortArrayList(_testList, "DecimalProperty", Order.Descending);

            DummyObject sortedObject1 = (DummyObject)sortedList[0];
            DummyObject sortedObject2 = (DummyObject)sortedList[1];
            DummyObject sortedObject3 = (DummyObject)sortedList[2];
            DummyObject sortedObject4 = (DummyObject)sortedList[3];
            DummyObject sortedObject5 = (DummyObject)sortedList[4];

            Assert.That(sortedObject1.DecimalProperty, Is.GreaterThan(sortedObject2.DecimalProperty));
            Assert.That(sortedObject2.DecimalProperty, Is.GreaterThan(sortedObject3.DecimalProperty));
            Assert.That(sortedObject3.DecimalProperty, Is.GreaterThan(sortedObject4.DecimalProperty));
            Assert.That(sortedObject4.DecimalProperty, Is.GreaterThan(sortedObject5.DecimalProperty));
        }
        /// <summary>
        /// Test FieldList on testList for IntProperty, StringProperty and DecimalProperty.
        /// </summary>
        [Test]
        public void FieldListTest()
        {
            ArrayList alResult = _collectionUtility.ArrayListItemPropertyValues(_testList, "DecimalProperty");
            Assert.That(alResult[0].ToString() == "1.12" && alResult[4].ToString() == "-0.55");

            alResult = _collectionUtility.ArrayListItemPropertyValues(_testList, "IntProperty");
            Assert.That(alResult[0].ToString() == "5" && alResult[4].ToString() == "1");

            alResult = _collectionUtility.ArrayListItemPropertyValues(_testList, "StringProperty");
            Assert.That(alResult[0].ToString() == "Apple" && alResult[4].ToString() == "Mango");

        }
        /// <summary>
        /// Test Add of Dict1 to Dict2.
        /// </summary>
        [Test]
        public void AddTest()
        {
            //instantiate Dict1 and Dict2 (where Dict1 & Dict2 have 1st key value pair identical
            Dictionary<int, int> dicDict1 = new Dictionary<int, int>();
            Dictionary<int, int> dicDict2 = new Dictionary<int, int>();
            dicDict1[1] = 10;
            dicDict1[2] = 20;
            //dictionaries have 2 & 3 elements each
            dicDict2[4] = 100;
            dicDict2[5] = 200;
            dicDict2[6] = 300;
            //call Add to add Dict2 to Dict1, the result should have 5 elements
            Dictionary<int, int> dicResult = _collectionUtility.AddDictionary(dicDict1, dicDict2, false);
            Assert.That(dicResult.Count == 5 && dicResult[1] == 10 && dicResult[4] == 100 && dicResult[6] == 300);
            //change key=4 item to be 30 and run with Overwrite==true
            dicDict1[4] = 30;
            dicDict2[7] = 400;
            dicResult = _collectionUtility.AddDictionary(dicDict1, dicDict2, true);
            //test result should be 6 elements and item[4] should be back to 100
            Assert.That(dicResult[7] == 400 && dicResult[4]==100 && dicResult.Count == 6);
        }
        /// <summary>
        /// Test Reverse of Dict1 Keys and Values get interchanged in a New Dictionary
        /// </summary>
        [Test]
        public void ReverseTest()
        {
            Dictionary<int, string> dict1 = new Dictionary<int, string>();
            dict1[1] = "Dog";
            dict1[2] = "Cat";
            dict1[3] = "Zebra";
            Dictionary<string, int> dictReverse = _collectionUtility.SwapDictionaryKeysAndValues(dict1);
            Assert.That(dictReverse["Zebra"] == 3 && dictReverse["Cat"] == 2 && dictReverse["Dog"] == 1);
        }
        /// <summary>
        /// Test Unique - create duplicates in int and string collections, then remove them
        /// </summary>
        [Test]
        public void UniqueTest()
        { 
            Collection<int> coll = new Collection<int>();
            coll.Add(1);
            coll.Add(2);
            coll.Add(2);
            coll.Add(3);
            coll.Add(10);
            coll.Add(10);
            Collection<int> res = _collectionUtility.RemoveCollectionDuplicates(coll);
            Assert.That(res[0].ToString(CultureInfo.InvariantCulture) == "1" && res[2].ToString(CultureInfo.InvariantCulture) == "3" && res[3].ToString(CultureInfo.InvariantCulture) == "10" && res.Count == 4);
            Collection<string> strcoll = new Collection<string>();
            strcoll.Add("String1");
            strcoll.Add("String2");
            strcoll.Add("String1");
            strcoll.Add("String3");
            strcoll.Add("String1");
            Collection<string> strRes = _collectionUtility.RemoveCollectionDuplicates(strcoll);
            Assert.That(strRes[0] == "String1" && strRes[1] == "String2" && strRes[2] == "String3" && strRes.Count == 3);
        }
    }
}