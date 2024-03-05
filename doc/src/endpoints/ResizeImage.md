# Resize Image

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
