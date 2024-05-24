using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using StockExchangeAPI.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class OutlierDetectionController : ControllerBase
{
    [HttpPost]
    public IActionResult GetOutliers([FromBody] List<StockData> dataPoints)
    {
        if (dataPoints == null || dataPoints.Count != 30)
            return BadRequest("Invalid data points. Must be exactly 30.");

        var mean = dataPoints.Average(dp => dp.StockPrice);
        var stdDev = Math.Sqrt(dataPoints.Average(dp => Math.Pow((double)(dp.StockPrice - mean), 2)));

        var threshold = 2 * stdDev;

        var outliers = dataPoints.Where(dp => Math.Abs(dp.StockPrice - mean) > (decimal)threshold)
                                 .Select(dp => new Outlier
                                 {          
                                     Ticker = dp.Ticker,
                                     Timestamp = dp.Timestamp,
                                     StockPrice = dp.StockPrice,
                                     Mean = mean,
                                     Deviation = dp.StockPrice - mean,
                                     PercentDeviation = ((dp.StockPrice - mean) / (decimal)threshold) * 100
                                 }).ToList();
        if (outliers.Any())
        {
            string fileName = $"Outliers_{DateTime.Now:yyyyMMddHHmmss}.csv";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Output", fileName);

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(outliers);
            }
        }

        return Ok(outliers);
    }
}
