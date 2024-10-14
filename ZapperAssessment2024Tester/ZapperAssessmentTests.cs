using ZapperAssessment2024;

namespace ZapperAssessment2024Tester
{

    namespace ZapperAssessmentTests
    {
        [TestClass]
        public class SettingsManagerTests
        {
            /// <summary>
            /// Test for false on 0 digit.
            /// </summary>
            [TestMethod]
            public void Settings_False_OnDigit7()
            {
                Assert.IsFalse(SettingsManager.SettingsTest(0b00000000, 7));
            }

            /// <summary>
            /// Test for true on 1 digit.
            /// </summary>
            [TestMethod]
            public void Settings_True_ExistsOnDigit7()
            {
                Assert.IsTrue(SettingsManager.SettingsTest(0b00000010, 7));
            }

            /// <summary>
            /// Some more digits toggled for true test.
            /// </summary>
            [TestMethod]
            public void Settings_True_ExistsOnDigit7_WithOtherDigits()
            {
                Assert.IsTrue(SettingsManager.SettingsTest(0b00111010, 7));
            }

            /// <summary>
            /// False in between other 1 digits.
            /// </summary>
            [TestMethod]
            public void Settings_False_AmongOthers()
            {
                Assert.IsFalse(SettingsManager.SettingsTest(0b00111010, 6));
            }

            /// <summary>
            /// True for test between all flipped digits.
            /// </summary>
            [TestMethod]
            public void Settings_True_OnDigit4()
            {
                Assert.IsTrue(SettingsManager.SettingsTest(0b11111111, 4));
            }

            /// <summary>
            /// Test for when out of range of settings value.
            /// </summary>
            [TestMethod]
            public void Settings_False_SettingOutOfBounds()
            {
                Assert.IsFalse(SettingsManager.SettingsTest(0b11111111, 9));
            }

            /// <summary>
            /// Test writing binary to file.
            /// </summary>
            [TestMethod]
            public void Writing_Settings_ToFile()
            {
                string fileName = "./fileWriteTest.bin";

                SettingsManager.WriteSettingsToFile(0b00111010, fileName);

                Assert.IsTrue(File.Exists(fileName));

                File.Delete(fileName);
            }

            /// <summary>
            /// Test for successful read from file.
            /// </summary>
            [TestMethod]
            public void Reading_Settings_FromFile()
            {
                string fileName = "./fileReadTest.bin";
                byte testSettings = 0b00111010;

                SettingsManager.WriteSettingsToFile(testSettings, fileName);

                byte readSettings = SettingsManager.ReadSettingsFromFile(fileName);

                Assert.IsTrue(readSettings == testSettings);

                File.Delete(fileName);
            }
        }
    }
}