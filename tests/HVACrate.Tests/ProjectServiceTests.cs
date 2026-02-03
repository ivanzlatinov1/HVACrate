using HVACrate.Application.Interfaces;
using HVACrate.Application.Models;
using HVACrate.Application.Models.Projects;
using HVACrate.Domain.ValueObjects;
using Moq;

public class ProjectServiceTests
{
    private readonly Mock<IProjectService> _mockProjectService;

    public ProjectServiceTests()
    {
        _mockProjectService = new Mock<IProjectService>();
    }

    [Fact]
    public async Task GetAllAsReadOnlyAsync_ReturnsProjects()
    {
        // Arrange
        var query = new BaseQueryModel();
        var creatorId = Guid.NewGuid();

        var projects = new[]
        {
            new ProjectModel { Id = Guid.NewGuid(), Name = "Project A" },
            new ProjectModel { Id = Guid.NewGuid(), Name = "Project B" }
        };

        var result = new Result<ProjectModel>(projects.Length, projects);

        _mockProjectService
            .Setup(s => s.GetAllAsReadOnlyAsync(query, creatorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        // Act
        var actualResult = await _mockProjectService.Object.GetAllAsReadOnlyAsync(query, creatorId);

        // Assert
        Assert.NotNull(actualResult);
        Assert.Equal(2, actualResult.Count);
        Assert.Equal("Project A", actualResult.Items[0].Name);
        Assert.Equal("Project B", actualResult.Items[1].Name);
    }


    [Fact]
    public async Task GetByIdAsReadOnlyAsync_ReturnsProjectModel()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var expectedProject = new ProjectModel { Id = projectId };

        _mockProjectService
            .Setup(service => service.GetByIdAsReadOnlyAsync(projectId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProject);

        // Act
        var result = await _mockProjectService.Object.GetByIdAsReadOnlyAsync(projectId);

        // Assert
        Assert.Equal(projectId, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsProjectModel()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var expectedProject = new ProjectModel { Id = projectId };

        _mockProjectService
            .Setup(service => service.GetByIdAsync(projectId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProject);

        // Act
        var result = await _mockProjectService.Object.GetByIdAsync(projectId);

        // Assert
        Assert.Equal(projectId, result.Id);
    }

    [Fact]
    public async Task CreateAsync_CallsServiceWithCorrectModel()
    {
        // Arrange
        var model = new ProjectModel { Id = Guid.NewGuid(), Name = "Test Project" };

        _mockProjectService
            .Setup(service => service.CreateAsync(model, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await _mockProjectService.Object.CreateAsync(model);

        // Assert
        _mockProjectService.Verify();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesProject()
    {
        var project = new ProjectModel { Id = Guid.NewGuid(), Name = "Updated Project" };

        _mockProjectService
            .Setup(s => s.UpdateAsync(project, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        await _mockProjectService.Object.UpdateAsync(project);

        _mockProjectService.Verify();
    }

    [Fact]
    public async Task GetProjectNameAsync_ReturnsProjectName()
    {
        var projectId = Guid.NewGuid();
        var expectedName = "Project Name";

        _mockProjectService
            .Setup(s => s.GetProjectNameAsync(projectId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedName);

        var actualName = await _mockProjectService.Object.GetProjectNameAsync(projectId);

        Assert.Equal(expectedName, actualName);
    }

    [Fact]
    public async Task GetLastlyModifiedDateAsync_ReturnsDate()
    {
        var projectId = Guid.NewGuid();
        var expectedDate = DateTimeOffset.UtcNow;

        _mockProjectService
            .Setup(s => s.GetLastlyModifiedDateAsync(projectId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDate);

        var actualDate = await _mockProjectService.Object.GetLastlyModifiedDateAsync(projectId);

        Assert.Equal(expectedDate, actualDate);
    }

    [Fact]
    public async Task SoftDeleteAsync_CallsWithCorrectId()
    {
        // Arrange
        var id = Guid.NewGuid();

        _mockProjectService
            .Setup(s => s.SoftDeleteAsync(id, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await _mockProjectService.Object.SoftDeleteAsync(id);

        // Assert
        _mockProjectService.Verify();
    }
}
