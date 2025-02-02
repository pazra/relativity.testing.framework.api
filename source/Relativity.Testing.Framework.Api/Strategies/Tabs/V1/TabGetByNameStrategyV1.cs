﻿using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class TabGetByNameStrategyV1 : IGetWorkspaceEntityByNameStrategy<Tab>
	{
		private readonly IRestService _restService;
		private readonly IObjectService _objectService;

		public TabGetByNameStrategyV1(IRestService restService, IObjectService objectService)
		{
			_restService = restService;
			_objectService = objectService;
		}

		public Tab Get(int workspaceId, string entityName)
		{
			Tab tab = _objectService.Query<Tab>()
				.For(workspaceId)
				.FetchOnlyArtifactID()
				.Where(x => x.Name, entityName)
				.FirstOrDefault();

			if (tab == null)
			{
				return null;
			}

			TabResponseV1 result = _restService.Get<TabResponseV1>($"relativity-data-visualization/v1/{workspaceId}/tabs/{tab.ArtifactID}");

			tab = result.ToTab();

			return tab;
		}
	}
}
