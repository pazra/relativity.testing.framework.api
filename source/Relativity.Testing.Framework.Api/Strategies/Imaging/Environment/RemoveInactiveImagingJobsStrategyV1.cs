﻿using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class RemoveInactiveImagingJobsStrategyV1 : IRemoveInactiveImagingJobsStrategy
	{
		private readonly IRestService _restService;

		public RemoveInactiveImagingJobsStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public void Remove()
		{
			var url = BuildUrl();
			_restService.Delete(url);
		}

		private string BuildUrl()
		{
			return $"relativity-imaging/v1/environment/jobs/inactive-jobs";
		}
	}
}
