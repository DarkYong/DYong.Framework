
using System;

namespace DYong.Code
{
    /// <summary>
    /// 系统操作信息登记类
    /// </summary>
    public class OperatorModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 登录IP地址
        /// </summary>
        public string LoginIPAddress { get; set; }
        /// <summary>
        /// 登录IP名称
        /// </summary>
        public string LoginIPAddressName { get; set; }
        /// <summary>
        /// 登录Tocken
        /// </summary>
        public string LoginToken { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 是否系统人员
        /// </summary>
        public bool IsSystem { get; set; }
    }
}
