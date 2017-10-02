using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;
using System.Windows.Forms;
using System.Diagnostics;

namespace FindAllPictures
{
    public class FindPictures
    {
        System.Windows.Forms.CheckedListBox displayClb;

        List<PictureInfo> PicInfoList;
        public FindPictures(System.Windows.Forms.CheckedListBox clb )
        {
            PicInfoList = new List<PictureInfo>();
            displayClb = clb;
        }
        public  BlockingCollection<PictureInfo> picturesFound = new BlockingCollection<PictureInfo>();
        public static List<string> PictureFileTypes = new List<string>() { ".png", ".jpg", ".gif" };

        public static void ListAllPictures(System.Windows.Forms.CheckedListBox clb)
        {
            
        }

        public   void GetPictures(DirectoryInfo workingDir, CheckedListBox clb)
             {
            FileInfo[] fi = null; ;
            // if this directory has files in it, add its path to the list.
            try
            {
                fi = workingDir.GetFiles();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            if (fi != null && fi.Length > 0)
            {
                foreach(var file in fi)
                {
                    foreach (var pType in PictureFileTypes)
                    {
                        if (file.FullName.EndsWith(pType))
                        {
                            Debug.WriteLine(file.FullName);
                            var picInfo = new PictureInfo() { FullPath = file.FullName };
                            PicInfoList.Add(picInfo);
                          
                            picturesFound.Add(picInfo);
                        }
                    }

                }
             //   paths.Add(workingDir.FullName);
            }

            // Else, this directory has no files, so iterate through its children.
            DirectoryInfo[] diList = null;
            try
            {
                diList = workingDir.GetDirectories();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

            if (diList != null && diList.Length > 0)
            {
                foreach (var childDir in diList)
                {
                    GetPictures(childDir, clb);
                }
            }
           }
        
    }
    public class PictureInfo
    {
        public string FullPath { get; set; }
        public bool WillUse { get; set; }
    }
}
