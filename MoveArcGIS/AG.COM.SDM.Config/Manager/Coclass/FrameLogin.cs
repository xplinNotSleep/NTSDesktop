using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.COM.SDM.Config
{
   public class FrameLogin
    {
        public bool CreateUser(AGSDM_SYSTEM_USER m_User)
        {
            try
            {
                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
                AGSDM_SYSTEM_USER tUserSameName = tEntityHandler.GetEntity<AGSDM_SYSTEM_USER>("from AGSDM_SYSTEM_USER as t where t.NAME_EN=?", m_User.NAME_EN);
                if (tUserSameName==null)
                {

                    //string strHQL = "from AGSDM_SYSTEM_USER ORDER BY USER_ID DESC";
                    //IList tListUser = tEntityHandler.GetEntities(strHQL);
                    //if (tListUser.Count > 0)
                    //{
                    //    AGSDM_SYSTEM_USER userTemp = tListUser[0] as AGSDM_SYSTEM_USER;
                    //    if (userTemp != null)
                    //    {
                    //        m_User.USER_ID = userTemp.USER_ID;
                    //        m_User.USER_ID++;
                    //    }
                    //}
                    //添加到用户表
                    object obj = m_User;
                    tEntityHandler.AddEntity(obj);
                    AGSDM_USER_ROLE tUserRole = new AGSDM_USER_ROLE();
                    tUserRole.USER_ID = m_User.USER_ID.ToString();
                    //默认是管理员
                    tUserRole.ROLE_ID = "21";
                    tEntityHandler.AddEntity(tUserRole);
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
           
        }
    }
}
