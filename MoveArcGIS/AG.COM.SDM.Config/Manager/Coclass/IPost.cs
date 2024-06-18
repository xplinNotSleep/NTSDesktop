using System.Collections.Generic;
using AG.COM.SDM.Model;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 岗位信息读取接口
    /// </summary>
    public interface IPost
    {
        /// <summary>
        /// 获取所有的岗位
        /// </summary>
        /// <returns>岗位信息列表</returns>
        List<AGSDM_POSITION> GetPostList(); 

        /// <summary>
        /// 根据岗位ID获取岗位
        /// </summary>
        /// <param name="postID">岗位编号</param>
        /// <returns>岗位信息实例</returns>
        AGSDM_POSITION GetPost(decimal postID);

        /// <summary>
        /// 得到某个用户的所有岗位
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<AGSDM_POSITION> GetPosts(decimal userID);

        /// <summary>
        /// 获取所有用户岗位信息
        /// </summary>
        /// <returns></returns>
        List<AGSDM_USERPOSITIONRLT> GetUserPostions();
    }
}
