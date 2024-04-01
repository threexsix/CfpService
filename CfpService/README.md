# CallForPaperServiceAPI

## Techstack
Dapper, FluentMigrator, FluentValidator, ASP.NET DI, Postgresql, Npgsql

## Prerequisites

NET 6.0 SDK or later

PostgreSQL server

## Setup

1. Docker compose up
2. Connect and configure DB
3. Set PostgresConnectionString in appsetings.json
4. Run project and check for migrations

## Endpoints

### Applications endpoints

1. POST /Applications
```json
{
  "author": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "activity": "report",
  "name": "C# Basics",
  "description": "",
  "outline": "very interesting"
}
```
Response ==> 201 OK
```json
{
  "id": "f1303566-6c19-439f-8063-d302750c7930",
  "author": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "activity": "report",
  "name": "C# Basics",
  "description": "",
  "outline": "very interesting"
}
```

2. POST /Applications/{applicationId}/Submit

Response ==> 200 OK


3. GET /Applications/{applicationId}

Response ==> 200 OK
```json
{
  "id": "f1303566-6c19-439f-8063-d302750c7930",
  "author": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "activity": "report",
  "name": "C# Basics",
  "description": "",
  "outline": "very interesting"
}
```
4. GET /Applications/submittedAfter={timeString}

Response ==> 200 OK
```json
[
...
  {
    "id": "f1303566-6c19-439f-8063-d302750c7930",
    "author": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "activity": "report",
    "name": "C# Basics",
    "description": "",
    "outline": "very interesting"
  }
  ...
]
```

5. GET /Applications/unsubmittedOlder={timeString}

Response ==> 200 OK
```json
[
...
  {
    "id": "f1303566-6c19-439f-8063-d302750c7930",
    "author": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "activity": "report",
    "name": "C# Basics",
    "description": "",
    "outline": "very interesting"
  }
  ...
]
```

6. DELETE /Applications/{applicationId}

Response ==> 200 OK

7. PUT /Applications/{applicationId}
```json
{
  "activity": "report",
  "name": "report",
  "description": "string",
  "outline": "string"
}
```
Response ==> 201 OK
```json
{
  "id": "863f91a5-95c7-4995-a661-8375018c4642",
  "author": "3fa85f64-5717-4562-b3fc-2c963f66a666",
  "activity": "report",
  "name": "report",
  "description": "string",
  "outline": "string"
}
```
### Activities endpoints
1. GET /Activities

Response ==> 200 OK
```json
[
  {
    "name": "report",
    "description": "Доклад, 35-45 минут"
  },
  {
    "name": "masterclass",
    "description": "Мастеркласс, 1-2 часа"
  },
  {
    "name": "discussion",
    "description": "Дискуссия / круглый стол, 40-50 минут"
  }
]
```
### Users endpoints
1. GET /Users/{userId}/currentapplication

Response ==> 200 OK
```json
{
  "id": "863f91a5-95c7-4995-a661-8375018c4642",
  "author": "3fa85f64-5717-4562-b3fc-2c963f66a666",
  "activity": "report",
  "name": "report",
  "description": "string",
  "outline": "string"
}
```