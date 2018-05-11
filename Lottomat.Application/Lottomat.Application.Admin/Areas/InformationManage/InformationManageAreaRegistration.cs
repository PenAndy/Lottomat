﻿using System.Web.Mvc;

namespace Lottomat.Application.Admin.Areas.InformationManage
{
    public class InformationManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "InformationManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                this.AreaName + "_Default",
                this.AreaName + "/{controller}/{action}/{id}",
                new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Lottomat.Application.Admin.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}