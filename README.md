# Imagizer API 

## Introduction

Imagizer is an image manipulation API built using Magick.NET

- This is an API where users can manipulate their provided image.
- The API allows users to resize images and convert images from one format to another

---

## Features

- Resize images
- Convert image formats

Visit the [official documentation](https://kimfom01.github.io/Imagizer/) for more info

[//]: # (---)

[//]: # ()
[//]: # (## Additional Notes)

[//]: # ()
[//]: # (- **Rate Limiting**: &#40;Include details about rate limiting, if applicable&#41;)

[//]: # (---)

[//]: # ()
[//]: # (## Demo)

[//]: # ()
[//]: # (- Include screenshots or a demo video to visually demonstrate your project.)

---

## Resize Image

- **Endpoint**: `POST /api/image/resize`
- **Description**: Resizes an uploaded image to a specified size.
- **Content-Type**: `multipart/form-data`
- **Request Parameters**:
    - `ImageFile` (required): The image file to be resized.
    - `Size` (required): The target size for the image resizing.
- **Responses**:
    - `200 OK`: Returns the resized image file.
    - `400 Bad Request`: If the request is invalid or missing required fields.
- **Sample Request**:
  ```plaintext
  POST /api/image/resize
  Content-Type: multipart/form-data

  FormData:
    ImageFile: (binary)
    Size: 600
  ```
- **Remarks**: Ensure the request uses the `multipart/form-data` content type to include the image file and the size
  parameter.

## Convert Image

- **Endpoint**: `POST /api/image/convert`
- **Description**: Converts an uploaded image to a specified format.
- **Content-Type**: `multipart/form-data`
- **Request Parameters**:
    - `ImageFile` (required): The image file to convert.
    - `Formats` (required): The target image format for conversion. Supported
      values: `Gif = 80`, `Heic = 88`, `Jpeg = 112`, `Jpg = 113`, `Png = 182`, `Raw = 204`, `Tiff = 235`.

- **Responses**:
    - `200 OK`: Returns the converted image file in the specified format.
    - `400 Bad Request`: If the request is invalid, such as unsupported file format or missing file.
- **Sample Request**:
  ```plaintext
  POST /api/image/convert
  Content-Type: multipart/form-data

  FormData:
    ImageFile: (binary)
    Formats: 112
  ```
- **Remarks**: The request must be `multipart/form-data` content type to include the image file and the format
  parameter. Choose the target format from the supported `ImageFormats` enum values.

---

## Roadmap

- Add more formats
- Add rate limiting
- Build a minimal user interface
- Add more image manipulation endpoints

---

## Hosting

- [Render](https://render.com/) (docker image)

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
cd Imagizer/Imagizer.Api
```

- Build the project
```sh
dotnet build
````

### Running the Application

- Navigate to the project root directory

```sh
cd Imagizer/Imagizer.Api
```

- Restore dependencies

```sh
dotnet restore
```

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

[//]: # "## Changelog"

[//]: # "- (Optional) Include a changelog file detailing the chronological changes made to the project."

[//]: #

[//]: # "## FAQs"

[//]: # "- (Optional) Frequently asked questions about the project."

