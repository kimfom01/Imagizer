# Imagizer API 

Imagizer is an image manipulation API built using Magick.NET

[//]: # (## Getting Started)

[//]: # ()
[//]: # (- **Prerequisites**: &#40;List any prerequisites or dependencies required to use your API&#41;)

[//]: # (- **Installation**: &#40;If your API is part of a larger application or requires installation, provide the steps here&#41;)

[//]: # (- **Quickstart**: &#40;A quick example to get started with calling an endpoint, perhaps with `curl` or another simple tool&#41;)

## Resize Image

- **Endpoint**: `POST /resize`
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
  POST /resize
  Content-Type: multipart/form-data

  FormData:
    ImageFile: (binary)
    Size: 600
  ```
- **Remarks**: Ensure the request uses the `multipart/form-data` content type to include the image file and the size parameter.

## Convert Image

- **Endpoint**: `POST /convert`
- **Description**: Converts an uploaded image to a specified format.
- **Content-Type**: `multipart/form-data`
- **Request Parameters**:
    - `ImageFile` (required): The image file to convert.
    - `Formats` (required): The target image format for conversion. Supported values: `Gif = 80`, `Heic = 88`, `Jpeg = 112`, `Jpg = 113`, `Png = 182`, `Raw = 204`, `Tiff = 235`.
  
- **Responses**:
    - `200 OK`: Returns the converted image file in the specified format.
    - `400 Bad Request`: If the request is invalid, such as unsupported file format or missing file.
- **Sample Request**:
  ```plaintext
  POST /convert
  Content-Type: multipart/form-data

  FormData:
    ImageFile: (binary)
    Formats: 112
  ```
- **Remarks**: The request must be `multipart/form-data` content type to include the image file and the format parameter. Choose the target format from the supported `ImageFormats` enum values.

---

[//]: # (### Additional Notes)

[//]: # ()
[//]: # (- **Authentication**: &#40;Describe how users are authenticated, if applicable&#41;)

[//]: # (- **Rate Limiting**: &#40;Include details about rate limiting, if applicable&#41;)

[//]: # (- **Errors**: &#40;Provide general information on how errors are returned and how to interpret them&#41;)

