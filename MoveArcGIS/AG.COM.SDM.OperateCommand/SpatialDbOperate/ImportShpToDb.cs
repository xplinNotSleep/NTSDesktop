using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility.Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AG.COM.SDM.DataOperate;

namespace AG.COM.SDM.OperateCommand
{
    /// <summary>
    /// 将矢量数据导入到数据库中
    /// </summary>
    public class ImportShpToDb : BaseCommand, IUseImage
    {
        private IHookHelper m_hookHelper = new HookHelper();

        public ImportShpToDb()
        {
            this.m_caption = "矢量数据入库";
            this.m_name = GetType().Name;
            this.m_toolTip = "矢量数据入库";
            this.m_message = "矢量数据入库";

            m_Image16 = Properties.Resources.lv16;
            m_Image32 = Properties.Resources.lv32;
        }

        private Image m_Image16;
        private Image m_Image32;
        public Image Image16
        {
            get
            {
                return m_Image16;
            }
            set { m_Image16 = value; }
        }
        public Image Image32
        {
            get { return m_Image32; }
            set { m_Image16 = value; }
        }

        public override void OnClick()
        {
            try
            {
                FrmShpImportToDb frmShpToDb = new FrmShpImportToDb();
                IDockDocumentService tDockDocumentService = m_hookHelper.DockDocumentService;
                if (!tDockDocumentService.ContainsDocument(frmShpToDb.TabText))
                {
                    m_hookHelper.DockDocumentService.AddDockDocument(frmShpToDb.TabText,
                        frmShpToDb, DockState.Document);
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.LogError(ex.Message, ex);
            }
        }

        public override void OnCreate(object hook)
        {
            m_hookHelper.Framework = hook as IFramework;
        }

    }
}
