using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Map.Models;

namespace Map.Services
{
    public class CentroidCsvReaderService
    {
        public static readonly char SeparatorChar = ';';
        public Task<IEnumerable<Centroid>> GetCentroidsAsync(string archivePath, string fileName)
        {
            ZipArchive zip = ZipFile.OpenRead(archivePath);

            ZipArchiveEntry zipEntry = zip.GetEntry(fileName);

            StreamReader reader = new StreamReader(zipEntry.Open());

            List<Centroid> centroids = new List<Centroid>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine().Trim();

                string[] values =  line.Split(SeparatorChar);

                if (values.Length > 10)
                {
                    Console.WriteLine(values.Length);
                }

                centroids.Add(new Centroid(values));
            }

            //remove header
            centroids.RemoveAt(0);

            return Task.FromResult((IEnumerable<Centroid>) centroids);
        }
    }
}
