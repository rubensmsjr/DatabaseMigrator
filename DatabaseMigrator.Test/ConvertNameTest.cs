using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseMigrator.Test
{
    [TestClass]
    public class ConvertNameTest
    {
        private const string nameTable1 = "TEST_CONVERT_TABLE_NAME_IN_THIRTY_CHARACTERS_1";
        private const string nameTable2 = "CONVERT_TABLE_NAME_IN_THIRTY_CHARACTERS_2_TEST";

        private const string nameColumn1 = "TEST_CONVERT_COLUMN_NAME_IN_THIRTY_CHARACTERS_1";
        private const string nameColumn2 = "CONVERT_COLUMN_NAME_IN_THIRTY_CHARACTERS_2_TEST";

        [TestMethod]
        public void TestConvertTableName()
        {
            string result;

            ConvertName convertName = new ConvertName();

            result = convertName.Table(nameTable1);
            Assert.AreEqual("TEST_CONVERT_TABLE_NAME_IN_THI", result);

            result = convertName.Table(nameTable1);
            Assert.AreEqual("TEST_CONVERT_TABLE_NAME_IN_T_1", result);

            result = convertName.Table(nameTable1);
            Assert.AreEqual("TEST_CONVERT_TABLE_NAME_IN_T_2", result);

            result = convertName.Table(nameTable1);
            Assert.AreEqual("TEST_CONVERT_TABLE_NAME_IN_T_3", result);

            result = convertName.Table(nameTable1);
            Assert.AreEqual("TEST_CONVERT_TABLE_NAME_IN_T_4", result);

            result = convertName.Table("TEST_CONVERT_TABLE_NAME");
            Assert.AreEqual("TEST_CONVERT_TABLE_NAME", result);

            result = convertName.Table(nameTable1);
            Assert.AreEqual("TEST_CONVERT_TABLE_NAME_IN_T_5", result);

            result = convertName.Table(nameTable1);
            Assert.AreEqual("TEST_CONVERT_TABLE_NAME_IN_T_6", result);

            result = convertName.Table(nameTable2);
            Assert.AreEqual("CONVERT_TABLE_NAME_IN_THIRTY_C", result);

            result = convertName.Table(nameTable1);
            Assert.AreEqual("TEST_CONVERT_TABLE_NAME_IN_T_7", result);

            result = convertName.Table(nameTable2);
            Assert.AreEqual("CONVERT_TABLE_NAME_IN_THIRTY_1", result);

            result = convertName.Table(nameTable1);
            Assert.AreEqual("TEST_CONVERT_TABLE_NAME_IN_T_8", result);

            result = convertName.Table(nameTable1);
            Assert.AreEqual("TEST_CONVERT_TABLE_NAME_IN_T_9", result);

            result = convertName.Table(nameTable1);
            Assert.AreEqual("TEST_CONVERT_TABLE_NAME_IN__10", result);
        }

        [TestMethod]
        public void TestConvertColumnName()
        {
            string result;

            ConvertName convertName = new ConvertName();

            result = convertName.Column("TEST1", nameColumn1);
            Assert.AreEqual("TEST_CONVERT_COLUMN_NAME_IN_TH", result);

            result = convertName.Column("TEST1", nameColumn1);
            Assert.AreEqual("TEST_CONVERT_COLUMN_NAME_IN__1", result);

            result = convertName.Column("TEST1", "TEST_CONVERT_COLUMN_NAME");
            Assert.AreEqual("TEST_CONVERT_COLUMN_NAME", result);

            result = convertName.Column("TEST1", nameColumn1);
            Assert.AreEqual("TEST_CONVERT_COLUMN_NAME_IN__2", result);

            result = convertName.Column("TEST1", nameColumn2);
            Assert.AreEqual("CONVERT_COLUMN_NAME_IN_THIRTY_", result);

            result = convertName.Column("TEST1", nameColumn1);
            Assert.AreEqual("TEST_CONVERT_COLUMN_NAME_IN__3", result);

            result = convertName.Column("TEST1", nameColumn2);
            Assert.AreEqual("CONVERT_COLUMN_NAME_IN_THIRT_1", result);

            result = convertName.Column("TEST1", nameColumn1);
            Assert.AreEqual("TEST_CONVERT_COLUMN_NAME_IN__4", result);

            result = convertName.Column("TEST2", nameColumn1);
            Assert.AreEqual("TEST_CONVERT_COLUMN_NAME_IN_TH", result);

            result = convertName.Column("TEST2", nameColumn1);
            Assert.AreEqual("TEST_CONVERT_COLUMN_NAME_IN__1", result);

            result = convertName.Column("TEST2", "TEST_CONVERT_COLUMN_NAME");
            Assert.AreEqual("TEST_CONVERT_COLUMN_NAME", result);

            result = convertName.Column("TEST2", nameColumn1);
            Assert.AreEqual("TEST_CONVERT_COLUMN_NAME_IN__2", result);

            result = convertName.Column("TEST2", nameColumn2);
            Assert.AreEqual("CONVERT_COLUMN_NAME_IN_THIRTY_", result);

            result = convertName.Column("TEST2", nameColumn1);
            Assert.AreEqual("TEST_CONVERT_COLUMN_NAME_IN__3", result);

            result = convertName.Column("TEST2", nameColumn2);
            Assert.AreEqual("CONVERT_COLUMN_NAME_IN_THIRT_1", result);

            result = convertName.Column("TEST2", nameColumn1);
            Assert.AreEqual("TEST_CONVERT_COLUMN_NAME_IN__4", result);
        }
    }
}
