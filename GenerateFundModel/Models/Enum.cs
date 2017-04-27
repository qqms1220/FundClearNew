using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FundClear.Models
{   

    public enum 性别
    {
        男, 女
    }

    public enum 证件类型
    {
        身份证, 护照, 驾照
    }

    public enum 状态
    {
        在职, 离职
    }

    public enum 付息方式
    {
        月付, 季付, 半年付, 年付, 到期一次付清
    }

    public enum 产品状态
    {
        待审核, 在售, 停售
    }
}