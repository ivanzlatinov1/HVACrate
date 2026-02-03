using HVACrate.Application.Interfaces;
using HVACrate.Application.Models;
using HVACrate.Application.Models.HVACUsers;
using HVACrate.Domain.ValueObjects;
using Moq;

public class UserServiceTests
{
    private readonly Mock<IUserService> _mockUserService;

    public UserServiceTests()
    {
        _mockUserService = new Mock<IUserService>();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsUsers()
    {
        // Arrange
        var query = new BaseQueryModel();
        var users = new[]
        {
            new HVACUserModel { Id = Guid.NewGuid(), Username = "softunibg" },
            new HVACUserModel { Id = Guid.NewGuid(), Username = "softunidigital" }
        };

        var expectedResult = new Result<HVACUserModel>(users.Length, users);

        _mockUserService
            .Setup(s => s.GetAllAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _mockUserService.Object.GetAllAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("softunibg", result.Items[0].Username);
        Assert.Equal("softunidigital", result.Items[1].Username);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedUser = new HVACUserModel { Id = userId, Username = "softunibg" };

        _mockUserService
            .Setup(s => s.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedUser);

        // Act
        var result = await _mockUserService.Object.GetByIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
        Assert.Equal("softunibg", result.Username);
    }

    [Fact]
    public async Task RemoveAsync_CallsServiceWithCorrectId()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _mockUserService
            .Setup(s => s.RemoveAsync(userId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await _mockUserService.Object.RemoveAsync(userId);

        // Assert
        _mockUserService.Verify();
    }

    [Fact]
    public async Task GetRolesAsync_ReturnsUserRoles()
    {
        // Arrange
        var userIds = new[] { Guid.NewGuid(), Guid.NewGuid() };
        var roles = new Dictionary<Guid, string>
        {
            { userIds[0], "Admin" },
            { userIds[1], "User" }
        };

        _mockUserService
            .Setup(s => s.GetRolesAsync(userIds, It.IsAny<CancellationToken>()))
            .ReturnsAsync(roles);

        // Act
        var result = await _mockUserService.Object.GetRolesAsync(userIds);

        // Assert
        Assert.Equal("Admin", result[userIds[0]]);
        Assert.Equal("User", result[userIds[1]]);
    }

    [Fact]
    public async Task GetRoleAsync_ReturnsRole()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedRole = "Admin";

        _mockUserService
            .Setup(s => s.GetRoleAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedRole);

        // Act
        var result = await _mockUserService.Object.GetRoleAsync(userId);

        // Assert
        Assert.Equal("Admin", result);
    }
}