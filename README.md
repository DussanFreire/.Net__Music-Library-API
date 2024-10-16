# .Net - Music Library API

## Overview

The Music Library API is a .NET web API designed for managing music albums, artists, and songs. It provides a set of RESTful endpoints to perform CRUD operations on albums and artists, as well as to retrieve information about the most played songs and albums.

## Features

- Manage artists and their albums.
- Create, read, update, and delete operations for albums and songs.
- Fetch top albums and most played songs.
- Reports on artist statistics, including mean followers based on years of career.

## API Endpoints

### Artists

- **GET** `/api/artists`
  - Retrieves a list of all artists.
  - **Query Parameters:**
    - `orderBy` (optional): Field to order the results by (default: "id").

- **GET** `/api/artists/{artistId:long}`
  - Retrieves details of a specific artist.

- **POST** `/api/artists`
  - Creates a new artist.

- **PUT** `/api/artists/{artistId:long}`
  - Updates the information of an existing artist.

- **DELETE** `/api/artists/{artistId:long}`
  - Deletes a specific artist.

- **PUT** `/api/artists/{artistId:long}/followers`
  - Updates the follower count of an artist.

- **GET** `/api/artists/report/`
  - Retrieves the mean number of followers by years of career.
  - **Query Parameters:**
    - `years` (optional): Minimum years of career to consider (default: 3).

- **GET** `/api/artists/year/`
  - Retrieves artists based on their year of birth.

### Albums

- **GET** `/api/artists/{artistId:long}/albums`
  - Retrieves all albums of a specific artist.

- **GET** `/api/artists/{artistId:long}/albums/{albumId:long}`
  - Retrieves details of a specific album.

- **POST** `/api/artists/{artistId:long}/albums`
  - Creates a new album for a specific artist.

- **PUT** `/api/artists/{artistId:long}/albums/{albumId:long}`
  - Updates an existing album.

- **DELETE** `/api/artists/{artistId:long}/albums/{albumId:long}`
  - Deletes a specific album.

- **GET** `/api/bestalbums`
  - Retrieves a list of the best albums.

- **GET** `/api/topalbums`
  - Retrieves the top albums based on certain criteria.
  - **Query Parameters:**
    - `value` (optional): Filter for albums.
    - `top` (optional): Number of top albums to return (default: 5).
    - `descending` (optional): Order direction (default: false).

### Songs

- **GET** `/api/artists/{artistId:long}/albums/{albumId:long}/songs`
  - Retrieves all songs from a specific album.

- **GET** `/api/artists/{artistId:long}/albums/{albumId:long}/songs/{songId:long}`
  - Retrieves details of a specific song.

- **POST** `/api/artists/{artistId:long}/albums/{albumId:long}/songs`
  - Creates a new song in a specific album.

- **DELETE** `/api/artists/{artistId:long}/albums/{albumId:long}/songs/{songId:long}`
  - Deletes a specific song from an album.

### Most Heard Albums

- **GET** `/api/mostheardalbums`
  - Retrieves a list of the most heard albums.

### Most Played Songs

- **GET** `/api/mostplayedsongs`
  - Retrieves the most played songs.
  - **Query Parameters:**
    - `orderBy` (optional): Field to order the results by (default: "reproductions").
    - `filter` (optional): Filter criteria for the results (default: "top10").

## Error Handling

The API handles errors gracefully, returning appropriate HTTP status codes:

- **404 Not Found**: If the requested resource does not exist.
- **400 Bad Request**: If the request data is invalid.
- **500 Internal Server Error**: For unexpected errors.

## Dependencies

- .NET Core (version)
- Other relevant packages (if any)

## Getting Started

1. Clone the repository.
2. Open the project in Visual Studio or your preferred IDE.
3. Restore dependencies and build the project.
4. Run the API and access it at `http://localhost:{port}/api`.

## Contributing

Contributions are welcome! Please submit a pull request or open an issue for any improvements or bug fixes.
