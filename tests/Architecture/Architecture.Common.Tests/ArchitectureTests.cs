using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;

namespace Architecture.Tests;

public class ArchitectureTests
{
	private const string DomainNamespace = "Axess.Common.Domain";
	private const string ApplicationNamespace = "Axess.Common.Application";
	private const string InfrastructureNamespace = "Axess.Common.Infrastructure";
	private const string PresentationNamespace = "Axess.Common.Presentation";

	[Fact]
	public void Domain_Should_Not_HaveDependencyOnOtherProjetcs()
	{
		// Arrange
		///var assembly = typeof(Axess.Domain.ValueObjects.ValueObject).Assembly;
		var assembly = Assembly.Load(DomainNamespace);

		var otherProjects = new[]
		{
			ApplicationNamespace,
			InfrastructureNamespace,
			PresentationNamespace,
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
		var assembly = Assembly.Load(ApplicationNamespace);

		var otherProjects = new[]
		{
			InfrastructureNamespace,
			PresentationNamespace,
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
	public void Infrastructure_Should_Not_HaveDependencyOnOtherProjetcs()
	{
		// Arrange
		///var assembly = typeof(Axess.Common.Domain.ValueObjects.ValueObject).Assembly;
		var assembly = Assembly.Load(InfrastructureNamespace);

		var otherProjects = new[]
		{
			PresentationNamespace,
		};

		// Act
		var testResult = Types
			.InAssembly(assembly)
			.ShouldNot()
			.HaveDependencyOnAll(otherProjects).GetResult();
		// Assert 
		testResult.IsSuccessful.Should().BeTrue();

	}

}