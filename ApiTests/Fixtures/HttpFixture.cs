using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTests.Fixtures
{
    public class HttpFixture
    {



        public HttpClient GetClient()
            => new()
            {
                BaseAddress = new Uri("http://localhost:5000/api/")
            };
        public HttpClient GetGraphQLClient()
       => new()
       {
           BaseAddress = new Uri("http://localhost:5000/api/graphql/")
       };
    }
}
