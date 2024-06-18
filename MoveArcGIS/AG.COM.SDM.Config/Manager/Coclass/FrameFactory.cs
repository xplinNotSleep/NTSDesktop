namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 框架实现的系统表访问工厂，提供一系列对系统表的访问接口，如用户表，角色表，部门表，菜单表的访问。
    /// </summary>
    public class FrameFactory : AbstractFactory 
    {
        /// <summary>
        /// 创建岗位表访问接口
        /// </summary>
        /// <returns>岗位表访问接口</returns>
        public override IPost CreatePost()
        {
            IPost post = new FramePost();
            return post;
        }
         
        /// <summary>
        /// 创建用户表访问接口
        /// </summary>
        /// <returns>用户表访问接口</returns>
        public override IUser CreateUser()
        {
            IUser user = new FrameUser();
            return user;
        }

        /// <summary>
        /// 创建部门表访问接口
        /// </summary>
        /// <returns>部门表访问接口</returns>
        public override IDepartment CreateDepartment()
        {
            IDepartment department = new FrameDepartment();
            return department;
        }

        /// <summary>
        /// 创建菜单表访问接口
        /// </summary>
        /// <returns>菜单表访问接口</returns>
        public override IMenu CreateMenu()
        {
            IMenu menu = new FrameMenu();
            return menu;
        }

        /// <summary>
        /// 创建角色表访问接口
        /// </summary>
        /// <returns>角色表访问接口</returns>
        public override IRole CreateRole()
        {
            IRole role = new FrameRole();
            return role;
        }
    }
}
