using AirbnbClone.Application.Common.Abstractions;
using AirbnbClone.Application.Features.Listings.DTOs;
using AirbnbClone.Application.Features.Listings.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbClone.Api.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class ListingsController : ControllerBase
{
    private readonly IListingService _listingService;
    private readonly IValidator<CreateListingDto> _createValidator;
    private readonly IValidator<UpdateListingDto> _updateValidator;
    private readonly ILoggerAdapter<ListingsController> _logger;

    public ListingsController(
        IListingService listingService,
        IValidator<CreateListingDto> createValidator,
        IValidator<UpdateListingDto> updateValidator,
        ILoggerAdapter<ListingsController> logger)
    {
        _listingService = listingService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        _logger.LogInformation("GET /api/listings");

        var listings = await _listingService.GetAllListings(ct);
        return Ok(listings);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        _logger.LogInformation("GET /api/listings/{ListingId}", id);

        var listing = await _listingService.GetListingById(id, ct);
        if (listing is null)
        {
            _logger.LogWarning("Listing {ListingId} not found", id);
            return NotFound();
        }

        return Ok(listing);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateListingDto dto, CancellationToken ct)
    {
        _logger.LogInformation("POST /api/listings for user {UserId}", dto.UserId);

        var validationResult = await _createValidator.ValidateAsync(dto, ct);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Create listing validation failed for user {UserId}", dto.UserId);
            return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
        }

        var created = await _listingService.CreateListing(dto, ct);
        _logger.LogInformation("Created listing {ListingId}", created.Id);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateListingDto dto, CancellationToken ct)
    {
        _logger.LogInformation("PUT /api/listings/{ListingId}", id);

        if (id != dto.Id)
        {
            _logger.LogWarning("Route id {RouteId} does not match body id {BodyId}", id, dto.Id);
            return BadRequest("Route id and body id must match.");
        }

        var validationResult = await _updateValidator.ValidateAsync(dto, ct);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Update listing validation failed for listing {ListingId}", id);
            return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
        }

        var updated = await _listingService.UpdateListing(dto, ct);
        if (updated is null)
        {
            _logger.LogWarning("Listing {ListingId} not found for update", id);
            return NotFound();
        }

        _logger.LogInformation("Updated listing {ListingId}", id);
        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        _logger.LogInformation("DELETE /api/listings/{ListingId}", id);

        var deleted = await _listingService.DeleteListing(id, ct);
        if (!deleted)
        {
            _logger.LogWarning("Listing {ListingId} not found for delete", id);
            return NotFound();
        }

        _logger.LogInformation("Deleted listing {ListingId}", id);
        return NoContent();
    }
}
