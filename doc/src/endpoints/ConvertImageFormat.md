# Convert Image Format

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
