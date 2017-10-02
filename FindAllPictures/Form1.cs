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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string picPath = (string)pictureList.SelectedItem;
            picDisplay.Load(picPath);
        }

        private void picDisplay_Click(object sender, EventArgs e)
        {
           
        }
    }
}
