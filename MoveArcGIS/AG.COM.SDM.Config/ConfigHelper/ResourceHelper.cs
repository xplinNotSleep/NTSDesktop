using AG.COM.SDM.Utility;
using System.Collections;
using System.IO;
using System.Resources;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 资源文件帮助类
    /// </summary>
    public class ResourceHelper
    {
        private string m_FilePath;
        private Hashtable m_Hashtable;
        private string m_TempPath;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="filepath">资源文件路径</param>
        public ResourceHelper(string filepath, string tempPath)
        {
            this.m_FilePath = filepath;
            this.m_TempPath = tempPath;
            this.m_Hashtable = new Hashtable();

            //如果存在
            if (File.Exists(filepath))
            {
                //string tempFile = CommonConstString.STR_TempPath + "\\decryptFile.resx";
                string tempFile = tempPath + "\\decryptFile.resx";

                //解密文件
                Security tSecurity = new Security();
                tSecurity.DecryptDES(filepath, tempFile);

                using (ResourceReader ResReader = new ResourceReader(tempFile))
                {
                    IDictionaryEnumerator tDictEnum = ResReader.GetEnumerator();
                    while (tDictEnum.MoveNext())
                    {
                        try
                        {
                            //有可能没这类型导致反序列化出错，catch住不用处理
                            this.m_Hashtable.Add(tDictEnum.Key, tDictEnum.Value);
                        }
                        catch { }
                    }

                    ResReader.Close();
                }

                //删除临时文件
                File.Delete(tempFile);
            }
        }

        /// <summary>
        /// 获取或设置哈希表键值
        /// </summary>
        public Hashtable KeyValues
        {
            get
            {
                return this.m_Hashtable;
            }
            set
            {
                this.m_Hashtable = value;
            }
        }

        /// <summary>
        /// 获取指定关键字的字符串对象
        /// </summary>
        /// <param name="pResName">关键字</param>
        /// <returns>如果不存在指定的关键字则返回""</returns>
        public string GetString(string pResName)
        {
            object tObj = GetObject(pResName);
            return (tObj == null) ? "" : tObj.ToString();
        }

        /// <summary>
        /// 获取指定关键字对象的值
        /// </summary>
        /// <param name="pResName">关键字</param>
        /// <returns>如果不存在指定关键字对象则返回null.</returns>
        public object GetObject(string pResName)
        {
            string strKeyUpper = pResName.ToUpper();
            if (this.m_Hashtable.ContainsKey(strKeyUpper))
            {
                return this.m_Hashtable[strKeyUpper];
            }
            else if (this.m_Hashtable.ContainsKey(pResName))
            {
                return this.m_Hashtable[pResName];
            }
            return null;
        }

        /// <summary>
        /// 添加指定关键字及其值对象(如果存在则更新)
        /// </summary>
        /// <param name="pResName">关键字</param>
        /// <param name="pObj">Object值</param>
        public void SetObject(string pResName, object pObj)
        {
            string strKeyUpper = pResName.ToUpper();
            if (this.m_Hashtable.ContainsKey(strKeyUpper))
            {
                this.m_Hashtable[strKeyUpper] = pObj;
            }
            else
            {
                this.m_Hashtable.Add(strKeyUpper, pObj);
            }
        }

        /// <summary>
        /// 添加指定关键字及其值对象(如果存在则更新)
        /// </summary>
        /// <param name="pResName">关键字</param>
        /// <param name="pStr">字符串值</param>
        public void SetString(string pResName, string pStr)
        {
            SetObject(pResName, pStr);
        }

        /// <summary>
        /// 删除指定关键字的对象
        /// </summary>
        /// <param name="pResName">关键字名称</param>
        public void DeleteObject(string pResName)
        {
            if (m_Hashtable.ContainsKey(pResName) == true)
            {
                this.m_Hashtable.Remove(pResName);
            }
        }

        /// <summary>
        /// 保存对原有资源文件的修改
        /// </summary>
        public void Save()
        {
            SaveAs(this.m_FilePath, this.m_TempPath);
        }

        /// <summary>
        /// 另存为指定路径的资源文件
        /// </summary>
        /// <param name="filepath">指定的文件路径</param>
        public void SaveAs(string filepath, string tempPath)
        {
            if (filepath.Length == 0) return;

            //如果存在则先删除
            if (File.Exists(filepath)) File.Delete(filepath);

            string tempFile = tempPath + "\\encryFile.resx";
            //--刘飞 修改----2020/12/31 路径不存在会报错
            if (!Directory.Exists(tempPath))
            {
                DirectoryInfo info = Directory.CreateDirectory(tempPath);
                if (!info.Exists)
                {
                    return;
                }
            }
            using (ResourceWriter ResWriter = new ResourceWriter(tempFile))
            {
                foreach (DictionaryEntry tDictEntry in this.m_Hashtable)
                {
                    //添加资源对
                    ResWriter.AddResource(tDictEntry.Key.ToString(), tDictEntry.Value);
                }

                //将所有资源以系统默认格式输出到流
                ResWriter.Generate();

                ResWriter.Close();
            }

            //文件加密
            Security tSecurity = new Security();
            tSecurity.EncryptDES(tempFile, filepath);

            //删除临时文件
            File.Delete(tempFile);
        }
    }
}
