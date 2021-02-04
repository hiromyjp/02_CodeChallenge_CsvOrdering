using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSV.Test
{
    [TestClass]
    public class ColumnTest
    {
        [TestMethod]
        public void TestCreateColumn()
        {
            string columnName = "MyColumn";
            var col = new Column(columnName);
            Assert.AreEqual(col.Name, columnName);
        }

        [TestMethod]
        public void TestInsertRowsIntoColumn()
        {
            string columnName = "MyColumn";
            var col = new Column(columnName);
            col.AddValue("one");
            col.AddValue("two");
            Assert.AreEqual(col.Values.Count, 2);
            Assert.AreEqual(col.GetRowValue(0), "one");
        }
    }
}
