using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using System.Globalization;
using StockExchangeAPI.Models;
using System.IO;
using CsvHelper.Configuration;

[ApiController]
[Route("api/[controller]")]
public class DataSelectionController : ControllerBase
{
    [HttpGet("{fileName}")]
    public IActionResult GetRandomDataPoints(string fileName)
    {
        try
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),"Data", fileName);
            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found.");

            var records = new List<StockData>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null, // Ignore header validation
                MissingFieldFound = null, // Ignore missing fields
                HasHeaderRecord = false // Specify there is no header record in the CSV
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                //csv.ReadHeader(); Skip the first row if it's not a header
                csv.Context.RegisterClassMap<StockDataMap>();
                records = csv.GetRecords<StockData>().ToList();
            }

            if (records.Count < 30)
                return BadRequest("Not enough data points.");

            var random = new Random();
            int startIndex = random.Next(0, records.Count - 29);
            var selectedData = records.Skip(startIndex).Take(30).ToList();

            return Ok(selectedData);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}


public class StockDataMap : ClassMap<StockData>
{
    public StockDataMap()
    {
        Map(m => m.Ticker).Index(0);
        Map(m => m.Timestamp).Index(1).TypeConverterOption.Format("dd-MM-yyyy");
        Map(m => m.StockPrice).Index(2);
    }
}