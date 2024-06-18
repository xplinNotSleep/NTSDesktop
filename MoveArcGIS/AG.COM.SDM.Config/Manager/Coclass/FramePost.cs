using System.Collections;
using System.Collections.Generic;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 框架实现的岗位表访问类
    /// </summary>
    public class FramePost : IPost 
    {
        #region IPost 成员

        /// <summary>
        /// 获取所有的岗位
        /// </summary>
        /// <returns>岗位列表</returns>
        public List<AGSDM_POSITION> GetPostList()
        {
            List<AGSDM_POSITION> lstPositions = new List<AGSDM_POSITION>();
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            IList lstEntity = tEntityHandler.GetEntities("from AGSDM_POSITION");
            for (int i = 0; i < lstEntity.Count; i++)
            {
                lstPositions.Add(lstEntity[i] as AGSDM_POSITION);
            }
            return lstPositions;
        }

        /// <summary>
        /// 根据岗位ID获取岗位
        /// </summary>
        /// <param name="postID">岗位ID</param>
        /// <returns>岗位信息</returns>
        public AGSDM_POSITION GetPost(decimal postID)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            IList lstEntity = tEntityHandler.GetEntities("from AGSDM_POSITION where ID =" + postID);
            if (lstEntity.Count > 0)
                return lstEntity[0] as AGSDM_POSITION;
            else
                return null;
        }
         
        /// <summary>
        /// 得到某个用户的所有岗位
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>岗位信息列表</returns>
        public List<AGSDM_POSITION> GetPosts(decimal userID)
        {
            List<AGSDM_POSITION> lstPosition = new List<AGSDM_POSITION>();
            List<AGSDM_USERPOSITIONRLT> lstUserPos = new List<AGSDM_USERPOSITIONRLT>();
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            IList lstEntity = tEntityHandler.GetEntities("from AGSDM_USERPOSITIONRLT where USER_ID =" + userID);
            for (int i = 0; i < lstEntity.Count; i++)
            {
                lstUserPos.Add(lstEntity[i] as AGSDM_USERPOSITIONRLT);
            }

            string strHQL;
            for (int i = 0; i < lstUserPos.Count; i++)
            {
                strHQL = "from AGSDM_POSITION where  ID =" + lstUserPos[i].POS_ID;
                AGSDM_POSITION pos = tEntityHandler.GetEntity<AGSDM_POSITION>(strHQL);
                if (pos != null)
                {
                    lstPosition.Add(pos);
                }
            }

            return lstPosition;
        }

        /// <summary>
        /// 获取所有用户岗位信息
        /// </summary>
        /// <returns>用户岗位关联信息</returns>
        public List<AGSDM_USERPOSITIONRLT> GetUserPostions()
        {
            List<AGSDM_USERPOSITIONRLT> lstUserPosition = new List<AGSDM_USERPOSITIONRLT>();
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            IList lstEntity = tEntityHandler.GetEntities("from AGSDM_USERPOSITIONRLT");
            for (int i = 0; i < lstEntity.Count; i++)
            {
                AGSDM_USERPOSITIONRLT userPos = lstEntity[i] as AGSDM_USERPOSITIONRLT;
                lstUserPosition.Add(userPos);
            }
            return lstUserPosition;
        }

        #endregion
    }
}
