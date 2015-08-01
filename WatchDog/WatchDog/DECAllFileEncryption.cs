using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WatchDog
{
    public class DECAllFileEncryption
    {
        private string _password;

        public DECAllFileEncryption(string password)
        {
            _password = password;
        }

        /// <summary>
        /// 加密文件夹中所有文件
        /// </summary>
        /// <param name="sourceDirectoryInfo">需要加密的文件夹</param>
        /// <param name="targetPath">存储加密结果的文件夹</param>
        public void EncryptAllFiles(DirectoryInfo sourceDirectoryInfo, string targetPath)
        {
            //在目标文件夹下创建源文件根目录文件夹
            string myTargetPath = targetPath + "\\" + sourceDirectoryInfo.Name;
            if (!Directory.Exists(myTargetPath))
            {
                Directory.CreateDirectory(myTargetPath);
            }

            //获取目标文件夹中的子文件夹
            DirectoryInfo[] dirInfos = sourceDirectoryInfo.GetDirectories();
            foreach (DirectoryInfo item in dirInfos)
            {
                EncryptAllFiles(item, myTargetPath);
            }

            //获取目标文件夹中的子文件
            FileInfo[] fileInfos = sourceDirectoryInfo.GetFiles();
            foreach (FileInfo item in fileInfos)
            {
                DECFileEncryption.EncryptFile(item.FullName, myTargetPath + "\\" + item.Name, _password);
            }
        }

        /// <summary>
        /// 解密文件夹中所有文件
        /// </summary>
        /// <param name="sourceDirectoryInfo">需要解密的文件夹</param>
        /// <param name="targetPath">存储解密结果的文件夹</param>
        public void DecryptAllFiles(DirectoryInfo sourceDirectoryInfo, string targetPath)
        {
            //在目标文件夹下创建源文件根目录文件夹
            string myTargetPath = targetPath + "\\" + sourceDirectoryInfo.Name;
            if (!Directory.Exists(myTargetPath))
            {
                Directory.CreateDirectory(myTargetPath);
            }

            //获取目标文件夹中的子文件夹
            DirectoryInfo[] dirInfos = sourceDirectoryInfo.GetDirectories();
            foreach (DirectoryInfo item in dirInfos)
            {
                DecryptAllFiles(item, myTargetPath);
            }

            //获取目标文件夹中的子文件
            FileInfo[] fileInfos = sourceDirectoryInfo.GetFiles();
            foreach (FileInfo item in fileInfos)
            {
                DECFileEncryption.DecryptFile(item.FullName, myTargetPath + "\\" + item.Name, _password);
            }
        }
    }
}
