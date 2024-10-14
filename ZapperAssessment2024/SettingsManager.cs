using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("ZapperAssessment2024Tester")]
namespace ZapperAssessment2024
{
    /// <summary>
    /// Class for operations on account settings.
    /// </summary>
    internal static class SettingsManager
    {
        // Question 2.1 function.
        /// <summary>
        /// Test the nth digit of account settings.
        /// </summary>
        /// <param name="settings">The settings to test against.</param>
        /// <param name="setting">The setting digit to target.</param>
        /// <returns></returns>
        internal static bool SettingsTest(byte settings, byte setting)
        {
            return (settings & (1 << 8 - setting)) != 0;
        }

        // Question 2.2a function.
        /// <summary>
        /// Writes a byte representing settings to a file.
        /// </summary>
        /// <param name="settings">The settings to write.</param>
        /// <param name="fileName">The target file name to write to.</param>
        /// <param name="force">Overwrite if this file already exists.</param>
        internal static void WriteSettingsToFile(byte settings, string fileName, bool force = true)
        {
            using FileStream stream = File.Open(fileName, force ? FileMode.Create : FileMode.OpenOrCreate);
            using BinaryWriter writer = new(stream, Encoding.UTF8, false);
            writer.Write(settings);
        }

        // Question 2.2b function.
        /// <summary>
        /// Reads a byte representing settings from a file.
        /// </summary>
        /// <param name="fileName">The name to read the settings from.</param>
        /// <returns></returns>
        internal static byte ReadSettingsFromFile(string fileName)
        {
            using FileStream stream = File.Open(fileName, FileMode.Open);
            using BinaryReader reader = new(stream, Encoding.UTF8, false);
            return reader.ReadByte();
        }
    }
}
