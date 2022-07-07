using Map.Models;
using Map.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Map.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ArchiveDownloadService _archiveDownloadService;

        private readonly CentroidCsvReaderService _CentroidCsvReaderService;

        public List<Centroid> EdgeCentroids { get; private set; }

        public LatLng ViewLatLng { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, ArchiveDownloadService archiveDownloadService, CentroidCsvReaderService CentroidCsvReaderService)
        {
            _logger = logger;
            _archiveDownloadService = archiveDownloadService;
            _CentroidCsvReaderService = CentroidCsvReaderService;
        }

        public async Task OnGetAsync()
        {
            string path = await _archiveDownloadService.DownloadArchiveAsync();

            IEnumerable<Centroid> centroids = await  _CentroidCsvReaderService.GetCentroidsAsync(path, "AW_VIETU_CENTROIDI.CSV");

            Centroid southmostCentroid = centroids.First();
            Centroid northmostCentroid = centroids.First();
            Centroid westmostCentroid = centroids.First();
            Centroid eastmostCentroid = centroids.First();

            foreach (Centroid centroid in centroids)
            {
                if (centroid.X > eastmostCentroid.X)
                {
                    eastmostCentroid = centroid;
                }

                if (centroid.X < westmostCentroid.X)
                {
                    westmostCentroid = centroid;
                }

                if (centroid.Y > northmostCentroid.Y)
                {
                    northmostCentroid = centroid;
                }

                if (centroid.Y < southmostCentroid.Y)
                {
                    southmostCentroid = centroid;
                }
            }

            EdgeCentroids = new List<Centroid>();

            EdgeCentroids.Add(southmostCentroid);
            EdgeCentroids.Add(westmostCentroid);
            EdgeCentroids.Add(northmostCentroid);
            EdgeCentroids.Add(eastmostCentroid);

            ViewLatLng = new LatLng(EdgeCentroids.Average(c => c.DD_N), EdgeCentroids.Average(c => c.DD_E));
        }
    }
}
