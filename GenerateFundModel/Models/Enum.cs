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
        季付, 季末付,三十日付
    }

    public enum 产品状态
    {
        待审核, 在售, 停售
    }

    public enum 产品批次状态
    {
        待审核, 在售,募满, 停售
    }

    public enum 合同状态
    {
        待审核, 生效, 已兑付, 作废,已录入
    }

    public enum 合同流水摘要
    {
        收益兑付, 本金兑付
    }

    public enum 批次流水摘要
    {
        划款, 还息, 还本,还服务费
    }

    public enum 现金管理流水摘要
    {
        申购, 赎回
    }

    /// <summary>
    /// 存入方式，按月存入值1，按天存入值2
    /// </summary>
    public enum 存入方式
    {
        按月存入=1, 按天存入=2
    }
}