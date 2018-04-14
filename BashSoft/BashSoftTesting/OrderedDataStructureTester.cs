using BashSoft.Contracts;
using BashSoft.DataStructures;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BashSoftTesting
{
    [TestFixture]
    public class OrderedDataStructureTester
    {
        private ISimpleOrderedBag<string> names;
        
        [Test]
        public void TestEmptyCtor()
        {
            this.names = new SimpleSortedList<string>();
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void TestCtorWithInitialCapacity()
        {
            this.names = new SimpleSortedList<string>(20);
            Assert.AreEqual(this.names.Capacity, 20);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void TestCtorWithAllParams()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase, 30);
            Assert.AreEqual(this.names.Capacity, 30);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void TestCtorWithInitialComparer()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase);
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }

        [SetUp]
        public void SetUp()
        {
            this.names = new SimpleSortedList<string>();
        }

        [Test]
        public void TestAddIncreasesSize()
        {
            this.names.Add("Pesho");
            Assert.AreEqual(1, this.names.Size);
        }

        [Test]
        public void TestAddNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => this.names.Add(null));
        }

        [Test]
        [TestCase("Rosen", "Georgi", "Balkan")]
        public void TestAddUnsortedDataIsHeldSorted(params string[] names)
        {
            foreach (var name in names)
            {
                this.names.Add(name);
            }

            string[] expectedOrderedCollection = new string[] { "Balkan", "Georgi", "Rosen" };

            CollectionAssert.AreEqual(expectedOrderedCollection, this.names);
        }

        [Test]
        public void TestAddingMoreThanInitialCapacity()
        {
            string[] namesToAdd = new string[17] { "1", "2", "3", "4", "5", "8", "9", "7", "5", "2", "1", "3", "4", "5", "6", "78", "8"};
            this.names.AddAll(namesToAdd);

            Assert.That(this.names.Size, Is.EqualTo(17));
            Assert.That(this.names.Capacity, Is.GreaterThan(16));
        }

        [Test]
        [TestCase("Pesho", "Gosho")]
        public void TestAddingAllFromCollectionIncreasesSize(params string[] namesToAdd)
        {
            List<string> elementsToAdd = new List<String>(namesToAdd);

            this.names.AddAll(elementsToAdd);

            Assert.AreEqual(2, this.names.Size);
        }

        [Test]
        public void TestAddingAllFromNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => this.names.AddAll(null));
        }

        [Test]
        [TestCase("Rosen", "Georgi", "Balkan")]
        public void TestAddAllKeepsSorted(params string[] namesToAdd)
        {
            this.names.AddAll(namesToAdd);

            string[] expectedOrderedCollection = new string[] { "Balkan", "Georgi", "Rosen" };

            CollectionAssert.AreEqual(expectedOrderedCollection, this.names);
        }

        [Test]
        public void TestRemoveValidElementDecreasesSize()
        {
            this.names.Add("Rosen");

            this.names.Remove("Rosen");

            Assert.That(this.names.Size, Is.EqualTo(0));
        }

        [Test]
        [TestCase("Ivan", "Nasko")]
        public void TestRemoveValidElementRemovesSelectedOne(params string[] namesToAdd)
        {
            this.names.AddAll(namesToAdd);

            this.names.Remove("Ivan");

            Assert.That(this.names.Size, Is.EqualTo(1));
        }

        [Test]
        public void TestRemovingNullThrowsException()
        {
            this.names.AddAll(new string[] { "Rosen", "Goshko" });

            Assert.Throws<ArgumentNullException>(() => this.names.Remove(null));
        }

        [Test]
        public void TestJoinWithNull()
        {
            this.names.AddAll(new string[] { "Rosen", "Goshko" });

            Assert.Throws<ArgumentNullException>(() => this.names.JoinWith(null));
        }

        [Test]
        public void TestJoinWorksFine()
        {
            string[] namesToAdd = new string[] { "Rosen", "Goshko" };
            this.names.AddAll(namesToAdd);

            Array.Sort(namesToAdd);

            Assert.That(this.names.JoinWith(", "), Is.EqualTo(string.Join(", ", namesToAdd)));
        }
    }
}
