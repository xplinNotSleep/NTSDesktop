using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG.COM.SDM.Config
{
    [Serializable]
    public class LoginDesignHelper
    {
        public static readonly LoginDesignHelper Instance = new LoginDesignHelper();

        private LoginDesignHelper()
        {
        }

        public const string xmlFile = "LoginWindowDesign.Setting";

        public AGLoginFormEntity MainForm { get; set; }

        public List<AGLoginUCEntity> Controls { get; set; }

        public void Save()
        {
            using (FileStream fs = new FileStream(xmlFile, FileMode.OpenOrCreate))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, this);
            }
            isLoad = false;
        }
        bool isLoad = false;
        public bool Init(bool isForce = false)
        {
            if (!isForce && isLoad) return true;

            try
            {
                isLoad = false;
                if (File.Exists(xmlFile))
                {
                    using (FileStream fs = new FileStream(xmlFile, FileMode.OpenOrCreate))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        var m = bf.Deserialize(fs) as LoginDesignHelper;
                        if (m != null)
                        {
                            this.MainForm = m.MainForm;
                            this.Controls = m.Controls;
                        }
                    }
                    isLoad = true;
                }
            }
            catch (Exception ex)
            {
                isLoad = false;
                MessageBox.Show(ex.Message);
            }

            if (isForce && !isLoad)
            {
                MainForm = new AGLoginFormEntity() { Name = "frmLogin", Text = "地下管线管理信息系统" };
                Controls = new List<AGLoginUCEntity>() {
                    new AGLoginUCEntity() { Name = "txtLogName", Text = "用户名" },
                    new AGLoginUCEntity() { Name = "txtPassword", Text = "密码" },
                    new AGLoginUCEntity() { Name = "chkLoadLastProject", Text = "自动加载工程" },
                    new AGLoginUCEntity() { Name = "lblMessage", Text = "提示文本" },
                    new AGLoginUCEntity() { Name = "btnCancel", Text = "取消按钮" },
                    new AGLoginUCEntity() { Name = "btnLogin", Text = "确定按钮" },
                    new AGLoginUCEntity() { Name = "btnDBSet", Text = "设置按钮" }
                };
                return true;
            }

            return isLoad;
        }
    }
}
