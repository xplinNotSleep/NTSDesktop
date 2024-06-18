using AG.COM.SDM.DAL;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Framework.DocumentView;
using AG.COM.SDM.Model;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace AG.COM.SDM.Plugins.Startup
{
    /// <summary>
    /// ϵͳ��ʼ��������
    /// </summary>
    public sealed class InitSystem : BaseStartupPlugin
    {
        private IHookHelper m_HookHelper = new HookHelper();
        private IFramework m_Framework;

        /// <summary>
        /// ����
        /// </summary>
        public override void Startup()
        {
            this.m_HookHelper.Framework = m_Framework;
            //������ͼ�ĵ������Ĳ˵�
            ITocDocumentView tTocDocument = this.m_HookHelper.DockDocumentService.GetDockDocument(EnumDocumentType.TocDocument.ToString()) as ITocDocumentView;
            tTocDocument.DefaultContextMenu = new AG.COM.SDM.Plugins.TocDefaultContextMenu();

            //������ͼ�ĵ������Ĳ˵�
            IMainDocumentView tMainDocument = this.m_HookHelper.DockDocumentService.GetDockDocument(EnumDocumentType.MainDocument.ToString()) as IMainDocumentView;
            tMainDocument.DefaultContextMenu = new AG.COM.SDM.Plugins.MainDefaultContextMenu();

        }

        private void MapControl_OnBeforeScreenDraw(int hdc)
        {           
            foreach (object obj in m_Framework.StatusBar.ItemLinks)
            {
                if (obj is BarEditItemLink)
                {
                    BarEditItemLink barStaticItemLink = obj as BarEditItemLink;
                    BarEditItem barStaticItem = barStaticItemLink.Item;
                    if (barStaticItem.Name == "lblProcess")
                    {
                        barStaticItem.Visibility = BarItemVisibility.Always;
                    }
                }
            }
        }

        private void MapControl_OnAfterScreenDraw(int hdc)
        {
            foreach (object obj in m_Framework.StatusBar.ItemLinks)
            {
                if (obj is BarEditItemLink)
                {
                    BarEditItemLink barStaticItemLink = obj as BarEditItemLink;
                    BarEditItem barStaticItem = barStaticItemLink.Item;
                    if (barStaticItem.Name == "lblProcess")
                    {
                        barStaticItem.Visibility = BarItemVisibility.Never;
                    }
                }
            }
        }

        private void MapControl_OnMouseMove(int button, int shift, int X, int Y, double mapX, double mapY)
        {
            double mx = mapX;
            double my = mapY;

            foreach (object obj in m_Framework.StatusBar.ItemLinks)
            {
                if (obj is BarStaticItemLink)
                {
                    BarStaticItemLink barStaticItemLink = obj as BarStaticItemLink;
                    BarStaticItem barStaticItem = barStaticItemLink.Item;
                    if (barStaticItem.Name == "lblMousePos")
                    {
                        barStaticItem.Caption = mx.ToString("0.##") + "," + my.ToString("0.##");
                    }
                }
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
            //this.m_HookHelper.Hook = hook;
            this.m_Framework = hook as IFramework;
        }


    }
}
