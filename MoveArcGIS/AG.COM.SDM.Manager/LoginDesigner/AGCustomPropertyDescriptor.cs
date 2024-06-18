using System;
using System.ComponentModel;

namespace AG.COM.SDM.Manager
{
    internal class AGCustomPropertyDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor mProp;
        private object mComponent;

        public AGCustomPropertyDescriptor(object pComponent, PropertyDescriptor pPD)
          : base(pPD)
        {
            mCategory = base.Category;
            mDisplayName = base.DisplayName;
            mProp = pPD;
            mComponent = pComponent;
        }
        private string mCategory;
        public override string Category
        {
            get { return mCategory; }
        }
        private string mDisplayName;
        public override string DisplayName
        {
            get { return mDisplayName; }
        }
        public void SetDisplayName(string pDispalyName)
        {
            mDisplayName = pDispalyName;
        }
        public void SetCategory(string pCategory)
        {
            mCategory = pCategory;
        }
        public override bool CanResetValue(object component)
        {
            return mProp.CanResetValue(component);
        }

        public override Type ComponentType
        {
            get { return mProp.ComponentType; }
        }

        public override object GetValue(object component)
        {
            return mProp.GetValue(component);
        }

        public override bool IsReadOnly
        {
            get { return mProp.IsReadOnly; }
        }

        public override Type PropertyType
        {
            get { return mProp.PropertyType; }
        }

      
        public override void ResetValue(object component) { mProp.ResetValue(component); }
        public override void SetValue(object component, object value) { mProp.SetValue(component, value); }
        public override bool ShouldSerializeValue(object component)
        {
            return mProp.ShouldSerializeValue(component);
        }
    }
}