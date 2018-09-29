using Aerospike.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AerospikeDemo.Controllers
{
    public class DataController : ApiController
    {
        AerospikeClient aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
        string nameSpace = "AirEngine";
        string setName = "Nihit";

        [HttpPost]
        public List<Record> GetRecordByIds([FromBody] String[] ids)
        {
            List<Record> records = new List<Record>();
            foreach(string id in ids)
            {
                var key = new Key(nameSpace, setName, id);
                Record result = aerospikeClient.Get(new WritePolicy(), key);
                records.Add(result);
            }
            return records;
        }

        [HttpPut]
        public void UpdateRecords([FromUri] string id, [FromBody] string content)
        {
            var key = new Key(nameSpace, setName, id);
            var bin = new Bin("content", content);
            aerospikeClient.Put(new WritePolicy(), key, new Bin[] { bin });
        }

        [HttpDelete]
        public void DeleteRecord([FromBody] string id)
        {
            var key = new Key(nameSpace, setName, id);
            aerospikeClient.Delete(new WritePolicy(), key);
        }
    }
}
