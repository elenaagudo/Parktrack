using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Log
    {
        private ParktrackContext _db;


        public int Id { get; set; }
        public string Endpoint { get; set; }
        public string Request { get; set; }
        public string Details { get; set; }
        public string Code { get; set; }
        public string Error { get; set; }
        public DateTime Timestamp { get; set; }

        public Log(ParktrackContext db)
        {
            _db = db;
        }

        public Log(string endpoint, string request, string details, string code, string error, ParktrackContext db)
        {
            Endpoint = endpoint;
            Request = request;
            Details = details;
            Code = code;
            Error = error;

            Timestamp = DateTime.Now;

            _db = db;
        }

        public void Persist()
        {
            
        }

    }
}
