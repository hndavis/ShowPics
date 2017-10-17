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

        private void pbSave_Click(object sender, EventArgs e)
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
                    if (pictureList.GetItemChecked(i))
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
                XmlDocument picList = new XmlDocument();
                picList.Load(openDialogPictList.FileName);
            }

        }
    }
}
