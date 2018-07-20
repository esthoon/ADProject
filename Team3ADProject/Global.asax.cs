﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Team3ADProject
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Session_Start(object sender, EventArgs e)
        {

            //alan--start
            Session["role"] = "12";


            //alan-> need to code this using in User is in role after IIS setup
            /*if (User.IsInRole("storemanager"))
            {
                Session["role"] = "storemanager";

            }
            else if (User.IsInRole("storesup"))
            {
                Session["role"] = "storesup";
            }*/
            //alan-- end

        }



    }
}