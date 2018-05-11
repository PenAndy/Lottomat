﻿using System.Web.Mvc;

namespace Lottomat.Application.Admin.Areas.GalleryManage
{
    public class GalleryManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "GalleryManage";
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
