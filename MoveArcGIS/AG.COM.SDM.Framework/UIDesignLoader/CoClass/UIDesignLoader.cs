
//QIOS版框架控件加载，已不用

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.Design.Serialization;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Text;
//using System.Windows.Forms;
//using System.Xml;
//using AG.COM.UIDesign.Utility;

//namespace AG.COM.SDM.Framework.UIDesignLoader
//{
//    /// <summary>
//    /// UI设计加载成控件
//    /// </summary>
//    public class UIDesignLoader
//    {
//        /// <summary>
//        /// 加载控件到Form
//        /// </summary>
//        /// <param name="tUIDesignXml">界面设置Xml对象</param>
//        /// <param name="BindFuns">绑定功能设置</param>
//        /// <param name="tFormMain">加载控件的Form</param>
//        /// <param name="tRootContainer">控件与绑定功能设置的关联容器</param>
//        public void AddControlToForm(XmlDocument tUIDesignXml, List<ItemCommandInfo> BindFuns, Form tFormMain,
//            ref Dictionary<UIDesignControl, List<UIDesignControl>> tRootContainer)
//        {
//            tRootContainer = new Dictionary<UIDesignControl, List<UIDesignControl>>();

//            //从最高级的第一个Xml节点加载，因为这个节点是指Form
//            if (tUIDesignXml.DocumentElement.ChildNodes.Count > 0)
//            {
//                ArrayList errors = new ArrayList();

//                XmlNode tNodeRoot = tUIDesignXml.DocumentElement.ChildNodes[0];

//                XmlAttribute childAttr = tNodeRoot.Attributes["children"];

//                foreach (XmlNode tNodeControl in tNodeRoot.ChildNodes)
//                {
//                    if (tNodeControl.Name.Equals("Object"))
//                    {
//                        object tControlInstance = ReadObject(tNodeControl, errors, null, tRootContainer, BindFuns);

//                        tFormMain.Controls.Add(tControlInstance as Control);                     
//                    }
//                }            
//            }
//        }

//        #region 实例化代码

//        /// <summary>
//        /// 生成Object，这里的Object指可视化控件
//        /// </summary>
//        /// <param name="node"></param>
//        /// <param name="errors"></param>
//        /// <returns></returns>
//        private object ReadObject(XmlNode node, ArrayList errors, List<UIDesignControl> parentContainer,
//            Dictionary<UIDesignControl, List<UIDesignControl>> rootContainer, List<ItemCommandInfo> BindFuns)
//        {
//            XmlAttribute typeAttr = node.Attributes["type"];

//            if (typeAttr == null)
//            {
//                errors.Add("<Object> tag is missing required type attribute");
//                return null;
//            }

//            Type type = Type.GetType(typeAttr.Value);

//            if (type == null)
//            {
//                errors.Add(string.Format("Type {0} could not be loaded.", typeAttr.Value));
//                return null;
//            }

//            // This can be null if there is no name for the object.
//            //
//            XmlAttribute nameAttr = node.Attributes["name"];

//            object instance= Activator.CreateInstance(type);

//            if (parentContainer != null && nameAttr != null && !string.IsNullOrEmpty(nameAttr.Value))
//            {
//                string controlName = nameAttr.Value;
//                ItemCommandInfo tItemCommandInfo = BindFuns.FirstOrDefault(t => t.ControlName == controlName);
//                if (tItemCommandInfo != null)
//                {
//                    UIDesignControl tUIDesignControl = new UIDesignControl();
//                    tUIDesignControl.Name = nameAttr.Value;
//                    tUIDesignControl.BindFun = tItemCommandInfo;
//                    tUIDesignControl.Control = instance;
//                    parentContainer.Add(tUIDesignControl);
//                }
//            }

//            // Got an object, now we must process it.  Check to see if this tag
//            // offers a child collection for us to add children to.
//            //
//            XmlAttribute childAttr = node.Attributes["children"];
//            IList childList = null;

//            if (childAttr != null)
//            {
//                PropertyDescriptor childProp = TypeDescriptor.GetProperties(instance)[childAttr.Value];

//                if (childProp == null)
//                {
//                    errors.Add(string.Format("The children attribute lists {0} as the child collection but this is not a property on {1}", childAttr.Value, instance.GetType().FullName));
//                }
//                else
//                {
//                    childList = childProp.GetValue(instance) as IList;
//                    if (childList == null)
//                    {
//                        errors.Add(string.Format("The property {0} was found but did not return a valid IList", childProp.Name));
//                    }
//                }
//            }

