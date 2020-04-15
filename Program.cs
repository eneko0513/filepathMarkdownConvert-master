using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Imaging;

namespace filepathMarkdownConvert
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string markdownLink = System.String.Empty;
            try
            {
                string srcPath = args[0].ToString();
                string fileName = Path.GetFileName(srcPath);

                string format = GetImageFormat(Path.GetExtension(srcPath));
                markdownLink = fileName + "](" + srcPath + ")";
                // 画像形式とそれ以外でフォーマットを変える
                markdownLink = (format != null) ? "![" + markdownLink : "[" + markdownLink;
                Clipboard.SetDataObject(markdownLink, true);
            }
            catch
            {
                // not
            }
        }

        /// <summary>
        /// イメージのファイルフォーマットを拡張子から判断し取得する [C#]:humming bird http://yas-hummingbird.blogspot.com/2009/02/c_06.html
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string GetImageFormat(string ext)
        {
            foreach (ImageCodecInfo ici in ImageCodecInfo.GetImageDecoders())
            {
                foreach (string s in GetFileNameExtensions(ici))
                {
                    if (s.ToUpper() == ext.ToUpper())
                    {
                        return ici.FormatDescription;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// イメージのファイルフォーマットを拡張子から判断し取得する [C#]:humming bird http://yas-hummingbird.blogspot.com/2009/02/c_06.html
        /// </summary>
        /// <param name="ici"></param>
        /// <returns></returns>

        public static IEnumerable GetFileNameExtensions(ImageCodecInfo ici)
        {
            foreach (string s in ici.FilenameExtension.Split(';'))
            {
                yield return s.Substring(s.IndexOf('.'));
            }
        }
    }
}
