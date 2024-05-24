# LSEG_Assign


## Setup

1. Clone the repository.
2. Navigate to the project directory.
   
## using Dotnet CLI  
1. Restore the dependencies: `dotnet restore`
2. Build the project: `dotnet build`
3. Run the project: `dotnet run`

   ##or
   
## Using Visual studio
1. Open Sln file in Visual studio
2. Build The sloution
3. Run the solution
   ##or
## Docker
1- Build the docker file and do docker run



## Usage
## Access the API on Local host and port: ex - https://localhost:44350/swagger/index.html

### First API: Get Random Data Points

Endpoint: `GET /api/DataSelection/{fileName}`

- `fileName`: The name of the CSV file in the `Data` folder.

### Second API: Get Outliers

Endpoint: `POST /api/OutlierDetection`

- Request Body: A JSON array of 30 data points.

### Example Request

```json
[
    {
        "Ticker": "AAPL",
        "Timestamp": "2023-01-01T00:00:00",
        "StockPrice": 150.00
    },
    ...
]
