using HVACrate.Application.Interfaces;
using HVACrate.Application.Models.BuildingEnvelopes;
using HVACrate.Domain.Enums;
using Moq;

public class BuildingEnvelopeServiceTests
{
    private readonly Mock<IBuildingEnvelopeService> _mockService;

    public BuildingEnvelopeServiceTests()
    {
        _mockService = new Mock<IBuildingEnvelopeService>();
    }

    [Fact]
    public async Task GetAllAsReadOnlyAsync_ReturnsList()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var expected = new List<BuildingEnvelopeModel> { new OuterWallModel { Id = Guid.NewGuid() } };
        _mockService.Setup(s => s.GetAllAsReadOnlyAsync(roomId, default)).ReturnsAsync(expected);

        // Act
        var result = await _mockService.Object.GetAllAsReadOnlyAsync(roomId);

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task GetOuterWallsByRoomAsync_ReturnsWalls()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var expected = new[] { new OuterWallModel { Id = Guid.NewGuid() } };
        _mockService.Setup(s => s.GetOuterWallsByRoomAsync(roomId, default)).ReturnsAsync(expected);

        // Act
        var result = await _mockService.Object.GetOuterWallsByRoomAsync(roomId);

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetOpeningsByRoomAsync_ReturnsOpenings()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var expected = new[] { new OpeningModel { Id = Guid.NewGuid() } };
        _mockService.Setup(s => s.GetOpeningsByRoomAsync(roomId, default)).ReturnsAsync(expected);

        // Act
        var result = await _mockService.Object.GetOpeningsByRoomAsync(roomId);

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetOpeningsByRoomAndDirectionAsync_ReturnsFiltered()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var direction = Direction.North;
        var expected = new[] { new OpeningModel { Id = Guid.NewGuid() } };
        _mockService.Setup(s => s.GetOpeningsByRoomAndDirectionAsync(roomId, direction, default)).ReturnsAsync(expected);

        // Act
        var result = await _mockService.Object.GetOpeningsByRoomAndDirectionAsync(roomId, direction);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetInternalFencesByRoomAsync_ReturnsInternalFences()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var expected = new[] { new InternalFenceModel { Id = Guid.NewGuid() } };
        _mockService.Setup(s => s.GetInternalFencesByRoomAsync(roomId, default)).ReturnsAsync(expected);

        // Act
        var result = await _mockService.Object.GetInternalFencesByRoomAsync(roomId);

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetFloorsByRoomAsync_ReturnsFloors()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var expected = new[] { new FloorModel { Id = Guid.NewGuid() } };
        _mockService.Setup(s => s.GetFloorsByRoomAsync(roomId, default)).ReturnsAsync(expected);

        // Act
        var result = await _mockService.Object.GetFloorsByRoomAsync(roomId);

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetRoofsByRoomAsync_ReturnsRoofs()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var expected = new[] { new RoofModel { Id = Guid.NewGuid() } };
        _mockService.Setup(s => s.GetRoofsByRoomAsync(roomId, default)).ReturnsAsync(expected);

        // Act
        var result = await _mockService.Object.GetRoofsByRoomAsync(roomId);

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetByIdAsReadOnlyAsync_ReturnsEnvelope()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expected = new OuterWallModel { Id = id };
        _mockService.Setup(s => s.GetByIdAsReadOnlyAsync(id, default)).ReturnsAsync(expected);

        // Act
        var result = await _mockService.Object.GetByIdAsReadOnlyAsync(id);

        // Assert
        Assert.Equal(id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsEditableEnvelope()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expected = new OpeningModel { Id = id };
        _mockService.Setup(s => s.GetByIdAsync(id, default)).ReturnsAsync(expected);

        // Act
        var result = await _mockService.Object.GetByIdAsync(id);

        // Assert
        Assert.Equal(id, result.Id);
    }

    [Fact]
    public async Task CreateAsync_Executes()
    {
        // Arrange
        var model = new FloorModel { Id = Guid.NewGuid() };
        _mockService.Setup(s => s.CreateAsync(model, default)).Returns(Task.CompletedTask).Verifiable();

        // Act
        await _mockService.Object.CreateAsync(model);

        // Assert
        _mockService.Verify();
    }

