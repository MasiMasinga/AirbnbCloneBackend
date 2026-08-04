namespace AirbnbClone.Application.Features.Listings.DTOs;

public class ListingDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Amenities { get; set; } = string.Empty;
    public string HouseRules { get; set; } = string.Empty;
    public decimal Pricing { get; set; }
    public bool? Availability { get; set; }
    public int BedCount { get; set; }
    public int BathCount { get; set; }
    public string PropertyType { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string Photo { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string ReviewId { get; set; } = string.Empty;
}