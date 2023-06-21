using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;

namespace Architecture.Tests;

public class ArchitectureAxessTests
{
	private const string DomainNamespace = "Axess.Domain";
	private const string ApplicationNamespace = "Axess.Application";
	private const string InfrastructureNamespace = "Axess.Infrastructure";
	private const string PresentationNamespace = "Axess.Presentation";
	private const string WebApiNamespace = "Axess.Api";

	[Fact]
	public void Domain_Should_Not_HaveDependencyOnOtherProjetcs()
	{
		// Arrange
		///var assembly = typeof(Axess.Common.Domain.ValueObjects.ValueObject).Assembly;
		var assembly = Assembly.Load(DomainNamespace);

		var otherProjects = new[]
		{
			ApplicationNamespace,
			InfrastructureNamespace,
			PresentationNamespace,
			WebApiNamespace,
		};

		// Act
		var testResult = Types
			.InAssembly(assembly)
			.ShouldNot()
			.HaveDependencyOnAll(otherProjects).GetResult();
		// Assert 
		testResult.IsSuccessful.Should().BeTrue();

	}

	[Fact]
	public void Application_Should_Not_HaveDependencyOnOtherProjetcs()
	{
		// Arrange
		///var assembly = typeof(Axess.Common.Domain.ValueObjects.ValueObject).Assembly;
		var assembly = Assembly.Load(ApplicationNamespace);

		var otherProjects = new[]
		{
			InfrastructureNamespace,
			PresentationNamespace,
			WebApiNamespace,
		};

		// Act
		var testResult = Types
			.InAssembly(assembly)
			.ShouldNot()
			.HaveDependencyOnAll(otherProjects).GetResult();
		// Assert 
		testResult.IsSuccessful.Should().BeTrue();

	}


	[Fact]
	public void Handlers_Should_Have_DependencyOnDomain()
	{
		// Arrange
		///var assembly = typeof(Axess.Common.Domain.ValueObjects.ValueObject).Assembly;
		var assembly = Assembly.Load(ApplicationNamespace);


		// Act
		var testResult = Types
			.InAssembly(assembly)
			.That()
			.HaveNameEndingWith("Handler")
			.Should()
			.HaveDependencyOn(DomainNamespace).GetResult();
		// Assert 
		testResult.IsSuccessful.Should().BeTrue();

	}

	[Fact]
	public void Queries_Should_Be_Sealed_And_Have_DependencyOnMediatR()
	{
		// Arrange
		///var assembly = typeof(Axess.Common.Domain.ValueObjects.ValueObject).Assembly;
		var assembly = Assembly.Load(ApplicationNamespace);


		// Act
		var testResult = Types
			.InAssembly(assembly)
			.That()
			.HaveNameEndingWith("Query")
			.Should()
			.BeSealed().And().HaveDependencyOn("MediatR")
			.GetResult();
		// Assert 
		testResult.IsSuccessful.Should().BeTrue();

	}

	[Fact]
	public void Infrastructure_Should_Not_HaveDependencyOnOtherProjetcs()
	{
		// Arrange
		///var assembly = typeof(Axess.Common.Domain.ValueObjects.ValueObject).Assembly;
		var assembly = Assembly.Load(InfrastructureNamespace);

		var otherProjects = new[]
		{
			PresentationNamespace,
			WebApiNamespace,
		};

		// Act
		var testResult = Types
			.InAssembly(assembly)
			.ShouldNot()
			.HaveDependencyOnAll(otherProjects).GetResult();
		// Assert 
		testResult.IsSuccessful.Should().BeTrue();

	}

	[Fact]
	public void Presentation_Should_Not_HaveDependencyOnOtherProjetcs()
	{
		// Arrange
		///var assembly = typeof(Axess.Common.Domain.ValueObjects.ValueObject).Assembly;
		var assembly = Assembly.Load(PresentationNamespace);

		var otherProjects = new[]
		{
			InfrastructureNamespace,
			WebApiNamespace,
		};

		// Act
		var testResult = Types
			.InAssembly(assembly)
			.ShouldNot()
			.HaveDependencyOnAll(otherProjects).GetResult();
		// Assert 
		testResult.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void Controllers_Should_Have_DependencyOnMediatR()
	{
		// Arrange
		///var assembly = typeof(Axess.Common.Domain.ValueObjects.ValueObject).Assembly;
		var assembly = Assembly.Load(PresentationNamespace);

		// Act
		var testResult = Types
		.InAssembly(assembly)
			.That()
			.HaveNameEndingWith("ControllerBase")
			.Should()
			.HaveDependencyOn("MediatR")
			.GetResult();
		// Assert 
		testResult.IsSuccessful.Should().BeTrue();

	}
}