//            //只支持如下类型控件
//            if (type == typeof(QRibbonPage) || type == typeof(QMainMenu) || type == typeof(QToolBar))
//            {
//                parentContainer = new List<UIDesignControl>();
//                UIDesignControl tUIDesignControl = new UIDesignControl();
//                tUIDesignControl.Name = nameAttr.Value;
//                tUIDesignControl.Control = instance;
//                rootContainer.Add(tUIDesignControl, parentContainer);
//            }

//            // Now, walk the rest of the tags under this element.
//            //
//            foreach (XmlNode childNode in node.ChildNodes)
//            {
//                if (childNode.Name.Equals("Object"))
//                {
//                    // Another object.  In this case, create the object, and
//                    // parent it to ours using the children property.  If there
//                    // is no children property, bail out now.
//                    if (childAttr == null)
//                    {
//                        errors.Add("Child object found but there is no children attribute");
//                        continue;
//                    }

//                    // no sense doing this if there was an error getting the property.  We've already reported the
//                    // error above.
//                    if (childList != null)
//                    {
//                        object childInstance = ReadObject(childNode, errors, parentContainer, rootContainer, BindFuns);

//                        childList.Add(childInstance);
//                    }
//                }
//                else if (childNode.Name.Equals("Property"))
//                {
//                    // A property.  Ask the property to parse itself.
//                    //
//                    ReadProperty(childNode, instance, errors);
//                }           
//            }

//            return instance;
//        }

//        /// <summary>
//        /// 生成属性
//        /// </summary>
//        /// <param name="node"></param>
//        /// <param name="instance"></param>
//        /// <param name="errors"></param>
//        private void ReadProperty(XmlNode node, object instance, ArrayList errors)
//        {
//            XmlAttribute nameAttr = node.Attributes["name"];

//            if (nameAttr == null)
//            {
//                errors.Add("Property has no name");
//                return;
//            }

//            PropertyDescriptor prop = TypeDescriptor.GetProperties(instance)[nameAttr.Value];

//            if (prop == null)
//            {
//                errors.Add(string.Format("Property {0} does not exist on {1}", nameAttr.Value, instance.GetType().FullName));
//                return;
//            }

//            // Get the type of this property.  We have three options:
//            // 1.  A normal read/write property.
//            // 2.  A "Content" property.
//            // 3.  A collection.
//            //
//            bool isContent = prop.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);

//            if (isContent)
//            {
//                object value = prop.GetValue(instance);

//                // Handle the case of a content property that is a collection.
//                //
//                if (value is IList)
//                {
//                    foreach (XmlNode child in node.ChildNodes)
//                    {
//                        if (child.Name.Equals("Item"))
//                        {
//                            object item;
//                            XmlAttribute typeAttr = child.Attributes["type"];

//                            if (typeAttr == null)
//                            {
//                                errors.Add("Item has no type attribute");
//                                continue;
//                            }

//                            Type type = Type.GetType(typeAttr.Value);

//                            if (type == null)
//                            {
//                                errors.Add(string.Format("Item type {0} could not be found.", typeAttr.Value));
//                                continue;
//                            }

//                            if (ReadValue(child, TypeDescriptor.GetConverter(type), errors, out item))
//                            {
//                                try
//                                {
//                                    ((IList)value).Add(item);
//                                }
//                                catch (Exception ex)
//                                {
//                                    errors.Add(ex.Message);
//                                }
//                            }
//                        }
//                        else
//                        {
//                            errors.Add(string.Format("Only Item elements are allowed in collections, not {0} elements.", child.Name));
//                        }
//                    }
//                }
//                else
//                {
//                    // Handle the case of a content property that consists of child properties.
//                    //
//                    foreach (XmlNode child in node.ChildNodes)
//                    {
//                        if (child.Name.Equals("Property"))
//                        {
//                            ReadProperty(child, value, errors);
//                        }
//                        else
//                        {
//                            errors.Add(string.Format("Only Property elements are allowed in content properties, not {0} elements.", child.Name));
//                        }
//                    }
//                }
//            }
//            else
//            {
//                object value;

