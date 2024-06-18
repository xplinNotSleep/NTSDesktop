/*******************************************************************************
 * Copyright © 2017 Augurit.com 版权所有
 * Author: 陈世峰
 * History: Created by Author 2017/5/15 10:02:44
 * Website：http://www.augurit.com
 * Description: 
*********************************************************************************/
using AG.COM.SDM.Config;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AG.COM.SDM.Manager
{
    public class AGDesignSurface : DesignSurface
    {
        private const string _Name_ = "AGDesignSurface";

        public void UseSnapLines()
        {
            IServiceContainer serviceProvider = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
            DesignerOptionService opsService = serviceProvider.GetService(typeof(DesignerOptionService)) as DesignerOptionService;
            if (null != opsService)
            {
                serviceProvider.RemoveService(typeof(DesignerOptionService));
            }
            DesignerOptionService opsService2 = new DesignerOptionServiceExt4SnapLines();
            serviceProvider.AddService(typeof(DesignerOptionService), opsService2);
        }
        public AGForm CreateRootComponent(AGLoginFormEntity m)
        {
            try
            {
                IDesignerHost host = GetIDesignerHost();
                if (null == host) return null;
                if (null != host.RootComponent) return null;
                this.BeginLoad(typeof(AGForm));
                IDesignerHost ihost = GetIDesignerHost();

                //var ctrl = this.View as Control;
                //ctrl.BackColor = Color.DarkGray;
                if (null != m)
                {
                    //- set the Size
                    PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(typeof(AGForm));
                    //- Sets a PropertyDescriptor to the specific property.
                    PropertyDescriptor pd = pdc.Find("Size", false);
                    if (null != pd)
                        pd.SetValue(ihost.RootComponent, m.Size);

                    pd = pdc.Find("Text", false);
                    if (null != pd)
                        pd.SetValue(ihost.RootComponent, m.Text);

                    if (null != m.BackgroundImage)
                    {
                        pd = pdc.Find("BackgroundImage", false);
                        if (null != pd)
                            pd.SetValue(ihost.RootComponent, m.BackgroundImage);
                    }
                }
                var f = ihost.RootComponent as AGForm;
                f.Name = m.Name;
                return f;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw new Exception(_Name_ + "::CreateRootComponent() - Exception: (see Inner Exception)", ex);
            }
            return null;
        }

        public AGControl CreateControl(AGLoginUCEntity m)
        {
            try
            {
                IDesignerHost host = GetIDesignerHost();
                if (null == host) return null;
                if (null == host.RootComponent) return null;

                IComponent newComp = host.CreateComponent(typeof(AGControl), m.Name);
                if (null == newComp) return null;
                IDesigner designer = host.GetDesigner(newComp);
                if (null == designer) return null;
                if (designer is IComponentInitializer)
                    ((IComponentInitializer)designer).InitializeNewComponent(null);
                if (null != m)
                {
                    //- try to modify the Size/Location of the object just created
                    PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(newComp);
                    //- Sets a PropertyDescriptor to the specific property.
                    PropertyDescriptor pd = pdc.Find("Size", false);
                    if (null != pd)
                        pd.SetValue(newComp, m.Size);

                    pd = pdc.Find("Location", false);
                    if (null != pd)
                        pd.SetValue(newComp, m.Location);

                    pd = pdc.Find("Text", false);
                    if (null != pd)
                        pd.SetValue(newComp, m.Text);

                    pd = pdc.Find("Visible", false);
                    if (null != pd)
                        pd.SetValue(newComp, m.Visible);
                }
                var ctrl = newComp as AGControl;
                ctrl.Parent = host.RootComponent as AGForm;
                return ctrl;
            }//end_try
            catch (Exception ex)
            {
                throw new Exception(_Name_ + "::CreateControl() - Exception: (see Inner Exception)", ex);
            }//end_catch
        }

        public IDesignerHost GetIDesignerHost()
        {
            return (IDesignerHost)(this.GetService(typeof(IDesignerHost)));
        }

        public Control GetView()
        {
            Control ctrl = this.View as Control;
            ctrl.Dock = DockStyle.Fill;
            return ctrl;
        }




    }
    internal class DesignerOptionServiceExt4SnapLines : DesignerOptionService
    {
        public DesignerOptionServiceExt4SnapLines() : base() { }

        protected override void PopulateOptionCollection(DesignerOptionCollection options)
        {
            if (null != options.Parent) return;

            DesignerOptions ops = new DesignerOptions();
            ops.UseSnapLines = true;
            ops.UseSmartTags = true;
            DesignerOptionCollection wfd = this.CreateOptionCollection(options, "WindowsFormsDesigner", null);
            this.CreateOptionCollection(wfd, "General", ops);
        }
    }

    internal class DesignerOptionServiceExt4Grid : DesignerOptionService
    {
        private System.Drawing.Size _gridSize;

        public DesignerOptionServiceExt4Grid(System.Drawing.Size gridSize) : base() { _gridSize = gridSize; }

        protected override void PopulateOptionCollection(DesignerOptionCollection options)
        {
            if (null != options.Parent) return;

            DesignerOptions ops = new DesignerOptions();
            ops.GridSize = _gridSize;
            ops.SnapToGrid = true;
            ops.ShowGrid = true;
            ops.UseSnapLines = false;
            ops.UseSmartTags = true;
            DesignerOptionCollection wfd = this.CreateOptionCollection(options, "WindowsFormsDesigner", null);
            this.CreateOptionCollection(wfd, "General", ops);
        }
    }
}
