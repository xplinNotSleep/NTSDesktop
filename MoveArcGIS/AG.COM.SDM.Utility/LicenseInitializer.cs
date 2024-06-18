using System.Windows.Forms;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// License初始化
    /// </summary>
    public class LicenseInitializer
    {
        private IAoInitialize m_AoInitialize = new AoInitializeClass();

        /// <summary>
        /// 应用程序初始化时设置license
        /// </summary>
        /// <returns>如果成功 则返回 true,否则返回 false</returns>
        public bool InitializeAppliction()
        {
            #region
            //bool bInitizlized = true;
            //if (m_AoInitialize == null)
            //{
            //    MessageBox.Show("ArcGIS Lincense初始化不成功，应用程序不能运行！", "信息提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    bInitizlized = false;
            //}

            //Initialize the application
            //esriLicenseStatus pLicenseStatus = esriLicenseStatus.esriLicenseUnavailable;

            //签出ArcInfo的产品许可
            //pLicenseStatus = CheckOutLicenses(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);

            //状态不可用情况下
            //if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
            //{
            //    //签出EngineGeoDB的产品许可
            //    pLicenseStatus = CheckOutLicenses(esriLicenseProductCode.esriLicenseProductCodeArcInfo);

            //    if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
            //    {
            //        //签出桌面版ArcEditor的运行许可(此许可包含SDE编辑)
            //        pLicenseStatus = CheckOutLicenses(esriLicenseProductCode.esriLicenseProductCodeArcEditor);

            //        //如果此许可不可用，则再签出其低级许可ArcView;
            //        if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
            //        {
            //            pLicenseStatus = CheckOutLicenses(esriLicenseProductCode.esriLicenseProductCodeArcView);
            //            if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
            //            {
            //                MessageBox.Show(LicenseMessage(pLicenseStatus), "ESRI License Initializer");
            //                bInitizlized = false;
            //            }
            //        }
            //    }
            //}
            #endregion

            bool bInitizlized = true;
            if (m_AoInitialize == null)
            {
                MessageBox.Show("ArcGIS Lincense初始化不成功，应用程序不能运行！", "信息提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bInitizlized = false;
            }

            //Initialize the application
            esriLicenseStatus pLicenseStatus = esriLicenseStatus.esriLicenseUnavailable;

            //签出ArcInfo的产品许可
            pLicenseStatus = CheckOutLicenses((esriLicenseProductCode)60);/*esriLicenseProductCodeArcInfo*/
            if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
            {
                //签出ArcEditor的产品许可
                pLicenseStatus = CheckOutLicenses((esriLicenseProductCode)50/*esriLicenseProductCodeArcEditor*/);
                if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                {
                    //签出桌面版ArcView的运行许可
                    pLicenseStatus = CheckOutLicenses((esriLicenseProductCode)40/*esriLicenseProductCodeArcView*/);
                    if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                    {
                        //签出桌面版ArcServer的运行许可
                        pLicenseStatus = CheckOutLicenses((esriLicenseProductCode)30/*esriLicenseProductCodeArcServer*/);
                        if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                        {
                            //签出桌面版EngineGeoDB的运行许可
                            pLicenseStatus = CheckOutLicenses((esriLicenseProductCode)20/*esriLicenseProductCodeEngineGeoDB*/);
                            if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                            {
                                //签出桌面版Engine的运行许可
                                pLicenseStatus = CheckOutLicenses((esriLicenseProductCode)10/*esriLicenseProductCodeEngine*/);
                                if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                                {
                                    MessageBox.Show(LicenseMessage(pLicenseStatus), "ESRI License Initializer");
                                    bInitizlized = false;
                                }
                            }
                        }
                    }
                }
            }

            return bInitizlized;

        }

        public void InitializeAppliction1()
        {
            RuntimeManager.Bind(ProductCode.EngineOrDesktop);
            AoInitialize aoi = new AoInitializeClass();
            esriLicenseProductCode productCode = esriLicenseProductCode.esriLicenseProductCodeAdvanced;// esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB;
            if (aoi.IsProductCodeAvailable(productCode) == esriLicenseStatus.esriLicenseAvailable)
            {
                aoi.Initialize(productCode);
            }
        }

        /// <summary>
        /// 应用程序关闭时签入License
        /// </summary>
        public void ShutdownApplication()
        {
            if (m_AoInitialize == null) return;

            //Checkin the extensions
            m_AoInitialize.CheckInExtension(esriLicenseExtensionCode.esriLicenseExtensionCode3DAnalyst);
            m_AoInitialize.CheckInExtension(esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst);
            m_AoInitialize.CheckInExtension(esriLicenseExtensionCode.esriLicenseExtensionCodeDataInteroperability);

            m_AoInitialize.Shutdown();
            m_AoInitialize = null;
        }

        /// <summary>
        /// 签出相应许可的License
        /// </summary>
        /// <param name="productCode">license许可编号</param>
        /// <returns>返回license当前状态</returns>
        private esriLicenseStatus CheckOutLicenses(esriLicenseProductCode productCode)
        {
            esriLicenseStatus licenseStatus = esriLicenseStatus.esriLicenseFailure;

            //Determine if the product is available
            licenseStatus = m_AoInitialize.IsProductCodeAvailable(productCode);

            if (licenseStatus == esriLicenseStatus.esriLicenseAvailable)
            {
                //Initialize the license
                licenseStatus = m_AoInitialize.Initialize(productCode);

                esriLicenseStatus licenseStatusExt = esriLicenseStatus.esriLicenseFailure;

                //Determine if the extensions are available
                licenseStatusExt = m_AoInitialize.IsExtensionCodeAvailable(productCode, esriLicenseExtensionCode.esriLicenseExtensionCode3DAnalyst);

                if (licenseStatusExt == esriLicenseStatus.esriLicenseAvailable)
                {
                    licenseStatusExt = m_AoInitialize.CheckOutExtension(esriLicenseExtensionCode.esriLicenseExtensionCode3DAnalyst);
                }

                licenseStatusExt = m_AoInitialize.IsExtensionCodeAvailable(productCode, esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst);
                if (licenseStatusExt == esriLicenseStatus.esriLicenseAvailable)
                {
                    //If you want to check out the Desktop Extension only when it is required instead of for
                    //the entire application, then move this code to where the extension is required. 
                    //Checkout the extensions
                    licenseStatusExt = m_AoInitialize.CheckOutExtension(esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst);
                }

                licenseStatusExt = m_AoInitialize.IsExtensionCodeAvailable(productCode, esriLicenseExtensionCode.esriLicenseExtensionCodeDataInteroperability);
                if (licenseStatusExt == esriLicenseStatus.esriLicenseAvailable)
                {
                    //If you want to check out the Desktop Extension only when it is required instead of for
                    //the entire application, then move this code to where the extension is required. 
                    //Checkout the extensions
                    licenseStatusExt = m_AoInitialize.CheckOutExtension(esriLicenseExtensionCode.esriLicenseExtensionCodeDataInteroperability);
                }

                licenseStatusExt = m_AoInitialize.IsExtensionCodeAvailable(productCode, esriLicenseExtensionCode.esriLicenseExtensionCodeMLE);
                if (licenseStatusExt == esriLicenseStatus.esriLicenseAvailable)
                {
                    //If you want to check out the Desktop Extension only when it is required instead of for
                    //the entire application, then move this code to where the extension is required. 
                    //Checkout the extensions
                    licenseStatusExt = m_AoInitialize.CheckOutExtension(esriLicenseExtensionCode.esriLicenseExtensionCodeMLE);
                }
            }

            return licenseStatus;
        }

        /// <summary>
        /// 根据licenseState变量返回对应的信息
        /// </summary>
        /// <param name="licenseStatus">license状态枚举变量</param>
        /// <returns>返回当前状态所对应的消息</returns>
        private string LicenseMessage(esriLicenseStatus licenseStatus)
        {
            string message = "";

            //Not licensed
            if (licenseStatus == esriLicenseStatus.esriLicenseNotLicensed)
            {
                message = "You are not licensed to run this product!";
            }
            //The licenses needed are currently in use
            else if (licenseStatus == esriLicenseStatus.esriLicenseUnavailable)
            {
                message = "There are insuffient licenses to run!";
            }
            //The licenses unexpected license failure
            else if (licenseStatus == esriLicenseStatus.esriLicenseFailure)
            {
                message = "Unexpected license failure! Please contact your administrator.";
            }
            //Already initialized (Initialization can only occur once)
            else if (licenseStatus == esriLicenseStatus.esriLicenseAlreadyInitialized)
            {
                message = "The license has already been initialized! Please check your implementation.";
            }
            return message;
        }
        public static bool RuntimeBind()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop))
            {
                if (!ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
