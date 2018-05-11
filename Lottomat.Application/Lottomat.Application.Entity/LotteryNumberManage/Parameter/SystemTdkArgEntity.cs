using System.Linq;

namespace Lottomat.Application.Entity.LotteryNumberManage.Parameter
{
    /// <summary>
    /// 系统TDK
    /// </summary>
    public class SystemTdkArgEntity : BaseParameterEntity
    {
        /// <summary>
        /// TDK范围值域
        /// </summary>
        private readonly string[] AreaCodeArr = new[] { "MainPageTDK", "LotteryTDK", "SpecialTDK", "OpeningNumberTDK", "TestNumberTDK", "PlayTDK", "CommonTDK" };

        private string _areaCode;
        /// <summary>
        /// TDK范围
        /// <para>值为：MainPageTDK-主页，LotteryTDK-彩种，SpecialTDK-专题，OpeningNumberTDK-开机号，TestNumberTDK-试机号，PlayTDK-玩法规则，CommonTDK-公共</para>
        /// <para>可传入多个，多个用|隔开。此时值域为MainPageTDK，CommonTDK，SpecialTDK</para>
        /// </summary>
        public string AreaCode
        {
            get => _areaCode;
            set
            {
                if (AreaCodeArr.Contains(value))
                {
                    //是否传入多个
                    if (value.Contains("|"))
                    {
                        string[] valueArr = new[] { "MainPageTDK", "SpecialTDK", "CommonTDK" };

                        _areaCode = valueArr.Contains(value) ? value : "CommonTDK";
                    }
                    else
                    {
                        _areaCode = value;
                    }
                }
                else
                {
                    _areaCode = "CommonTDK";
                }
            }
        }

        private string _enumCode;
        /// <summary>
        /// 彩种枚举码，仅当AreaCode为LotteryTDK，OpeningNumberTDK，TestNumberTDK，PlayTDK时才传入对应彩种编码
        /// <para>可传入多个，多个用|隔开。此时AreaCode值域为LotteryTDK，OpeningNumberTDK，TestNumberTDK，PlayTDK其中之一</para>
        /// </summary>
        public string EnumCode
        {
            get => _enumCode;
            set
            {
                if (!string.IsNullOrEmpty(this._areaCode))
                {
                    string[] valueArr = new[] { "LotteryTDK", "OpeningNumberTDK", "TestNumberTDK", "PlayTDK" };
                    if (value.Contains("|"))
                    {
                        _enumCode = valueArr.Contains(this._areaCode) ? value : "";
                    }
                    else
                    {
                        _enumCode = value;
                    }
                }
            }
        }
    }
}