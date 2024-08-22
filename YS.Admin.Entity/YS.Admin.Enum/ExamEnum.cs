using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Admin.Enum
{
    public enum EvaluationResultsEnum
    {
        [Description("考完直接出")]
        Yes = 1,

        [Description("人工阅卷后")]
        No = 0
    }
    public enum FrepeatEnum
    {
        [Description("可以补考")]
        Yes = 1,

        [Description("仅一次")]
        No = 0
    }
    public enum FtwiceEnum
    {
        [Description("可补考一次")]
        Yes = 1,
        [Description("不限次数")]
        No = 0
    }
    public enum PublishAnswerEnum
    {
        [Description("公布")]
        Yes = 1,
        [Description("不公布")]
        No = 0
    }
    public enum IsAloneEnum
    {
        [Description("一道道题作答")]
        Yes = 1,
        [Description("试题全部罗列")]
        No = 0
    }
}
