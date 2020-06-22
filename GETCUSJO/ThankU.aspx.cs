using GETCUSJO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GETCUSJO
{
    public partial class ThankU : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    int id = Convert.ToInt32(Request.QueryString["ID"]);

                    
                    DAL dal = new BAL();
                    dal.UpdateonClick(id);
                }
            }
        }
    }
}