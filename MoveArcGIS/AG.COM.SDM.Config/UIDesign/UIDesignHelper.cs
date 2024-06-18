using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// UIDesign帮助类
    /// </summary>
    public class UIDesignHelper
    {
        /// <summary>
        /// 从xml文件读取UIDesign文本
        /// </summary>
        /// <returns></returns>
        public static void LoadFromXml(ref string UIDesignXmlString, ref string BindFunXmlString)
        {
            if (File.Exists(CommonConstString.STR_UIDesignXml))
            {
                using (StreamReader tStreamReader = new StreamReader(CommonConstString.STR_UIDesignXml))
                {
                    UIDesignXmlString = tStreamReader.ReadToEnd();
                    tStreamReader.Close();
                }
            }
            else
            {
                throw new Exception("界面设计文件（" + CommonConstString.STR_UIDesignXml + "）不存在");
            }

            if (File.Exists(CommonConstString.STR_BindFunXml))
            {
                using (StreamReader tStreamReader = new StreamReader(CommonConstString.STR_BindFunXml))
                {
                    BindFunXmlString = tStreamReader.ReadToEnd();
                    tStreamReader.Close();
                }
            }
            else
            {
                throw new Exception("功能绑定文件（" + CommonConstString.STR_BindFunXml + "）不存在");
            }
        }

        /// <summary>
        /// 从xml文件读取UIDesign对象
        /// </summary>
        /// <param name="UIDesignXml"></param>
        /// <param name="BindFuns"></param>
        public static void LoadFromXml(out XmlDocument UIDesignXml, out List<ItemCommandInfo> BindFuns)
        {
            string UIDesignXmlString = "", BindFunXmlString = "";
            //加载Xml内容字符串
            LoadFromXml(ref  UIDesignXmlString, ref  BindFunXmlString);

            UIDesignXmlStringToObject(UIDesignXmlString, BindFunXmlString,
              out  UIDesignXml, out  BindFuns);
        }

        /// <summary>
        /// 从Database读取UIDesign对象
        /// </summary>
        /// <param name="UIDesignXml"></param>
        /// <param name="BindFuns"></param>
        /// <param name="FilterRoleRight"></param>
        public static void LoadFromDatabase(out XmlDocument UIDesignXml, out List<ItemCommandInfo> BindFuns, bool FilterRoleRight)
        {
            string UIDesignXmlString = "", BindFunXmlString = "";
            //加载Xml内容字符串
            LoadFromDatabase(ref  UIDesignXmlString, ref  BindFunXmlString);

            UIDesignXmlStringToObject(UIDesignXmlString, BindFunXmlString,
              out  UIDesignXml, out  BindFuns);

            if (FilterRoleRight == true)
            {
                FilterCurrentRoleFun(ref UIDesignXml);
            }
        }

        /// <summary>
        /// 从Database读取UIDesign文本
        /// </summary>
        /// <param name="UIDesignXmlString"></param>
        /// <param name="BindFunXmlString"></param>
        public static void LoadFromDatabase(ref string UIDesignXmlString, ref string BindFunXmlString)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);

            AGSDM_UI_DESIGN tUI_DESIGN = tEntityHandler.GetEntity<AGSDM_UI_DESIGN>("from AGSDM_UI_DESIGN t");
            if (tUI_DESIGN != null)
            {
                UIDesignXmlString = tUI_DESIGN.UIDESIGN;
                BindFunXmlString = tUI_DESIGN.BINDFUN;
            }
        }

        /// <summary>
        /// 过滤当前用户角色的菜单功能
        /// </summary>
        /// <param name="UIDesignXml"></param>
        public static void FilterCurrentRoleFun(ref XmlDocument UIDesignXml)
        {
            //管理员显示所有菜单
            if (SystemInfo.IsAdminUser) return;

            List<AGSDM_UI_ROLEFUNRLT> tRoleFunRlts = new List<AGSDM_UI_ROLEFUNRLT>();

            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            //通过当前用户ID找到其角色ID
            AGSDM_USER_ROLE tUSER_ROLE = tEntityHandler.GetEntity<AGSDM_USER_ROLE>
                ("from AGSDM_USER_ROLE where USER_ID ='" + SystemInfo.UserID + "'");
            if (tUSER_ROLE != null && !string.IsNullOrEmpty(tUSER_ROLE.ROLE_ID))
            {
                tRoleFunRlts = tEntityHandler.GetEntities<AGSDM_UI_ROLEFUNRLT>
                ("from AGSDM_UI_ROLEFUNRLT where ROLEID ='" + tUSER_ROLE.ROLE_ID + "'") as List<AGSDM_UI_ROLEFUNRLT>;
            }

            FilterFunNode(UIDesignXml.DocumentElement.ChildNodes, tRoleFunRlts);
        }

        /// <summary>
        /// 过滤角色的菜单功能
        /// </summary>
        /// <param name="UIDesignXml"></param>
        /// <param name="tRoleFunRlts"></param>
        public static void FilterRoleFun(ref XmlDocument UIDesignXml, List<AGSDM_UI_ROLEFUNRLT> tRoleFunRlts)
        {
            FilterFunNode(UIDesignXml.DocumentElement.ChildNodes, tRoleFunRlts);
        }

        /// <summary>
        /// 在Xml节点中过滤
        /// </summary>
        /// <param name="tXmlNodes"></param>
        /// <param name="tRoleFunRlts"></param>
        private static void FilterFunNode(XmlNodeList tXmlNodes, List<AGSDM_UI_ROLEFUNRLT> tRoleFunRlts)
        {
            for (int i = 0; i < tXmlNodes.Count; i++)
            {
                XmlNode tXmlNode = tXmlNodes[i];

                if (tXmlNode.Name == "Object")
                {
                    XmlAttribute tXmlAttrName = tXmlNode.Attributes["name"];
                    if (tXmlAttrName != null)
                    {
                        if (tRoleFunRlts.Any(t => t.CONTROLNAME == tXmlAttrName.Value) != true)
                        {
                            tXmlNode.ParentNode.RemoveChild(tXmlNode);
                            //删除节点后后面的节点序号全部向前一位
                            i--;
                        }
                        else
                        {
                            FilterFunNode(tXmlNode.ChildNodes, tRoleFunRlts);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// UIDesign界面设计与功能绑定由字符串转为对象
        /// </summary>
        /// <param name="UIDesignXmlString"></param>
        /// <param name="BindFunXmlString"></param>
        /// <param name="UIDesignXml"></param>
        /// <param name="BindFuns"></param>
        public static void UIDesignXmlStringToObject(string UIDesignXmlString, string BindFunXmlString,
            out XmlDocument UIDesignXml, out List<ItemCommandInfo> BindFuns)
        {
            UIDesignXmlString = "<DOCUMENT_ELEMENT>" + UIDesignXmlString + "</DOCUMENT_ELEMENT>";

            UIDesignXml = new XmlDocument();
            //完成UIDesign的XmlDocument
            UIDesignXml.LoadXml(UIDesignXmlString);

            BindFuns = new List<ItemCommandInfo>();
            if (!string.IsNullOrEmpty(BindFunXmlString))
            {
                using (StringReader tStringReader = new StringReader(BindFunXmlString))
                {
                    //通过反序列化生成绑定功能的集合
                    XmlSerializer tXmlSerializer = new XmlSerializer(typeof(List<ItemCommandInfo>));
                    BindFuns = tXmlSerializer.Deserialize(tStringReader) as List<ItemCommandInfo>;
                    tStringReader.Close();
                }
            }
        }

        /// <summary>
        /// UIDesign界面设计与功能绑定由对象转为字符串
        /// </summary>
        /// <param name="UIDesignXmlString"></param>
        /// <param name="BindFunXmlString"></param>
        /// <param name="UIDesignXml"></param>
        /// <param name="BindFuns"></param>
        public static void UIDesignObjectToXmlString(ref string UIDesignXmlString, ref string BindFunXmlString,
            XmlDocument UIDesignXml, List<ItemCommandInfo> BindFuns)
        {
            UIDesignXmlString = UIDesignXml.OuterXml.Replace("<DOCUMENT_ELEMENT>", "");
            UIDesignXmlString = UIDesignXmlString.Replace("</DOCUMENT_ELEMENT>", "");

            using (StringWriter tStringWriter = new StringWriter())
            {
                XmlSerializer tXmlSerializer = new XmlSerializer(typeof(List<ItemCommandInfo>));
                tXmlSerializer.Serialize(tStringWriter, BindFuns);

                BindFunXmlString = tStringWriter.ToString();
            }
        }

        /// <summary>
        /// 把UIDesign保存到Xml
        /// </summary>
        /// <param name="UIDesignXmlString"></param>
        /// <param name="BindFunXmlString"></param>
        public static void SaveToXml(string UIDesignXmlString, string BindFunXmlString)
        {
            //若文件存在先删除
            if (File.Exists(CommonConstString.STR_UIDesignXml))
            {
                File.Delete(CommonConstString.STR_UIDesignXml);
            }

            if (File.Exists(CommonConstString.STR_BindFunXml))
            {
                File.Delete(CommonConstString.STR_BindFunXml);
            }

            using (StreamWriter writer = new StreamWriter(CommonConstString.STR_UIDesignXml))
            {
                writer.Write(UIDesignXmlString);
                writer.Close();
            }

            using (StreamWriter writer = new StreamWriter(CommonConstString.STR_BindFunXml))
            {
                writer.Write(BindFunXmlString);
                writer.Close();
            }
        }

        /// <summary>
        /// 把UIDesign保存到Database
        /// </summary>
        /// <param name="UIDesignXmlString"></param>
        /// <param name="BindFunXmlString"></param>
        public static void SaveToDatabase(string UIDesignXmlString, string BindFunXmlString)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            //先把所有数据清掉（因为此表只会存在一条数据）
            IList<AGSDM_UI_DESIGN> tUI_DESIGNs = tEntityHandler.GetEntities<AGSDM_UI_DESIGN>("from AGSDM_UI_DESIGN t");
            foreach (AGSDM_UI_DESIGN tUI_DESIGN in tUI_DESIGNs)
            {
                tEntityHandler.DeleteEntity(tUI_DESIGN);
            }

            AGSDM_UI_DESIGN tUI_DESIGNAdd = new AGSDM_UI_DESIGN();
            tUI_DESIGNAdd.UIDESIGN = UIDesignXmlString;
            tUI_DESIGNAdd.BINDFUN = BindFunXmlString;
            tEntityHandler.AddEntity(tUI_DESIGNAdd);
        }

        /// <summary>
        /// 删除多余的控件和绑定功能
        /// </summary>
        /// <param name="UIDesignXmlString"></param>
        /// <param name="BindFunXmlString"></param>
        public static void DeleteNoUseContent(ref string UIDesignXmlString, ref string BindFunXmlString)
        {
            XmlDocument UIDesignXml = null;
            List<ItemCommandInfo> BindFuns = null;

            UIDesignXmlStringToObject(UIDesignXmlString, BindFunXmlString, out  UIDesignXml, out  BindFuns);

            DeleteNoUseContent(ref  UIDesignXml, ref BindFuns);

            UIDesignObjectToXmlString(ref  UIDesignXmlString, ref  BindFunXmlString, UIDesignXml, BindFuns);
        }

        /// <summary>
        /// 删除多余的控件和绑定功能
        /// </summary>
        /// <param name="UIDesignXml"></param>
        /// <param name="BindFuns"></param>
        public static void DeleteNoUseContent(ref XmlDocument UIDesignXml, ref List<ItemCommandInfo> BindFuns)
        {
            //删除除Form以外的顶级控件
            for (int i = 1; i < UIDesignXml.DocumentElement.ChildNodes.Count; i++)
            {
                UIDesignXml.DocumentElement.RemoveChild(UIDesignXml.DocumentElement.ChildNodes[i]);
            }

            //获取所有的可视控件名称
            List<string> tControlNames = new List<string>();
            GetControlName(ref tControlNames, UIDesignXml.DocumentElement.ChildNodes);

            //去掉没有绑定控件的绑定功能
            List<ItemCommandInfo> tBindFunsResult = new List<ItemCommandInfo>();
            foreach (ItemCommandInfo tBindFun in BindFuns)
            {
                if (tControlNames.Contains(tBindFun.ControlName))
                {
                    tBindFunsResult.Add(tBindFun);
                }
            }
            BindFuns = tBindFunsResult;
        }

        /// <summary>
        /// 循环获取的可视控件名称
        /// </summary>
        /// <param name="tControlNames"></param>
        /// <param name="tNodes"></param>
        private static void GetControlName(ref List<string> tControlNames, XmlNodeList tNodes)
        {
            foreach (XmlNode tNode in tNodes)
            {
                if (tNode.Name == "Object")
                {
                    XmlAttribute tNameAttr = tNode.Attributes["name"];
                    if (tNameAttr != null && !string.IsNullOrEmpty(tNameAttr.Value))
                    {
                        tControlNames.Add(tNameAttr.Value);

                        GetControlName(ref tControlNames, tNode.ChildNodes);
                    }
                }
            }
        }
    }
}
