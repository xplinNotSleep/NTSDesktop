namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// 选择类型枚举
    /// </summary>
    public enum AreaSelectType
    {
        TYPE_NONE = 0,
        /// <summary>
        /// 点选
        /// </summary>        
        TYPE_POINT = 1,
        /// <summary>
        /// 多线选择
        /// </summary>
        TYPE_POLYLINE = 2,
        /// <summary>
        /// 多边形选择
        /// </summary>
        TYPE_POLYGON = 3,
        /// <summary>
        /// 矩形选择
        /// </summary>
        TYPE_RECT = 4,
        /// <summary>
        /// 当前屏幕选择
        /// </summary>
        TYPE_SCREEN = 5,
        /// <summary>
        /// 圆选
        /// </summary>
        TYPE_CIRCLE = 6
    }

    /// <summary>
    /// 用户区域选择接口
    /// </summary>
    public interface ISelAreaForm
    {
        /// <summary>
        /// 获取用户区域选择控件
        /// </summary>
        ControlSelArea SelArea { get; }
    }
}
