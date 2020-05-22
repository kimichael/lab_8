using System;
using System.Collections.Generic;
using System.IO;
using lab_8;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab_8_tests
{
    [TestClass]
    public class Tests
    {
        private static List<PCStruct> _items;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            _items = new List<PCStruct>()
            {
                new PCStruct() { Code = "ABC", Manufacturer = "ABC", Proc = "ABC", Freq = 3, Mem = 4, HDD = 1000, Video = 4, Price = 1000, Count = 10 },
                new PCStruct() { Code = "BCD", Manufacturer = "BCD", Proc = "BCD", Freq = 2, Mem = 3, HDD = 500, Video = 3, Price = 500, Count = 5 },
                new PCStruct() { Code = "CDE", Manufacturer = "CDE", Proc = "CDE", Freq = 1, Mem = 2, HDD = 250, Video = 2, Price = 250, Count = 2 },
                new PCStruct() { Code = "DEF", Manufacturer = "DEF", Proc = "DEF", Freq = 0.5, Mem = 1, HDD = 125, Video = 1, Price = 125, Count = 1 },
                new PCStruct() { Code = "", Manufacturer = "", Proc = "", Freq = 0, Mem = 0, HDD = 0, Video = 0, Price = 0, Count = 0 },
                new PCStruct() { Code = "  ", Manufacturer = "  ", Proc = "  ", Freq = 0, Mem = 0, HDD = 0, Video = 0, Price = 0, Count = 0 },
                new PCStruct() { Code = "abc", Manufacturer = "abc", Proc = "abc", Freq = 0, Mem = 0, HDD = 0, Video = 0, Price = 0, Count = 0 },
                new PCStruct() { Code = "AbC", Manufacturer = "aBC", Proc = "ABc", Freq = 0, Mem = 0, HDD = 0, Video = 0, Price = 0, Count = 0 },
                new PCStruct() { Code = "   abc", Manufacturer = "abc    ", Proc = "   abc   ", Freq = 0, Mem = 0, HDD = 0, Video = 0, Price = 0, Count = 0 },
                new PCStruct() { Code = "wqerqwabcqwerqwr", Manufacturer = "23tabc13245", Proc = "aaabcccc", Freq = 0, Mem = 0, HDD = 0, Video = 0, Price = 0, Count = 0 }
            };
        }

        [TestMethod]
        public void TestFilter1()
        {
            var filter = new Filter();
            filter.AddPredicate(Filter.ContainsPredicate, item => item.Code, "ABC");

            var expected = new List<PCStruct>() { _items[0], _items[6], _items[7], _items[8], _items[9] };
            var result = filter.FilterItems(_items);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestFilter2()
        {
            var filter = new Filter();
            filter.AddPredicate(Filter.ContainsPredicate, item => item.Code, "");

            var expected = new List<PCStruct>(_items);
            var result = filter.FilterItems(_items);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestFilter3()
        {
            var filter = new Filter();
            filter.AddPredicate(Filter.ContainsPredicate, item => item.Code, " ");

            var expected = new List<PCStruct>() { _items[5], _items[8] };
            var result = filter.FilterItems(_items);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestFilter4()
        {
            var filter = new Filter();
            filter.AddPredicate(Filter.GreaterThanOrEqualToPredicate, item => item.Freq, 2);

            var expected = new List<PCStruct>() { _items[0], _items[1] };
            var result = filter.FilterItems(_items);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestFilter5()
        {
            var filter = new Filter();
            filter.AddPredicate(Filter.LessThanOrEqualToPredicate, item => item.Freq, 1);

            var expected = new List<PCStruct>() { _items[2], _items[3], _items[4], _items[5], _items[6], _items[7], _items[8], _items[9] };
            var result = filter.FilterItems(_items);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestXmlFileManager1()
        {
            var xmlFileManager = new XmlFileManager();

            File.Delete("someRandomFile.xml");
            Assert.ThrowsException<FileNotFoundException>(() => xmlFileManager.Load("someRandomFile.xml"));
        }

        [TestMethod]
        public void TestXmlFileManager2()
        {
            var xmlFileManager = new XmlFileManager();

            File.Delete("someRandomFile.xml");
            xmlFileManager.Save(_items, "someRandomFile.xml");

            Assert.IsTrue(File.Exists("someRandomFile.xml"));
        }

        [TestMethod]
        public void TestXmlFileManager3()
        {
            var xmlFileManager = new XmlFileManager();

            var expected = new List<PCStruct>();

            File.Delete("someRandomFile.xml");
            xmlFileManager.Save(expected, "someRandomFile.xml");

            var result = new List<PCStruct>(xmlFileManager.Load("someRandomFile.xml"));

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestXmlFileManager4()
        {
            var xmlFileManager = new XmlFileManager();

            var expected = new List<PCStruct>(_items);

            File.Delete("someRandomFile.xml");
            xmlFileManager.Save(expected, "someRandomFile.xml");

            var result = new List<PCStruct>(xmlFileManager.Load("someRandomFile.xml"));

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestPCStruct1()
        {
            var expected = new PCStruct() { Code = "ABC", Manufacturer = "ABC", Proc = "ABC", Freq = 3, Mem = 4, HDD = 1000, Video = 4, Price = 1000, Count = 10 };
            var element = expected.GetXElement();
            var result = PCStruct.FromXElement(element);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestPCStruct2()
        {
            var item = new PCStruct() { Code = "ABC", Manufacturer = "ABC", Proc = "ABC", Freq = 3, Mem = 4, HDD = 1000, Video = 4, Price = 1000, Count = 10 };
            var element = item.GetXElement();
            element.SetAttributeValue("Freq", "3.5");
            Assert.ThrowsException<FormatException>(() => PCStruct.FromXElement(element));
        }
    }
}
