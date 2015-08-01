using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Transactions;
using System.Windows.Forms;

namespace WatchDog
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtselectedFile.Text = dialog.SelectedPath;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string sourceFileName = "";
            string code = "";
            string savefileName = "";
            if (this.txtselectedFile.Text.Trim() != "")
            {
                sourceFileName = this.txtselectedFile.Text.Trim();
            }
            else
            {
                MessageBox.Show("请选择加密文件！");
                return;
            }
            if (this.txtCode.Text.Trim() != "")
            {
                code = this.txtCode.Text.Trim();
            }
            else
            {
                MessageBox.Show("请输入密码！");
                return;
            }

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择保存路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                savefileName = dialog.SelectedPath;
            }
            else
            {
                return;
            }

            using (TransactionScope scope = new TransactionScope())
            {
                DECAllFileEncryption decAll = new DECAllFileEncryption(code);
                DirectoryInfo sourceInfo = new DirectoryInfo(sourceFileName);
                try
                {
                    decAll.EncryptAllFiles(sourceInfo, savefileName);
                    scope.Complete();
                    MessageBox.Show("加密成功！");
                    this.txtCode.Text = "";
                    this.txtselectedFile.Text = "";
                }
                catch
                {
                    scope.Dispose();
                    MessageBox.Show("加密失败！");
                }
            }
        }

        private void btnDectypt_Click(object sender, EventArgs e)
        {
            string sourceFileName = "";
            string code = "";
            string savefileName = "";
            if (this.txtselectedFile.Text.Trim() != "")
            {
                sourceFileName = this.txtselectedFile.Text.Trim();
            }
            else
            {
                MessageBox.Show("请选择解密文件！");
                return;
            }
            if (this.txtCode.Text.Trim() != "")
            {
                code = this.txtCode.Text.Trim();
            }
            else
            {
                MessageBox.Show("请输入密码！");
                return;
            }

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件保存路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                savefileName = dialog.SelectedPath;
            }
            else
            {
                return;
            }

            using (TransactionScope scope = new TransactionScope())
            {
                DECAllFileEncryption decAll = new DECAllFileEncryption(code);
                DirectoryInfo sourceInfo = new DirectoryInfo(sourceFileName);
                try
                {
                    decAll.DecryptAllFiles(sourceInfo, savefileName);
                    scope.Complete();
                    MessageBox.Show("解密成功！");
                    this.txtCode.Text = "";
                    this.txtselectedFile.Text = "";
                }
                catch
                {
                    scope.Dispose();
                    MessageBox.Show("解密失败！");
                }
            }
        }
    }
}
