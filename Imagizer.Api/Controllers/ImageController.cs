using Imagizer.Api.Models;
using Imagizer.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Imagizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IImageProcessorService _imageProcessorService;

    public ImageController(IImageProcessorService imageProcessorService)
    {
        _imageProcessorService = imageProcessorService;
    }

    /// <summary>
    /// Gets a greeting message
    /// </summary>
    /// <response code="200">Returns a greeting message</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetGreetings()
    {
        return Ok(new { message = "Hello from Imagizer" });
    }

    /// <summary>
    /// Resizes an uploaded image to a specified size.
    /// </summary>
    /// <param name="resizeRequest">The resize request containing the image file and the target size.</param>
    /// <returns>A resized image file.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /resize
    ///     Content-Type: multipart/form-data
    ///     FormData:
    ///         ImageFile: (The image file to be resized),
    ///         Size: 600
    ///
    /// The request must be made with 'multipart/form-data' content type to include the image file and the size parameter.
    /// </remarks>
    /// <response code="200">Returns the resized image file</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPost("resize")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ResizeImage([FromForm] ResizeRequest resizeRequest)
    {
        var imageResponse = await Task.Run(() => _imageProcessorService.ResizeImage(resizeRequest));

        return File(imageResponse.ImageBytes, $"image/{imageResponse.Format.ToLower()}");
    }

    /// <summary>
    /// Converts an uploaded image to a specified format.
    /// </summary>
    /// <param name="convertRequest">The convert request containing the image file and the target format.</param>
    /// <returns>A converted image file.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /convert
    ///     Content-Type: multipart/form-data
    ///     FormData:
    ///         ImageFile: (The image file to be converted),
    ///         Format: 182
    ///
    /// The request must be made with 'multipart/form-data' content type to include the image file and the format parameter.
    /// </remarks>
    /// <response code="200">Returns the converted image file</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPost("convert")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ConvertImage([FromForm] ConvertRequest convertRequest)
    {
        var imageResponse = await _imageProcessorService.ConvertImage(convertRequest);

        return File(imageResponse.ImageBytes, $"image/{imageResponse.Format.ToLower()}");
    }
}