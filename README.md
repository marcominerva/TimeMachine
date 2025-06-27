# Time Machine

This example shows how to use the SQL Server Temporal Tables with Entity Framework Core to create a Time Machine for our data.

## Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd TimeMachine
```

### 2. Configure Database Connection

Update the connection string in `TimeMachine/appsettings.json`:

```json
{
    "ConnectionStrings": {
        "SqlConnection": "Server=(localdb)\\mssqllocaldb;Database=TimeMachineDb;Trusted_Connection=true;MultipleActiveResultSets=true"
    }
}
```

For other SQL Server configurations, adjust the connection string accordingly:
- **SQL Server Express**: `Server=.\\SQLEXPRESS;Database=TimeMachineDb;Trusted_Connection=true;MultipleActiveResultSets=true`
- **SQL Server with authentication**: `Server=localhost;Database=TimeMachineDb;User Id=your_username;Password=your_password;MultipleActiveResultSets=true`

### 3. Populate the Database

**Important**: After configuring the database connection, you must run the `Scripts.sql` file to create the temporal tables and populate the database with sample data. This script contains:

- Complete table definitions with temporal table configuration
- Sample data for Cities, People, and PhoneNumbers
- Proper foreign key relationships
- Commented scripts for converting existing tables to temporal tables (if needed)

Execute the `Scripts.sql` file using SQL Server Management Studio, Azure Data Studio, or via command line:

```bash
sqlcmd -S (localdb)\mssqllocaldb -d TimeMachineDb -i Scripts.sql
```

This step is required to have test data available for the API endpoints and to see the temporal table functionality in action.

## API Endpoints

### Get Person by ID

```
GET /api/people/{id}
```

Optional query parameter:
- `dateTime`: ISO 8601 formatted datetime to query historical data

Examples:
- Get current data: `GET /api/people/123e4567-e89b-12d3-a456-426614174000`
- Get historical data: `GET /api/people/123e4567-e89b-12d3-a456-426614174000?dateTime=2023-01-01T12:00:00Z`

## Database Schema

The application uses SQL Server Temporal Tables to automatically track changes to:
- **People**: Person information with city relationship
- **PhoneNumbers**: Phone numbers associated with people
- **Cities**: City information

Each table has corresponding history tables that store previous versions of records.

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.
