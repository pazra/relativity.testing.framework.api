﻿using System;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ApplicationFieldCodeUpdateStrategyV1))]
	public class ApplicationFieldCodeUpdateStrategyV1Fixture
	{
		private const int _WORKSPACE_ID = 100000;

		private ApplicationFieldCodeUpdateStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IApplicationFieldCodeGetStrategy> _applicationFieldCodeGetStrategy;
		private Mock<IWorkspaceIdValidator> _workspaceIdValidator;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_applicationFieldCodeGetStrategy = new Mock<IApplicationFieldCodeGetStrategy>();
			_workspaceIdValidator = new Mock<IWorkspaceIdValidator>();

			_sut = new ApplicationFieldCodeUpdateStrategyV1(_mockRestService.Object, _applicationFieldCodeGetStrategy.Object, _workspaceIdValidator.Object);
		}

		[Test]
		public void Update_WithNull_ShouldThrowArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.Update(_WORKSPACE_ID, null));
		}

		[Test]
		public void Update_WithAnyWorkspaceId_ShouldCallValidator()
		{
			_sut.Update(_WORKSPACE_ID, new ApplicationFieldCode());
			_workspaceIdValidator.Verify(x => x.Validate(_WORKSPACE_ID), Times.Once);
		}
	}
}
