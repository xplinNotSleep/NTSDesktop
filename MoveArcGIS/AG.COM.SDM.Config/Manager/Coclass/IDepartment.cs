using System.Collections.Generic;
using AG.COM.SDM.Model;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 部门信息读取接口
    /// </summary>
    public interface IDepartment
    {
        /// <summary>
        /// 获取所有部门信息
        /// </summary>
        /// <returns></returns>
        List<AGSDM_ORG> GetDepartmentList();

        /// <summary>
        /// 根据部门ID获取部门信息
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        AGSDM_ORG GetDepartment(decimal orgID);

        /// <summary>
        /// 获取部门父节点
        /// </summary>
        /// <returns></returns>
        List<AGSDM_ORG> GetMainORG();

        /// <summary>
        /// 获取部门父节点
        /// </summary>
        /// <param name="lstAllORG"></param>
        /// <returns></returns>
        List<AGSDM_ORG> GetMainORG(List<AGSDM_ORG> lstAllORG);
    }
}
