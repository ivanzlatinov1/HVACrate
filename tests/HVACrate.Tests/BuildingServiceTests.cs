using HVACrate.Application.Interfaces;
using HVACrate.Application.Models;
using HVACrate.Application.Models.Buildings;
using HVACrate.Domain.ValueObjects;
using Moq;

public class BuildingServiceTests
{
    private readonly Mock<IBuildingService> _mockBuildingService;

    public BuildingServiceTests()
    {
        _mockBuildingService = new Mock<IBuildingService>();
    }

    [Fact]
    public async Task GetAllAsReadOnlyAsync_ReturnsBuildings()
    {
        // Arrange
        var query = new BaseQueryModel();
        var projectId = Guid.NewGuid();

        var buildings = new[]
        {
            new BuildingModel { Id = Guid.NewGuid(), Name = "Building A" },
            new BuildingModel { Id = Guid.NewGuid(), Name = "Building B" }
        };
        var expectedResult = new Result<BuildingModel>(buildings.Length, buildings);

        _mockBuildingService
            .Setup(s => s.GetAllAsReadOnlyAsync(query, projectId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _mockBuildingService.Object.GetAllAsReadOnlyAsync(query, projectId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Building A", result.Items[0].Name);
        Assert.Equal("Building B", result.Items[1].Name);
    }

    [Fact]
    public async Task GetByIdAsReadOnlyAsync_ReturnsBuilding()
    {
        var buildingId = Guid.NewGuid();
        var expected = new BuildingModel { Id = buildingId, Name = "Test Building" };

        _mockBuildingService
            .Setup(s => s.GetByIdAsReadOnlyAsync(buildingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await _mockBuildingService.Object.GetByIdAsReadOnlyAsync(buildingId);

        Assert.NotNull(result);
        Assert.Equal(buildingId, result.Id);
        Assert.Equal("Test Building", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsEditableBuilding()
    {
        var id = Guid.NewGuid();
        var building = new BuildingModel { Id = id, Name = "Editable Building" };

        _mockBuildingService
            .Setup(s => s.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(building);

        var result = await _mockBuildingService.Object.GetByIdAsync(id);

        Assert.NotNull(result);
        Assert.Equal("Editable Building", result.Name);
    }

    [Fact]
    public async Task CreateAsync_CreatesBuilding()
    {
        var model = new BuildingModel { Id = Guid.NewGuid(), Name = "New Building" };

        _mockBuildingService
            .Setup(s => s.CreateAsync(model, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        await _mockBuildingService.Object.CreateAsync(model);

        _mockBuildingService.Verify();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesBuilding()
    {
        var model = new BuildingModel { Id = Guid.NewGuid(), Name = "Updated Building" };

        _mockBuildingService
            .Setup(s => s.UpdateAsync(model, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        await _mockBuildingService.Object.UpdateAsync(model);

        _mockBuildingService.Verify();
    }

    [Fact]
    public async Task SoftDeleteAsync_DeletesBuilding()
    {
        var id = Guid.NewGuid();

        _mockBuildingService
            .Setup(s => s.SoftDeleteAsync(id, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        await _mockBuildingService.Object.SoftDeleteAsync(id);

        _mockBuildingService.Verify();
    }

    [Fact]
    public async Task GetTotalFloors_ReturnsFloorCount()
    {
        var id = Guid.NewGuid();
        var expectedFloors = 5;

        _mockBuildingService
            .Setup(s => s.GetTotalFloors(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedFloors);

        var result = await _mockBuildingService.Object.GetTotalFloors(id);

        Assert.Equal(expectedFloors, result);
    }
}