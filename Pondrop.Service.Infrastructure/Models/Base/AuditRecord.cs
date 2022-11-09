namespace Pondrop.Service.Models;

public record AuditRecord(string CreatedBy, string UpdatedBy, DateTime CreatedUtc, DateTime UpdatedUtc);