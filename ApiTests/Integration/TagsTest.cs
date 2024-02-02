using Api;
using ApiTests.Fixtures;
using GreenDonut;
using HotChocolate;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter;
using Snapshooter.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL.SystemTextJson;
namespace ApiTests.Integration;

public class TagsTest : IClassFixture<HttpFixture>
{

	private readonly HttpFixture _httpFixture;

	public TagsTest(HttpFixture httpFixture)
	{
		_httpFixture = httpFixture;
	}

	[Fact]
	public async Task SayHello_HelloIsReturned()
	{
		var client = _httpFixture.GetGraphQLClient();
		var query = GraphQL.Types.Schema.For(@"{ 
			book { 
				author { 
					name 
				} 
			} 
		}");
		
	

        var message = new HttpRequestMessage
        {
            Content = new StringContent(query),
			Method = HttpMethod.Post

        };
        var result = await client.SendAsync(message);

		var strResult = await result.Content.ReadAsStringAsync();
		strResult.MatchSnapshot();

	}


}
