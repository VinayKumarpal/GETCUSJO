using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GETCUSJO.Model
{
    public abstract class DAL
    {
        public static string baseUrl { get; set; }
        public abstract bool GetEmail();
        public abstract bool UpdateonClick(int ID);
    }
}