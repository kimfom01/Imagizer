# Imagizer API

## Introduction

Imagizer is an image manipulation API built using Magick.NET

- This is an API where users can manipulate their provided image.
- The API allows users to resize images and convert images from one format to another
- The API is rate limited to **_10 requests per minute_**

---

## Features

- Resize images
- Convert image formats

Visit the [official documentation](https://kimfom01.github.io/Imagizer/) for more info

---

## Additional Notes

- **Rate Limiting**: The API is rate limited to **_10 requests per minute_**

---

## Roadmap

- [ ] Add more formats
- [x] Add rate limiting
- [x] Build a minimal user interface
- [ ] Add more image manipulation endpoints

---

## Authors

Contributors names and contact info

- Kim Fom - [kimfom01@gmail.com](mailto:kimfom01@gmail.com)

---

## Installation

### Prerequisites

- Ensure [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 is installed on your machine).
- Install [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/download)
  or [Rider](https://www.jetbrains.com/rider/) or whatever code editor.

### Getting the Project

- Clone the repository:

```sh
git clone https://github.com/kimfom01/Imagizer.git
```

- Alternatively, download and extract the project ZIP file.

### Building the Project

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

### Running the Application

- Run the API

```sh
dotnet run
```

- Open [http://localhost:5115/swagger/index.html](http://localhost:5115/swagger/index.html) on the web browser to access
  the Swagger (OpenAPI) documentation page.

### Publishing (For Deployment)

- To publish the application for deployment, run

```sh
dotnet publish -c Release -o ./publish
```

- Deploy the contents of the `./publish` directory to your hosting environment.

---

## License

- The Imagizer API is released under the [MIT license](https://github.com/kimfom01/Imagizer/blob/main/LICENSE)

---

## Credits

- [Magick.NET](https://github.com/dlemstra/Magick.NET)

---

## Contact Information

- Kim Fom - [kimfom01@gmail.com](mailto:kimfom01@gmail.com)
