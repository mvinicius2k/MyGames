using ApiTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTests.Integration;

public class TagsTest : IClassFixture<HttpFixture>
{

    private readonly HttpFixture _httpFixture;

    public TagsTest(HttpFixture httpFixture)
    {
        _httpFixture = httpFixture;
    }




}
