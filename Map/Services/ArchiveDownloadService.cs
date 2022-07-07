using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Map.Services
{
    public class ArchiveDownloadService
    {
        private static readonly string ArchiveUrl = @"https://data.gov.lv/dati/dataset/0c5e1a3b-0097-45a9-afa9-7f7262f3f623/resource/1d3cbdf2-ee7d-4743-90c7-97d38824d0bf/download/aw_csv.zip";
        private static readonly string TargetName = @"aw_csv.zip";

        public IWebHostEnvironment WebHostEnvironment { get; }
        public ArchiveDownloadService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        
        public Task<string> DownloadArchiveAsync()
        {
            string fullPath = Path.Combine(WebHostEnvironment.WebRootPath, "data" ,TargetName);
            if (!File.Exists(fullPath))
            {
                Task task = Task.Run(() =>
                {
                    WebClient client = new WebClient();
                    client.DownloadFile(new Uri(ArchiveUrl), fullPath);
                });

                task.Wait();
            }
            return Task.FromResult(fullPath);
        }
    }
}
