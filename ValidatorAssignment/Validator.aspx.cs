using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ValidatorAssignment
{
    public partial class Validator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Checks", "Checks();", true);

                lblmsg.Visible = true;

                lblmsg.Text = "!!!!!!!!!!!!!!!!!!!!Form submitted successfully!!!!!!!!!!!!!!!!!!!!!!!!";

            }
        }
    }
}