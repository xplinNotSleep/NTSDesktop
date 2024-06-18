using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 要素编辑帮助类
    /// </summary>
    public class EditHelper
    {
        /// <summary>
        /// 获取部分刷新的Envelope
        /// </summary>
        /// <param name="tEnvelopBefore"></param>
        /// <param name="tEnvelopAfter"></param>
        /// <returns></returns>
        public static IEnvelope GetPartialRefreshEnvelop(IEnvelope tEnvelopBefore, IEnvelope tEnvelopAfter)
        {
            IEnvelope tEnvelop = tEnvelopAfter;

            //两个Envelop合二为一，为了保证刷新新旧范围
            tEnvelop.Union(tEnvelopBefore);
            //扩大一点
            tEnvelop.Expand(5, 5, false);

            return tEnvelop;
        }

        /// <summary>
        /// 获取部分刷新的Envelope
        /// </summary>
        /// <param name="tEnvelopSingle"></param>
        /// <returns></returns>
        public static IEnvelope GetPartialRefreshEnvelop(IEnvelope tEnvelopSingle)
        {
            IEnvelope tEnvelop = tEnvelopSingle;
          
            //扩大一点
            tEnvelop.Expand(5, 5, false);

            return tEnvelop;
        }
    }
}