//                if (ReadValue(node, prop.Converter, errors, out value))
//                {
//                    // ReadValue succeeded.  Fill in the property value.
//                    //
//                    try
//                    {
//                        prop.SetValue(instance, value);
//                    }
//                    catch (Exception ex)
//                    {
//                        errors.Add(ex.Message);
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// 生成值
//        /// </summary>
//        /// <param name="node"></param>
//        /// <param name="converter"></param>
//        /// <param name="errors"></param>
//        /// <param name="value"></param>
//        /// <returns></returns>
//        private bool ReadValue(XmlNode node, TypeConverter converter, ArrayList errors, out object value)
//        {
//            try
//            {
//                foreach (XmlNode child in node.ChildNodes)
//                {
//                    if (child.NodeType == XmlNodeType.Text)
//                    {
//                        value = converter.ConvertFromInvariantString(node.InnerText);
//                        return true;
//                    }
//                    else if (child.Name.Equals("Binary"))
//                    {
//                        byte[] data = Convert.FromBase64String(child.InnerText);

//                        // Binary blob.  Now, check to see if the type converter
//                        // can convert it.  If not, use serialization.
//                        //
//                        if (GetConversionSupported(converter, typeof(byte[])))
//                        {
//                            value = converter.ConvertFrom(null, CultureInfo.InvariantCulture, data);
//                            return true;
//                        }
//                        else
//                        {
//                            BinaryFormatter formatter = new BinaryFormatter();
//                            MemoryStream stream = new MemoryStream(data);

//                            value = formatter.Deserialize(stream);
//                            return true;
//                        }
//                    }
//                    else if (child.Name.Equals("InstanceDescriptor"))
//                    {
//                        value = ReadInstanceDescriptor(child, errors);
//                        return (value != null);
//                    }
//                    else
//                    {
//                        errors.Add(string.Format("Unexpected element type {0}", child.Name));
//                        value = null;
//                        return false;
//                    }
//                }

//                // If we get here, it is because there were no nodes.  No nodes and no inner
//                // text is how we signify null.
//                //
//                value = null;
//                return true;
//            }
//            catch (Exception ex)
//            {
//                errors.Add(ex.Message);
//                value = null;
//                return false;
//            }
//        }

//        /// <summary>
//        /// Simple helper method that returns true if the given type converter supports
//        /// two-way conversion of the given type.
//        /// </summary>
//        private bool GetConversionSupported(TypeConverter converter, Type conversionType)
//        {
//            return (converter.CanConvertFrom(conversionType) && converter.CanConvertTo(conversionType));
//        }

//        private object ReadInstanceDescriptor(XmlNode node, ArrayList errors)
//        {
//            // First, need to deserialize the member
//            //
//            XmlAttribute memberAttr = node.Attributes["member"];

//            if (memberAttr == null)
//            {
//                errors.Add("No member attribute on instance descriptor");
//                return null;
//            }

//            byte[] data = Convert.FromBase64String(memberAttr.Value);
//            BinaryFormatter formatter = new BinaryFormatter();
//            MemoryStream stream = new MemoryStream(data);
//            MemberInfo mi = (MemberInfo)formatter.Deserialize(stream);
//            object[] args = null;

//            // Check to see if this member needs arguments.  If so, gather
//            // them from the XML.
//            if (mi is MethodBase)
//            {
//                ParameterInfo[] paramInfos = ((MethodBase)mi).GetParameters();

//                args = new object[paramInfos.Length];

//                int idx = 0;

//                foreach (XmlNode child in node.ChildNodes)
//                {
//                    if (child.Name.Equals("Argument"))
//                    {
//                        object value;

//                        if (!ReadValue(child, TypeDescriptor.GetConverter(paramInfos[idx].ParameterType), errors, out value))
//                        {
//                            return null;
//                        }

//                        args[idx++] = value;
//                    }
//                }

//                if (idx != paramInfos.Length)
//                {
//                    errors.Add(string.Format("Member {0} requires {1} arguments, not {2}.", mi.Name, args.Length, idx));
//                    return null;
//                }
//            }

//            InstanceDescriptor id = new InstanceDescriptor(mi, args);
//            object instance = id.Invoke();

//            // Ok, we have our object.  Now, check to see if there are any properties, and if there are, 
//            // set them.
//            //
//            foreach (XmlNode prop in node.ChildNodes)
//            {
//                if (prop.Name.Equals("Property"))
//                {
//                    ReadProperty(prop, instance, errors);
//                }
//            }

//            return instance;
//        }

//        #endregion
//    }
//}
