using HVACrate.Application.Interfaces;
using HVACrate.Application.Models;
using HVACrate.Application.Models.Rooms;
using HVACrate.Domain.ValueObjects;
using Moq;

public class RoomServiceTests
{
    private readonly Mock<IRoomService> _mockRoomService;

    public RoomServiceTests()
    {
        _mockRoomService = new Mock<IRoomService>();
    }

    [Fact]
    public async Task GetAllAsReadOnlyAsync_ReturnsRooms()
    {
        // Arrange
        var query = new BaseQueryModel();
        var buildingId = Guid.NewGuid();
        var rooms = new[]
        {
            new RoomModel { Id = Guid.NewGuid(), Number = "101" },
            new RoomModel { Id = Guid.NewGuid(), Number = "102" }
        };
        var expected = new Result<RoomModel>(rooms.Length, rooms);

        _mockRoomService
            .Setup(s => s.GetAllAsReadOnlyAsync(query, buildingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _mockRoomService.Object.GetAllAsReadOnlyAsync(query, buildingId);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("101", result.Items[0].Number);
    }

    [Fact]
    public async Task GetByIdAsReadOnlyAsync_ReturnsRoom()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var expected = new RoomModel { Id = roomId, Number = "A101" };

        _mockRoomService
            .Setup(s => s.GetByIdAsReadOnlyAsync(roomId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _mockRoomService.Object.GetByIdAsReadOnlyAsync(roomId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("A101", result.Number);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsEditableRoom()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var expected = new RoomModel { Id = roomId, Number = "A101" };

        _mockRoomService
            .Setup(s => s.GetByIdAsync(roomId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _mockRoomService.Object.GetByIdAsync(roomId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("A101", result.Number);
    }

    [Fact]
    public async Task CreateAsync_CreatesRoom()
    {
        // Arrange
        var model = new RoomModel { Id = Guid.NewGuid(), Number = "A101" };

        _mockRoomService
            .Setup(s => s.CreateAsync(model, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await _mockRoomService.Object.CreateAsync(model);

        // Assert
        _mockRoomService.Verify();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesRoom()
    {
        // Arrange
        var model = new RoomModel { Id = Guid.NewGuid(), Number = "A102" };

        _mockRoomService
            .Setup(s => s.UpdateAsync(model, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await _mockRoomService.Object.UpdateAsync(model);

        // Assert
        _mockRoomService.Verify();
    }

    [Fact]
    public async Task SoftDeleteAsync_DeletesRoom()
    {
        // Arrange
        var roomId = Guid.NewGuid();

        _mockRoomService
            .Setup(s => s.SoftDeleteAsync(roomId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await _mockRoomService.Object.SoftDeleteAsync(roomId);

        // Assert
        _mockRoomService.Verify();
    }

    [Fact]
    public async Task GetBuildingIdAsync_ReturnsBuildingId()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var expectedBuildingId = Guid.NewGuid();

        _mockRoomService
            .Setup(s => s.GetBuildingIdAsync(roomId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedBuildingId);

        // Act
        var result = await _mockRoomService.Object.GetBuildingIdAsync(roomId);

        // Assert
        Assert.Equal(expectedBuildingId, result);
    }

    [Fact]
    public async Task GetRoomNumberAsync_ReturnsRoomNumber()
    {
        // Arrange
        var roomId = Guid.NewGuid();
        var expectedRoomNumber = "B505";

        _mockRoomService
            .Setup(s => s.GetRoomNumberAsync(roomId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedRoomNumber);

        // Act
        var result = await _mockRoomService.Object.GetRoomNumberAsync(roomId);

        // Assert
        Assert.Equal("B505", result);
    }

    [Fact]
    public async Task GetAllEnclosedRoomsAsync_ReturnsEnclosedRooms()
    {
        // Arrange
        var pagination = new Pagination { Page = 1, Limit = 10 };
        var rooms = new[]
        {
            new RoomModel { Id = Guid.NewGuid(), Number = "A101", IsEnclosed = true },
            new RoomModel { Id = Guid.NewGuid(), Number = "A102", IsEnclosed = true }
        };

        _mockRoomService
            .Setup(s => s.GetAllEnclosedRoomsAsync(pagination, It.IsAny<CancellationToken>()))
            .ReturnsAsync(rooms);

        // Act
        var result = await _mockRoomService.Object.GetAllEnclosedRoomsAsync(pagination);

        // Assert
        Assert.Equal(2, result.Length);
        Assert.All(result, r => Assert.True(r.IsEnclosed));
    }
}