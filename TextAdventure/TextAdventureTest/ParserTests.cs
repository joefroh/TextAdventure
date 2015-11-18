using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAdventureLib;

namespace TextAdventureTest
{
    [TestClass]
    public class ParserTests
    {

        #region Constructor Tests
        [TestMethod]
        public void ConstructorFileNotExistException()
        {
            try
            {
                Parser parser = new Parser("THIS IS NOT A FILE");
                Assert.Fail();
            }
            catch(ArgumentException e)
            {
                Assert.IsTrue(true);
            }
        }


        [TestMethod]
        public void ConstructorFileExists()
        {
            Parser parser = new Parser("Sample1.taf");
        }
        #endregion
    }
}
