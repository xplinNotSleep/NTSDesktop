using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.COM.SDM.Manager
{
    public class AGCustomProperty : ICustomTypeDescriptor
    {
        private object mCurrentSelectObject;
        private List<string> mObjectAttribs = new List<string>();

        public AGCustomProperty(object pSelectObject, int type)
        {
            mCurrentSelectObject = pSelectObject;
            if (1 == type) mObjectAttribs = new List<string>() { "NewName", "NewText", "BackgroundImage" };
            else mObjectAttribs = new List<string>() { "Text", "NewName", "Size", "Location", "Visible" };
        }

        #region ICustomTypeDescriptor Members
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(mCurrentSelectObject);
        }
        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(mCurrentSelectObject);
        }
        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(mCurrentSelectObject);
        }
        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(mCurrentSelectObject);
        }
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(mCurrentSelectObject);
        }
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(mCurrentSelectObject);
        }
        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(mCurrentSelectObject, editorBaseType);
        }
        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(mCurrentSelectObject, attributes);
        }
        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(mCurrentSelectObject);
        }
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            List<AGCustomPropertyDescriptor> tmpPDCLst = new List<AGCustomPropertyDescriptor>();
            PropertyDescriptorCollection tmpPDC = TypeDescriptor.GetProperties(mCurrentSelectObject, attributes);
            IEnumerator tmpIe = tmpPDC.GetEnumerator();
            AGCustomPropertyDescriptor tmpCPD;
            PropertyDescriptor tmpPD;
            while (tmpIe.MoveNext())
            {
                tmpPD = tmpIe.Current as PropertyDescriptor;
                if (mObjectAttribs.Contains(tmpPD.Name))
                {
                    tmpCPD = new AGCustomPropertyDescriptor(mCurrentSelectObject, tmpPD);
                    tmpCPD.SetDisplayName(tmpPD.DisplayName);
                    tmpCPD.SetCategory(tmpPD.Category);
                    tmpPDCLst.Add(tmpCPD);
                }
            }
            return new PropertyDescriptorCollection(tmpPDCLst.ToArray());
        }
        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(mCurrentSelectObject);
        }
        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return mCurrentSelectObject;
        }
        #endregion
    }
}
