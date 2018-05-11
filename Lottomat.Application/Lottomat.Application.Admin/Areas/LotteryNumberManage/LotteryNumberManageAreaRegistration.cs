using System.Web.Mvc;

namespace Lottomat.Application.Admin.Areas.LotteryNumberManage
{
    public class LotteryNumberManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "LotteryNumberManage";
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
