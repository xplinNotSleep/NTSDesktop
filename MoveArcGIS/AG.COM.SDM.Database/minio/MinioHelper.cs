using Minio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AG.COM.SDM.Database
{
    /// <summary>
    /// Minio服务器读写帮助类
    /// </summary>
    public static partial class MinioHelper
    {

        public static bool IsUpload = false;
        /// <summary>
        /// 定义MinIO客户端对象
        /// </summary>
        private static MinioClient minio;

        #region 公用方法

        /// <summary>
        /// 设置桶的访问策略（读、写、读和写）
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="policyType"></param>
        /// <returns></returns>
        public static async Task SetPolicyAsync(string bucketName, int policyType = 1)
        {
            //设置桶的访问权限（读、写、读和写）
            //var policyRead = "{\"Version\":\"2012-10-17\",\"Statement\":[{\"Effect\":\"Allow\",\"Principal\":{\"AWS\":[\"*\"]},\"Action\":[\"s3:GetBucketLocation\"],\"Resource\":[\"arn:aws:s3:::BUCKETNAME\"]},{\"Effect\":\"Allow\",\"Principal\":{\"AWS\":[\"*\"]},\"Action\":[\"s3:ListBucket\"],\"Resource\":[\"arn:aws:s3:::BUCKETNAME\"],\"Condition\":{\"StringEquals\":{\"s3:prefix\":[\"BUCKETPREFIX\"]}}},{\"Effect\":\"Allow\",\"Principal\":{\"AWS\":[\"*\"]},\"Action\":[\"s3:GetObject\"],\"Resource\":[\"arn:aws:s3:::BUCKETNAME/BUCKETPREFIX*\"]}]}";
            //var policyWrite = "{\"Version\":\"2012-10-17\",\"Statement\":[{\"Effect\":\"Allow\",\"Principal\":{\"AWS\":[\"*\"]},\"Action\":[\"s3:GetBucketLocation\",\"s3:ListBucketMultipartUploads\"],\"Resource\":[\"arn:aws:s3:::BUCKETNAME\"]},{\"Effect\":\"Allow\",\"Principal\":{\"AWS\":[\"*\"]},\"Action\":[\"s3:AbortMultipartUpload\",\"s3:DeleteObject\",\"s3:ListMultipartUploadParts\",\"s3:PutObject\"],\"Resource\":[\"arn:aws:s3:::BUCKETNAME/BUCKETPREFIX*\"]}]}";
            //var policyReadWrite = "{\"Version\":\"2012-10-17\",\"Statement\":[{\"Effect\":\"Allow\",\"Principal\":{\"AWS\":[\"*\"]},\"Action\":[\"s3:GetBucketLocation\",\"s3:ListBucketMultipartUploads\"],\"Resource\":[\"arn:aws:s3:::BUCKETNAME\"]},{\"Effect\":\"Allow\",\"Principal\":{\"AWS\":[\"*\"]},\"Action\":[\"s3:ListBucket\"],\"Resource\":[\"arn:aws:s3:::BUCKETNAME\"],\"Condition\":{\"StringEquals\":{\"s3:prefix\":[\"BUCKETPREFIX\"]}}},{\"Effect\":\"Allow\",\"Principal\":{\"AWS\":[\"*\"]},\"Action\":[\"s3:PutObject\",\"s3:AbortMultipartUpload\",\"s3:DeleteObject\",\"s3:GetObject\",\"s3:ListMultipartUploadParts\"],\"Resource\":[\"arn:aws:s3:::BUCKETNAME/BUCKETPREFIX*\"]}]}";

            //var policySet = policyType == 1 ? policyRead :
            //   policyType == 2 ? policyWrite : policyReadWrite;

            //await minio.SetPolicyAsync(bucketName, policySet.Replace("BUCKETNAME", bucketName).Replace("BUCKETPREFIX", "*.*"));
        }

        /// <summary>
        /// 上传文件到MinIO
        /// </summary>
        /// <param name="minio"></param>
        /// <param name="bucketName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<string> UploadFile(MinioClient minio, string bucketName, string filePath)
        {
            return await Task.Run(async () =>
            {
                var retMsg = string.Empty;

                try
                {
                    //上传到MinIO中的文件名称
                    var objName = filePath.Substring(filePath.LastIndexOf('\\') + 1, filePath.Length - filePath.LastIndexOf('\\') - 1);

                    //检查是否存在桶
                    bool existBucket = await minio.BucketExistsAsync(bucketName);
                    if (!existBucket)
                    {
                        //创建桶
                        //await minio.MakeBucketAsync(bucketName);

                        //设置桶的访问权限（读、写、读和写）
                        //await MinioHelper.SetPolicyAsync(bucketName, 1);

                        retMsg = "系统配置中的桶名称在连接的minio服务器上不存在,请重新配置桶名称!";
                        IsUpload = false;
                        return retMsg;
                    }

                    //上传文件到桶中
                    await minio.PutObjectAsync(bucketName, objName, filePath);

                    retMsg = "成功上传：" + objName;
                    IsUpload = true;
                }
                catch (Exception e)
                {
                    retMsg = "文件上传错误：" + e.Message +",请检查系统配置中的minio服务器是否能进行连接";
                    //MessageBox.Show(retMsg);
                    IsUpload = false;
                    return retMsg;
                }

                return retMsg;
            });
        }



        #endregion
    }
}