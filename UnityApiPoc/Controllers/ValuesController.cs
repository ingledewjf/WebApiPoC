namespace UnityApiPoc.Controllers
{
    using System.Web.Http;

    using UnityApiPoc.Values;

    public class ValuesController : ApiController
    {
        private readonly IValuesProvider _provider;

        private readonly IDisposableValuesProvider _disposableProvider;

        public ValuesController(IValuesProvider provider, IDisposableValuesProvider disposableProvider)
        {
            _provider = provider;
            _disposableProvider = disposableProvider;
        }

        // GET api/values
        public string Get()
        {
            return _provider.GetValues();
        }

        // GET api/values/5
        public int Get(int id)
        {
            return _disposableProvider.GetValues();
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