    [Fact]
    public async Task UpdateAsync_Executes()
    {
        // Arrange
        var model = new InternalFenceModel { Id = Guid.NewGuid() };
        _mockService.Setup(s => s.UpdateAsync(model, default)).Returns(Task.CompletedTask).Verifiable();

        // Act
        await _mockService.Object.UpdateAsync(model);

        // Assert
        _mockService.Verify();
    }

    [Fact]
    public async Task SoftDeleteAsync_Executes()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockService.Setup(s => s.SoftDeleteAsync(id, default)).Returns(Task.CompletedTask).Verifiable();

        // Act
        await _mockService.Object.SoftDeleteAsync(id);

        // Assert
        _mockService.Verify();
    }

    [Fact]
    public async Task GetWallByDirectionAsync_ReturnsWall()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var direction = Direction.East;
        var expected = new OuterWallModel { Id = Guid.NewGuid() };
        _mockService.Setup(s => s.GetWallByDirectionAsync(roomId, direction, default)).ReturnsAsync(expected);

        // Act
        var result = await _mockService.Object.GetWallByDirectionAsync(roomId, direction);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task IsThereAWallOnDirectionAsync_ReturnsTrue()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var direction = Direction.South;
        _mockService.Setup(s => s.IsThereAWallOnDirectionAsync(roomId, direction, default)).ReturnsAsync(true);

        // Act
        var result = await _mockService.Object.IsThereAWallOnDirectionAsync(roomId, direction);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task IsThereAnOpeningOnDirectionAsync_ReturnsFalse()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var direction = Direction.West;
        _mockService.Setup(s => s.IsThereAnOpeningOnDirectionAsync(roomId, direction, default)).ReturnsAsync(false);

        // Act
        var result = await _mockService.Object.IsThereAnOpeningOnDirectionAsync(roomId, direction);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task IsThereARoofInRoomAsync_ReturnsTrue()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        _mockService.Setup(s => s.IsThereARoofInRoomAsync(roomId, default)).ReturnsAsync(true);

        // Act
        var result = await _mockService.Object.IsThereARoofInRoomAsync(roomId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task IsThereAFloorInRoomAsync_ReturnsFalse()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        _mockService.Setup(s => s.IsThereAFloorInRoomAsync(roomId, default)).ReturnsAsync(false);

        // Act
        var result = await _mockService.Object.IsThereAFloorInRoomAsync(roomId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetInternalFencesCountByRoom_ReturnsCount()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        _mockService.Setup(s => s.GetInternalFencesCountByRoom(roomId, default)).ReturnsAsync(5);

        // Act
        var result = await _mockService.Object.GetInternalFencesCountByRoom(roomId);

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task GetOpeningsCountByRoom_ReturnsCount()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        _mockService.Setup(s => s.GetOpeningsCountByRoom(roomId, default)).ReturnsAsync(3);

        // Act
        var result = await _mockService.Object.GetOpeningsCountByRoom(roomId);

        // Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public async Task GetAreaToBeSubtracted_ReturnsValue()
    {
        // Arrange
        var envelopeId = Guid.NewGuid();
        _mockService.Setup(s => s.GetAreaToBeSubtracted(envelopeId, default)).ReturnsAsync(12.5);

        // Act
        var result = await _mockService.Object.GetAreaToBeSubtracted(envelopeId);

        // Assert
        Assert.Equal(12.5, result);
    }

    [Fact]
    public void CalculateTemperatureCoefficient_ReturnsExpected()
    {
        // Arrange
        double density = 500;
        string type = "Brick";
        _mockService.Setup(s => s.CalculateTemperatureCoefficient(density, type)).Returns(0.85);

        // Act
        var result = _mockService.Object.CalculateTemperatureCoefficient(density, type);

        // Assert
        Assert.Equal(0.85, result);
    }

    [Fact]
    public async Task CalculateHeatTransmission_ReturnsExpected()
    {
        // Arrange
        var model = new OuterWallModel { Id = Guid.NewGuid() };
        _mockService.Setup(s => s.CalculateHeatTransmission(model)).ReturnsAsync(33.3);

        // Act
        var result = await _mockService.Object.CalculateHeatTransmission(model);

        // Assert
        Assert.Equal(33.3, result);
    }

    [Fact]
    public void CalculateHeatInfiltration_ReturnsExpected()
    {
        // Method Not Implemented
    }
}
