using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.DataTools.Manager
{
    public partial class FormCreateFeatureClass : Form
    {
        private IFeatureClassName m_FeatureClassName = null;

        public IFeatureClassName FeatureClassName
        {
            get
            {
                return m_FeatureClassName;
            }
        }

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FormCreateFeatureClass()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormCreateFeatureClass_Load(object sender, EventArgs e)
        {
            Bitmap bmp = new System.Drawing.Bitmap(btOpenWorkspace.Image);
            bmp.MakeTransparent();
            this.btOpenWorkspace.Image = bmp;
            this.btSelectSpatialRef.Image = bmp;

            //��ʼ���ռ�ο�
            ISpatialReference spf = new UnknownCoordinateSystemClass();
            txtSpatialRef.Tag = spf;
            txtSpatialRef.Text = spf.Name;

            colFieldType.Items.Add("�ַ���");
            colFieldType.Items.Add("˫����");
            colFieldType.Items.Add("����");
            colFieldType.Items.Add("������");
            colFieldType.Items.Add("����");
            colFieldType.Items.Add("������");

            lblRefScale.Visible = false;
            nudRefScale.Visible = false;

        }

        private esriFieldType GetFieldType(string fldTypeString)
        {
            if (fldTypeString == "�ַ���")
                return esriFieldType.esriFieldTypeString;
            if (fldTypeString == "˫����")
                return esriFieldType.esriFieldTypeDouble;
            if (fldTypeString == "����")
                return esriFieldType.esriFieldTypeInteger;
            if (fldTypeString == "������")
                return esriFieldType.esriFieldTypeSingle;
            if (fldTypeString == "����")
                return esriFieldType.esriFieldTypeDate;
            if (fldTypeString == "������")
                return esriFieldType.esriFieldTypeBlob;
            return  esriFieldType.esriFieldTypeString;
        }

        private string GetFieldTypeString(esriFieldType fldType)
        {
            if (fldType == esriFieldType.esriFieldTypeString)
                return "�ַ���";
            if (fldType == esriFieldType.esriFieldTypeDouble)
                return "˫����";
            if (fldType == esriFieldType.esriFieldTypeInteger)
                return "����";
            if (fldType == esriFieldType.esriFieldTypeSingle)
                return "������";
            if (fldType == esriFieldType.esriFieldTypeDate)
                return "����";
            if (fldType == esriFieldType.esriFieldTypeBlob)
                return "������";
            return "";
        }

        private void btOpenWorkspace_Click(object sender, EventArgs e)
        {
            AG.COM.SDM.Catalog.IDataBrowser frm = new AG.COM.SDM.Catalog.FormDataBrowser();
            frm.AddFilter(new AG.COM.SDM.Catalog.Filters.WorkspaceFilter());
            frm.AddFilter(new AG.COM.SDM.Catalog.Filters.FeatureDatasetFilter());
            frm.Text = "ѡ�����ռ�";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                IList<AG.COM.SDM.Catalog.DataItems.DataItem> items = frm.SelectedItems;
                if (items.Count == 0)
                    return;
                object obj = items[0].GetGeoObject();
                if (obj != null)
                {
                    if (obj is IWorkspace)
                        txtWorkspace.Text = (obj as IWorkspace).PathName;
                    else if (obj is IFeatureDataset)
                        txtWorkspace.Text = (obj as IFeatureDataset).Name;
                    else
                        return;
                    txtWorkspace.Tag = obj;
                    
                }
            }
        }

        private void btImportFields_Click(object sender, EventArgs e)
        {
            AG.COM.SDM.Catalog.IDataBrowser frm = new AG.COM.SDM.Catalog.FormDataBrowser();
            frm.AddFilter(new AG.COM.SDM.Catalog.Filters.FeatureClassFilter());
            frm.Text = "ѡ��ͼ��";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                IList<AG.COM.SDM.Catalog.DataItems.DataItem> items = frm.SelectedItems;
                if (items.Count == 0)
                    return;
                IFeatureClass fcls = items[0].GetGeoObject() as IFeatureClass;
                if (fcls != null)
                {
                    this.dgFields.Rows.Clear();
                    IField fld;
                    DataGridViewRow row;
                    for (int i = 0; i <= fcls.Fields.FieldCount - 1; i++)
                    {
                        fld = fcls.Fields.get_Field(i);
                        if ((fld.Type != esriFieldType.esriFieldTypeBlob) &&
                            (fld.Type != esriFieldType.esriFieldTypeGeometry) &&
                            (fld.Type != esriFieldType.esriFieldTypeGlobalID) &&
                            (fld.Type != esriFieldType.esriFieldTypeGUID) &&
                            (fld.Type != esriFieldType.esriFieldTypeOID) &&
                            (fld.Type != esriFieldType.esriFieldTypeRaster) &&
                            (fld.Type != esriFieldType.esriFieldTypeXML))
                        {
                            row = new DataGridViewRow();
                            row.CreateCells(dgFields);
                            row.Cells[0].Value = fld.Name;
                            row.Cells[1].Value = fld.AliasName;
                            row.Cells[2].Value = GetFieldTypeString(fld.Type);
                            row.Cells[3].Value = fld.Length;

                            dgFields.Rows.Add(row);
                        }
                    }
                }
            }
        }

        private void btSelectSpatialRef_Click(object sender, EventArgs e)
        {
            AG.COM.SDM.GeoDataBase.SpatialReferenceDialog dlg = new AG.COM.SDM.GeoDataBase.SpatialReferenceDialog();
            dlg.SpatialReference = this.txtSpatialRef.Tag as ISpatialReference;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtSpatialRef.Tag = dlg.SpatialReference;
                txtSpatialRef.Text = dlg.SpatialReference.Name;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            object  obj = txtWorkspace.Tag;
            if (obj == null)
            {
                MessageHandler.ShowErrorMsg("��ѡ��Ҫ����ͼ���λ�ã�", Text);
                return;
            }
            string lyname = txtName.Text.Trim();
            if (lyname.Length == 0)
            {
                MessageHandler.ShowErrorMsg("������ͼ�����ƣ�", Text);
                return;
            }

            try
            {
                //�����ʼ�ֶ�
                IFields flds = new FieldsClass();
                IFeatureClassDescription fclsDes;// = new FeatureClassDescriptionClass();
                if (optAnno.Checked)
                    fclsDes = new AnnotationFeatureClassDescriptionClass();
                else
                    fclsDes = new FeatureClassDescriptionClass();
                flds = (fclsDes as IObjectClassDescription).RequiredFields;

                IField fld;
                fld = flds.get_Field(flds.FindField(fclsDes.ShapeFieldName));
                if (optPoint.Checked)
                    (fld.GeometryDef as IGeometryDefEdit).GeometryType_2 = esriGeometryType.esriGeometryPoint;  
                else if (optLine.Checked)
                    (fld.GeometryDef as IGeometryDefEdit).GeometryType_2 = esriGeometryType.esriGeometryPolyline;  
                else
                    (fld.GeometryDef as IGeometryDefEdit).GeometryType_2 = esriGeometryType.esriGeometryPolygon;  

                //����б��е��ֶ�
                DataGridViewRow row;                 
                for (int i = 0; i <= dgFields.Rows.Count - 1; i++)
                {
                    row = dgFields.Rows[i];
                    if (row.Cells[0].Value == null)
                        break;
                    if (row.Cells[0].Value.ToString().Trim() == "")
                        break;
                    fld = new FieldClass();
                    (fld as IFieldEdit).Name_2 = row.Cells[0].Value.ToString().Trim();
                    if (row.Cells[1] != null)
                    {
                        (fld as IFieldEdit).AliasName_2 = row.Cells[1].Value.ToString();
                    }
                    if (row.Cells[2].Value != null)
                    {
                        (fld as IFieldEdit).Type_2 = GetFieldType(row.Cells[2].Value.ToString());
                    }
                    
                    if (row.Cells[3].Value != null)
                    {
                        int len;
                        if (int.TryParse(row.Cells[3].Value.ToString(),out len))
                        {
                            if (len > 0)
                            {
                                (fld as IFieldEdit).Length_2 = len;//(int)row.Cells[3].Value;
                            }
                        }
                    }
                    if (flds.FindField(fld.Name)<0)
                        (flds as IFieldsEdit).AddField(fld);
                }
                //����
                IFeatureClass fcls;
                if (optAnno.Checked == false)
                {
                    if (obj is IFeatureWorkspace)
                    {
                        fcls = (obj as IFeatureWorkspace).CreateFeatureClass(lyname, flds, null, null, esriFeatureType.esriFTSimple, fclsDes.ShapeFieldName, "");
                    }
                    else
                    {
                        if (obj is IGeoDataset)
                        {
                            fld = flds.get_Field(flds.FindField(fclsDes.ShapeFieldName));
                            (fld.GeometryDef as IGeometryDefEdit).SpatialReference_2 = (obj as IGeoDataset).SpatialReference;
                        }
                        fcls = (obj as IFeatureDataset).CreateFeatureClass(lyname, flds, null, null, esriFeatureType.esriFTSimple, fclsDes.ShapeFieldName, "");
                    }
                }
                else
                {
                    IFeatureWorkspaceAnno pWsAnno = null;
                    IFeatureDataset pFds= null;
                    if (obj is IFeatureWorkspace)
                        pWsAnno = obj as IFeatureWorkspaceAnno;
                    else
                    {
                        pWsAnno = (obj as IFeatureDataset).Workspace as IFeatureWorkspaceAnno;
                        pFds = obj as IFeatureDataset;

                        if (obj is IGeoDataset)
                        {
                            fld = flds.get_Field(flds.FindField(fclsDes.ShapeFieldName));
                            (fld.GeometryDef as IGeometryDefEdit).SpatialReference_2 = (obj as IGeoDataset).SpatialReference;
                        }
                    }
                    if (pWsAnno == null)
                    {
                        MessageHandler.ShowErrorMsg("��λ�ò��ܴ���ע��ͼ�㣡", Text);
                        return;
                    }
                    fcls = CreateAnnoFeatureClass(pWsAnno,pFds, flds, (double)nudRefScale.Value,lyname);
                }

                m_FeatureClassName = (fcls as IDataset).FullName as IFeatureClassName;
                MessageHandler.ShowInfoMsg("����ͼ��ɹ���", Text);
                this.DialogResult = DialogResult.OK;
                this.Close(); 
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("Must be the owner to perform this operation"))
                {
                    MessageHandler.ShowErrorMsg("û��Ȩ�޴���ͼ�㣡",Text);
                }
                else
                {
                    MessageHandler.ShowErrorMsg("����ͼ��ʧ�ܣ�" + ex.Message, Text);
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="featureWorkspaceAnno"></param>
        /// <param name="ds"></param>
        /// <param name="flds"></param>
        /// <param name="refScale"></param>
        /// <param name="lyname"></param>
        /// <returns></returns>
        public IFeatureClass CreateAnnoFeatureClass(IFeatureWorkspaceAnno featureWorkspaceAnno,IFeatureDataset ds, IFields flds, double refScale,string lyname)
        {
            
            //set up the reference scale
            ESRI.ArcGIS.Carto.IGraphicsLayerScale graphicLayerScale =new ESRI.ArcGIS.Carto.GraphicsLayerScaleClass();
            //IGeoDataset geoDataset = (IGeoDataset)dataset;
            graphicLayerScale.Units = ESRI.ArcGIS.esriSystem.esriUnits.esriMeters;
            graphicLayerScale.ReferenceScale = refScale;

            //set up symbol collection
            ESRI.ArcGIS.Display.ISymbolCollection symbolCollection =  new ESRI.ArcGIS.Display.SymbolCollectionClass();

            #region "MakeText"
            ESRI.ArcGIS.Display.IFormattedTextSymbol myTextSymbol = new ESRI.ArcGIS.Display.TextSymbolClass();
            
            //set the font for myTextSymbol
            stdole.IFontDisp myFont = new stdole.StdFontClass() as stdole.IFontDisp;
            myFont.Name = "����";
            myFont.Size = 10;
            myTextSymbol.Font = myFont;
           
            //set the Color for myTextSymbol to be Dark Red
            ESRI.ArcGIS.Display.IRgbColor rgbColor = new ESRI.ArcGIS.Display.RgbColorClass();
            rgbColor.Red = 0;
            rgbColor.Green = 0;
            rgbColor.Blue = 0;
            myTextSymbol.Color = (ESRI.ArcGIS.Display.IColor)rgbColor;
            
            //Set other properties for myTextSymbol
            myTextSymbol.Angle = 0;
            myTextSymbol.RightToLeft = false;
            myTextSymbol.VerticalAlignment = ESRI.ArcGIS.Display.esriTextVerticalAlignment.esriTVABaseline;
            myTextSymbol.HorizontalAlignment = ESRI.ArcGIS.Display.esriTextHorizontalAlignment.esriTHAFull;
            myTextSymbol.CharacterSpacing = 200;
            myTextSymbol.Case = ESRI.ArcGIS.Display.esriTextCase.esriTCNormal;
            #endregion

            symbolCollection.set_Symbol(0, (ESRI.ArcGIS.Display.ISymbol)myTextSymbol);

            //set up the annotation labeling properties including the expression
            ESRI.ArcGIS.Carto.IAnnotateLayerProperties annoProps = new ESRI.ArcGIS.Carto.LabelEngineLayerPropertiesClass();
            annoProps.FeatureLinked = false;
            annoProps.AddUnplacedToGraphicsContainer = false;
            annoProps.CreateUnplacedElements = true;
            annoProps.DisplayAnnotation = true;
            annoProps.UseOutput = true;

            ESRI.ArcGIS.Carto.ILabelEngineLayerProperties layerEngineLayerProps =
                (ESRI.ArcGIS.Carto.ILabelEngineLayerProperties)annoProps;
            ESRI.ArcGIS.Carto.IAnnotationExpressionEngine annoExpressionEngine =
                new ESRI.ArcGIS.Carto.AnnotationVBScriptEngineClass();
            layerEngineLayerProps.ExpressionParser = annoExpressionEngine;
            layerEngineLayerProps.Expression = "[DESCRIPTION]";
            layerEngineLayerProps.IsExpressionSimple = true;
            layerEngineLayerProps.Offset = 0;
            layerEngineLayerProps.SymbolID = 0;
            layerEngineLayerProps.Symbol = myTextSymbol;

            ESRI.ArcGIS.Carto.IAnnotateLayerTransformationProperties annoLayerTransProp =
                (ESRI.ArcGIS.Carto.IAnnotateLayerTransformationProperties)annoProps;
            annoLayerTransProp.ReferenceScale = graphicLayerScale.ReferenceScale;
            annoLayerTransProp.Units = graphicLayerScale.Units;
            annoLayerTransProp.ScaleRatio = 1;

            ESRI.ArcGIS.Carto.IAnnotateLayerPropertiesCollection annoPropsColl =
                new ESRI.ArcGIS.Carto.AnnotateLayerPropertiesCollectionClass();
            annoPropsColl.Add(annoProps);
            //use the AnnotationFeatureClassDescription to get the list of required
            //fields and the default name of the shape field
            IObjectClassDescription oCDesc = new ESRI.ArcGIS.Carto.AnnotationFeatureClassDescriptionClass();
            IFeatureClassDescription fCDesc = (IFeatureClassDescription)oCDesc;

            IField fld = flds.get_Field(flds.FindField(fCDesc.ShapeFieldName));
            (fld.GeometryDef as IGeometryDefEdit).GeometryType_2 = esriGeometryType.esriGeometryPoint;

            //create the new class
            return featureWorkspaceAnno.CreateAnnotationClass(lyname,
                flds, oCDesc.InstanceCLSID, oCDesc.ClassExtensionCLSID,
                fCDesc.ShapeFieldName, "",ds, null,
                annoPropsColl, graphicLayerScale, symbolCollection, true);
        }
        
        private void dgFields_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[2].Value = colFieldType.Items[0];
        }

        private void optAnno_CheckedChanged(object sender, EventArgs e)
        {
            lblRefScale.Visible = optAnno.Checked;
            nudRefScale.Visible = optAnno.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ws">IWorkspace��IFeatureDataset</param>
        /// <param name="canSelect"></param>
        public void Init(object ws, bool canSelect)
        {

            this.txtWorkspace.Tag = ws;
            if (ws is IWorkspace)
                txtWorkspace.Text = (ws as IWorkspace).PathName;
            else if (ws is IFeatureDataset)
                txtWorkspace.Text = (ws as IDataset).Name;

            btOpenWorkspace.Visible = canSelect;
        }
    }
}