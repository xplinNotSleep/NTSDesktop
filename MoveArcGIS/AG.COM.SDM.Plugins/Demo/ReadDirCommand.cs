﻿using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AG.COM.SDM.CommonFun;
using AG.COM.SDM.Framework.DocumentView;

namespace AG.COM.SDM.Plugins.Demo
{
    public class ReadDirCommand : BaseCommand, IUseImage
    {
        private IHookHelper m_hookHelper = new HookHelper();

        public ReadDirCommand()
        {
            this.m_caption = "选择路径读取文件";
            this.m_name = GetType().Name;
            this.m_toolTip = "选择路径读取文件";
            this.m_message = "选择路径读取文件";

            m_Image16 = AG.COM.SDM.Plugins.Properties.Resources.yin16;
            m_Image32 = AG.COM.SDM.Plugins.Properties.Resources.yin32;
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
                FrmReadDir frmReadDir = new FrmReadDir();
                IDockDocumentService tDockDocumentService = m_hookHelper.DockDocumentService;
                if (!tDockDocumentService.ContainsDocument(frmReadDir.Name))
                {
                    m_hookHelper.DockDocumentService.AddDockDocument(frmReadDir.Name,
                        frmReadDir, DockState.Document);
                }
                //if (tDockDocumentService.ContainsDocument(frmReadDir.Name))
                //{
                //    tDockDocumentService.RemoveDockDocument(frmReadDir.Name);
                //}

            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
            }
        }

        public override void OnCreate(object hook)
        {
            m_hookHelper.Framework = hook as IFramework;
        }
    }
}