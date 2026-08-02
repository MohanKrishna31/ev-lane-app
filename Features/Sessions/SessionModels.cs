namespace nApps.Futs.Mobile.Features.Sessions;

public sealed class PagedSessionResult
{
    public List<ChargingSessionDto> Items { get; set; } = [];
    public long TotalCount { get; set; }
}

public sealed class ChargingSessionDto
{
    public Guid Id { get; set; }
    public Guid? CustomerVehicleId { get; set; }
    public string? VehicleName { get; set; }
    public string? RegistrationNumber { get; set; }
    public Guid? StationId { get; set; }
    public string? StationName { get; set; }
    public Guid? ChargerId { get; set; }
    public string? ChargerCode { get; set; }
    public int ConnectorId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? StopTime { get; set; }
    public double EnergyKwh { get; set; }
    public int DurationSeconds { get; set; }
    public double Cost { get; set; }
    public string? Currency { get; set; }
    public double TariffRatePerKwh { get; set; }
    public double WalletDebitedAmount { get; set; }
    public double OutstandingAmount { get; set; }
    public int PaymentStatus { get; set; }
    public int SessionStatus { get; set; }
    public string? StopReason { get; set; }
    public int StopRequestStatus { get; set; }
}

public sealed class RequestChargingStartDto
{
    public Guid? ReservationId { get; set; }
    public Guid CustomerVehicleId { get; set; }
    public Guid ChargerId { get; set; }
    public int ConnectorId { get; set; }
}

public sealed class ChargingStartRequestDto
{
    public Guid Id { get; set; }
    public int Status { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public Guid? ChargingSessionId { get; set; }
    public string? FailureReason { get; set; }
}
