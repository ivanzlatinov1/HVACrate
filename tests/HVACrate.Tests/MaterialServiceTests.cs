using HVACrate.Application.Interfaces;
using HVACrate.Application.Models;
using HVACrate.Application.Models.Materials;
using Moq;

public class MaterialServiceTests
{
    private readonly Mock<IMaterialService> _mockMaterialService;

    public MaterialServiceTests()
    {
        _mockMaterialService = new Mock<IMaterialService>();
    }

    [Fact]
    public async Task GetAllAsReadOnlyAsync_ReturnsMaterials()
    {
        // Arrange
        var query = new BaseQueryModel();
        var expected = new List<MaterialModel>
        {
            new MaterialModel { Id = Guid.NewGuid(), Type = "Concrete", ThermalConductivity = 1.9 },
            new MaterialModel { Id = Guid.NewGuid(), Type = "Brick", ThermalConductivity = 3.2 }
        };

        _mockMaterialService
            .Setup(s => s.GetAllAsReadOnlyAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _mockMaterialService.Object.GetAllAsReadOnlyAsync(query);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, m => m.Type == "Concrete" && m.ThermalConductivity == 1.9);
        Assert.Contains(result, m => m.Type == "Brick" && m.ThermalConductivity == 3.2);
    }

    [Fact]
    public async Task GetByIdAsReadOnlyAsync_ReturnsMaterial()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expected = new MaterialModel { Id = id, Type = "Steel", ThermalConductivity = 50.0 };

        _mockMaterialService
            .Setup(s => s.GetByIdAsReadOnlyAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _mockMaterialService.Object.GetByIdAsReadOnlyAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Steel", result.Type);
        Assert.Equal(50.0, result.ThermalConductivity);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsEditableMaterial()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expected = new MaterialModel { Id = id, Type = "Wood", ThermalConductivity = 0.12 };

        _mockMaterialService
            .Setup(s => s.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _mockMaterialService.Object.GetByIdAsync(id);

        // Assert
        Assert.Equal(id, result.Id);
        Assert.Equal("Wood", result.Type);
        Assert.Equal(0.12, result.ThermalConductivity);
    }

    [Fact]
    public async Task CreateAsync_CreatesMaterial()
    {
        // Arrange
        var model = new MaterialModel { Id = Guid.NewGuid(), Type = "Glass", ThermalConductivity = 0.8 };

        _mockMaterialService
            .Setup(s => s.CreateAsync(model, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await _mockMaterialService.Object.CreateAsync(model);

        // Assert
        _mockMaterialService.Verify();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesMaterial()
    {
        // Arrange
        var model = new MaterialModel { Id = Guid.NewGuid(), Type = "Updated Glass", ThermalConductivity = 1.1 };

        _mockMaterialService
            .Setup(s => s.UpdateAsync(model, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await _mockMaterialService.Object.UpdateAsync(model);

        // Assert
        _mockMaterialService.Verify();
    }

    [Fact]
    public async Task SoftDeleteAsync_DeletesMaterial()
    {
        // Arrange
        var id = Guid.NewGuid();

        _mockMaterialService
            .Setup(s => s.SoftDeleteAsync(id, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await _mockMaterialService.Object.SoftDeleteAsync(id);

        // Assert
        _mockMaterialService.Verify();
    }
}
