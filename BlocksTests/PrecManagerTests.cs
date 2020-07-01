using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using SDPreSubmissionNS;
using System.Linq;

namespace BlocksTests
{
    [TestClass]
    public class PrecManagerTests
    {
        //Method Tested: GenerateDictionary
        //Context: Passing the following prec file:
        //0 0
        //1 2 2 9
        //2 2 3 10
        //3 2 4 11
        //4 1 5
        //5 2 0 6
        //Expectation: Create a dictionary where key: id, value: list of required blocks:
        //{
        //  0: []
        //  1: [2, 9]
        //  2: [3, 10]
        //  3: [4, 11]
        //  4: [5]
        //  5: [0, 6]
        //}
        [TestMethod]
        public void GenerateDictionaryTest()
        {
            List<int> l0 = new List<int>();
            List<int> l1 = new List<int>(new int[] {2, 9});
            List<int> l2 = new List<int>(new int[] {3, 10});
            List<int> l3 = new List<int>(new int[] {4, 11});
            List<int> l4 = new List<int>(new int[] {5});
            List<int> l5 = new List<int>(new int[] {0, 6});
            Dictionary<int, List<int>> correctDictionary = new Dictionary<int, List<int>>();
            correctDictionary.Add(0, l0);
            correctDictionary.Add(1, l1);
            correctDictionary.Add(2, l2);
            correctDictionary.Add(3, l3);
            correctDictionary.Add(4, l4);
            correctDictionary.Add(5, l5);
            Dictionary<int, List<int>> evaluatedDictionary = PrecManager.GenerateDictionary("Test Files/GenerateDictionaryTestFile.txt");
            bool dictionariesEqual = correctDictionary.Keys.Count == evaluatedDictionary.Keys.Count && correctDictionary.Keys.All(k => evaluatedDictionary.ContainsKey(k) && evaluatedDictionary[k].All(correctDictionary[k].Contains) && evaluatedDictionary[k].Count == correctDictionary[k].Count); //evaluatedDictionary[k].All(correctDictionary[k].Contains) && evaluatedDictionary[k].Count == correctDictionary[k].Count;
            Assert.IsTrue(dictionariesEqual);
        }
    }
}
