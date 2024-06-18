using System.Windows.Forms;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// License��ʼ��
    /// </summary>
    public class LicenseInitializer
    {
        private IAoInitialize m_AoInitialize = new AoInitializeClass();

        /// <summary>
        /// Ӧ�ó����ʼ��ʱ����license
        /// </summary>
        /// <returns>����ɹ� �򷵻� true,���򷵻� false</returns>
        public bool InitializeAppliction()
        {
            #region
            //bool bInitizlized = true;
            //if (m_AoInitialize == null)
            //{
            //    MessageBox.Show("ArcGIS Lincense��ʼ�����ɹ���Ӧ�ó��������У�", "��Ϣ��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    bInitizlized = false;
            //}

            //Initialize the application
            //esriLicenseStatus pLicenseStatus = esriLicenseStatus.esriLicenseUnavailable;

            //ǩ��ArcInfo�Ĳ�Ʒ���
            //pLicenseStatus = CheckOutLicenses(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);

            //״̬�����������
            //if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
            //{
            //    //ǩ��EngineGeoDB�Ĳ�Ʒ���
            //    pLicenseStatus = CheckOutLicenses(esriLicenseProductCode.esriLicenseProductCodeArcInfo);

            //    if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
            //    {
            //        //ǩ�������ArcEditor���������(����ɰ���SDE�༭)
            //        pLicenseStatus = CheckOutLicenses(esriLicenseProductCode.esriLicenseProductCodeArcEditor);

            //        //�������ɲ����ã�����ǩ����ͼ����ArcView;
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
                MessageBox.Show("ArcGIS Lincense��ʼ�����ɹ���Ӧ�ó��������У�", "��Ϣ��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bInitizlized = false;
            }

            //Initialize the application
            esriLicenseStatus pLicenseStatus = esriLicenseStatus.esriLicenseUnavailable;

            //ǩ��ArcInfo�Ĳ�Ʒ���
            pLicenseStatus = CheckOutLicenses((esriLicenseProductCode)60);/*esriLicenseProductCodeArcInfo*/
            if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
            {
                //ǩ��ArcEditor�Ĳ�Ʒ���
                pLicenseStatus = CheckOutLicenses((esriLicenseProductCode)50/*esriLicenseProductCodeArcEditor*/);
                if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                {
                    //ǩ�������ArcView���������
                    pLicenseStatus = CheckOutLicenses((esriLicenseProductCode)40/*esriLicenseProductCodeArcView*/);
                    if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                    {
                        //ǩ�������ArcServer���������
                        pLicenseStatus = CheckOutLicenses((esriLicenseProductCode)30/*esriLicenseProductCodeArcServer*/);
                        if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                        {
                            //ǩ�������EngineGeoDB���������
                            pLicenseStatus = CheckOutLicenses((esriLicenseProductCode)20/*esriLicenseProductCodeEngineGeoDB*/);
                            if (pLicenseStatus != esriLicenseStatus.esriLicenseAvailable && pLicenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                            {
                                //ǩ�������Engine���������
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
        /// Ӧ�ó���ر�ʱǩ��License
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
        /// ǩ����Ӧ��ɵ�License
        /// </summary>
        /// <param name="productCode">license��ɱ��</param>
        /// <returns>����license��ǰ״̬</returns>
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
        /// ����licenseState�������ض�Ӧ����Ϣ
        /// </summary>
        /// <param name="licenseStatus">license״̬ö�ٱ���</param>
        /// <returns>���ص�ǰ״̬����Ӧ����Ϣ</returns>
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
