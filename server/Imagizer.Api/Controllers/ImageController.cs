using Imagizer.Api.Models;
using Imagizer.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Imagizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("fixed-by-ip")]
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
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/image
    /// 
    /// </remarks>
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
    /// <returns>A link to download the resized image.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/image/resize
    ///     Content-Type: multipart/form-data
    ///     FormData:
    ///         ImageFile: (The image file to be resized),
    ///         Size: 600
    ///
    /// The request must be made with 'multipart/form-data' content type to include the image file and the size parameter.
    /// </remarks>
    /// <response code="200">Returns a link to download the image</response>
    /// <response code="400">If the request is invalid (missing image, size less than or equal to zero)</response>
    [HttpPost("resize")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UrlResponse>> ResizeImage([FromForm] ResizeRequest resizeRequest)
    {
        try
        {
            var urlResponse = await Task.Run(() => _imageProcessorService.ResizeImage(resizeRequest));

            return Ok(urlResponse);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    /// <summary>
    /// Converts an uploaded image to a specified format.
    /// </summary>
    /// <param name="convertRequest">The convert request containing the image file and the target format.</param>
    /// <returns>A link to download the converted image.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/image/convert
    ///     Content-Type: multipart/form-data
    ///     FormData:
    ///         ImageFile: (The image file to be converted),
    ///         Format: 182
    ///
    /// The request must be made with 'multipart/form-data' content type to include the image file and the format parameter.
    /// </remarks>
    /// <response code="200">Returns a link to download the image</response>
    /// <response code="400">If the request is invalid (missing image, missing/not supported format)</response>
    [HttpPost("convert")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UrlResponse>> ConvertImage([FromForm] ConvertRequest convertRequest)
    {
        try
        {
            var urlResponse = await _imageProcessorService.ConvertImage(convertRequest);

            return Ok(urlResponse);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}