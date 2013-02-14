using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MongoLinqpadDriverDeployer
{
    /// <summary>
    /// Deployment helper for testing the driver - not needed for driver redistribution
    /// </summary>
    internal class Program
    {
        private static readonly string[] Files = new[]
        {
			"MongoLinqpadDriver.dll",
			"MongoLinqpadDriver.pdb",
			"MongoUtils.dll",
			"MongoUtils.pdb",
			"..\\..\\Libs\\1_7\\MongoDB.Driver.dll",
			"..\\..\\Libs\\1_7\\MongoDB.Driver.pdb",
			"..\\..\\Libs\\1_7\\MongoDB.Bson.dll",
			"..\\..\\Libs\\1_7\\MongoDB.Bson.pdb",
        };

        private static void Main(string[] args)
        {
            string publicKeyToken = string.Empty;
            var assembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), Files.First()));
            assembly.GetName().GetPublicKeyToken().Select(t => publicKeyToken += t.ToString("x2")).ToList();

            string deploymentPath = string.Format(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                @"LINQPad\Drivers\DataContext\4.0\MongoLinqpadDriver ({0})\"), publicKeyToken);

            Console.WriteLine("Output path: " + deploymentPath);

            if (!Directory.Exists(deploymentPath))
                Directory.CreateDirectory(deploymentPath);

            Console.WriteLine(Directory.GetCurrentDirectory());

            foreach (string file in Files)
            {
                var destFile = Path.Combine(deploymentPath, Path.GetFileName(file));

                if (File.Exists(destFile))
                    File.Delete(destFile);

                if (File.Exists(file))
                    File.Copy(file, destFile);
            }
        }
    }
}