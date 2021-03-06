﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using SDPreSubmissionNS;

namespace BlocksTests
{
    [TestClass]
    public class BlockTests
    {
        //Method Tested: GetMassInKg
        //Context: Passing a Block with attributes:
        //Id=1, X=10, Y=10, Z=10, weight=1000
        //Expectation: Should return 1000000
        [TestMethod]
        public void GetMassInKgTest()
        {
            Block block = new Block(1, 10, 10, 10, 1000);
            double expectedValue = 1000000;
            Assert.AreEqual(expectedValue, block.GetMassInKg());

        }
    }
}
