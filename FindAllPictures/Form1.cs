using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace FindAllPictures
{
    enum SaveType { All, Checked};
    enum Operation { SaveList, CopyFiles};
    public partial class Form1 : Form
    {
        List<PictureInfo> PicInfoList = new List<PictureInfo>();
        Action<PictureInfo> AddToListDel;
        public Form1()
        {
            InitializeComponent();
            AddToListDel = AddToList;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            PicInfoList.Clear();
            pictureList.Items.Clear();
            char[] deliminators = { ';', ',' };
            string[] drives = tbDrives.Text.Split(deliminators);

            FindPictures pics = new FindPictures(pictureList);
            //longRunnning
            foreach (var drive in drives)
            {
                DirectoryInfo di = new DirectoryInfo(drive);
                await Task.Factory.StartNew(() =>
                {


                    pics.GetPictures(di, pictureList);
                    foreach (var pic in pics.picturesFound)
                    {

                        Debug.WriteLine(pic.FullPath);
                        pictureList.Invoke(AddToListDel, pic);

                    }

                },
                TaskCreationOptions.LongRunning);
            }


        }



        void AddToList(PictureInfo pic)
        {


            pictureList.Items.Add(pic.FullPath);
        }

        private void pictureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string picPath = (string)pictureList.SelectedItem;
            picDisplay.Load(picPath);
        }

        private void picDisplay_Click(object sender, EventArgs e)
        {

        }
        private void Copy(SaveType saveType)
        {
            var StartingPointer = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var toDirName = dlg.SelectedPath;
                for (int i = 0; i < pictureList.Items.Count; i++)
                {

                    if ( saveType == SaveType.Checked)
                    {
                        if (pictureList.GetItemChecked(i))
                        {
                            var fileNameToCopy = pictureList.Items[i].ToString();
                            var fileNameDest = toDirName + "\\" + Path.GetFileName(fileNameToCopy);
                            File.Copy(fileNameToCopy, fileNameDest);
                        }
                    }
                    else
                    {
                        var fileNameToCopy = pictureList.Items[i].ToString();
                        var fileNameDest = toDirName +"\\"+ Path.GetFileName(fileNameToCopy);
                        File.Copy(fileNameToCopy, fileNameDest);
                    }


                }
            }
            Cursor.Current = StartingPointer;


        }

        private void pbSave_Click(object sender, EventArgs e)
        {
            Save(SaveType.Checked, Operation.SaveList);
        }
        private void Save(SaveType saveType, Operation operation)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            var profileDir = System.Environment.GetEnvironmentVariable("userprofile");
            var picShowDir = profileDir + "\\" + "FAP";
            if (!Directory.Exists(picShowDir))
            {
                Directory.CreateDirectory(picShowDir);

            }
            saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.InitialDirectory = picShowDir;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);
                XmlElement body = doc.CreateElement(string.Empty, "body", string.Empty);
                doc.AppendChild(body);

               

                for (int i = 0; i < pictureList.Items.Count; i++)
                {
                    if (saveType == SaveType.Checked)
                    {
                        if (pictureList.GetItemChecked(i))
                        {
                                XmlElement pictLo = doc.CreateElement(string.Empty, "Picture", string.Empty);
                                var a = doc.CreateAttribute("Location");
                                a.Value = pictureList.Items[i].ToString();
                                pictLo.Attributes.Append(a);
                                body.AppendChild(pictLo);
               

                        }
                    }
                    else if (saveType == SaveType.All)
                    {
                        XmlElement pictLo = doc.CreateElement(string.Empty, "Picture", string.Empty);
                        var a = doc.CreateAttribute("Location");
                        a.Value = pictureList.Items[i].ToString();
                        pictLo.Attributes.Append(a);
                        body.AppendChild(pictLo);

                    }
                }
                doc.Save(saveFileDialog1.FileName);

            }
        }

        private void Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialogPictList = new OpenFileDialog();
            openDialogPictList.Filter = "xml files (*.xml)|*.xml";
            openDialogPictList.FilterIndex = 1;
            var profileDir = System.Environment.GetEnvironmentVariable("userprofile");
            var picShowDir = profileDir + "\\" + "FAP";
            openDialogPictList.InitialDirectory = picShowDir;
            if (openDialogPictList.ShowDialog() == DialogResult.OK)
            {
                pictureList.Items.Clear();
                XmlDocument picList = new XmlDocument();
                picList.Load(openDialogPictList.FileName);
                foreach (XmlNode ig in picList.ChildNodes[1].ChildNodes)
                {
                    var fileName = ig.Attributes.GetNamedItem("Location").Value;
                    pictureList.Items.Add(fileName);
                }
            }

        }

        private void pbSaveAll_Click(object sender, EventArgs e)
        {
            Save(SaveType.All, Operation.SaveList);
        }

        private void CopyChecked_Click(object sender, EventArgs e)
        {
            Copy(SaveType.Checked);
        }

        private void CopyAll_Click(object sender, EventArgs e)
        {
            Copy(SaveType.All);
        }
    }
}
