namespace Pondrop.Service.Interfaces.Services;

public interface IAddressService
{
    bool IsValidAustralianPostcode(string postcode);
    bool IsValidAustralianState(string state);
    bool IsValidAustralianCountry(string country);
}