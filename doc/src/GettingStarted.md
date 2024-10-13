# Getting Started 🚀

## Prerequisites

- Ensure [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 is installed on your machine).
- Install [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/download)
  or [Rider](https://www.jetbrains.com/rider/) or whatever code editor.

## Getting the Project ⏬

- Clone the repository:

```sh
git clone https://github.com/kimfom01/Imagizer.git
```

- Alternatively, download and extract the project ZIP file.

## Building the Project ⚙️

- Navigate to the project's root directory in the terminal.

```sh
cd Imagizer/server/ImagizerAPI
```

- Restore dependencies

```sh
dotnet restore
```

- Build the project

```sh
dotnet build
```

## Running the API

- Run the API

```sh
dotnet run
```

- Open [http://localhost:5115/swagger/index.html](http://localhost:5115/swagger/index.html) on the web browser to access
  the Swagger (OpenAPI) documentation page.

## Publishing (For Deployment) ⏫

- To publish the application for deployment, run

```sh
dotnet publish -c Release -o ./publish
```

- Deploy the contents of the `./publish` directory to your hosting environment.
