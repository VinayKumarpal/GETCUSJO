using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GETCUSJO.Model
{
    public class Email
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Subject { get; set; }
        public bool IsClicked { get; set; }
        public string ID { get; set; }
        public string Body { get; set; }
    }
}