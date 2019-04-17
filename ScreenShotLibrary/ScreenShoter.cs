using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System;

namespace ScreenShotLibrary
{
    public class ScreenShoter
    {
        //Error Logger
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Creates a screenshot and saves it to screenshot directory next to exe file
        /// </summary>
        public void MakeScreenShot()
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                           Screen.PrimaryScreen.Bounds.Height,
                                           PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            Graphics g = Graphics.FromImage(bitmap);

            // Take the screenshot from the upper left corner to the right bottom corner.
            g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            try
            {
                if (!Directory.Exists("screenshots"))
                {
                    Directory.CreateDirectory("screenshots");
                }
                // Save the screenshot to the specified path
                bitmap.Save(@"screenshots\Screenshot.png", ImageFormat.Png);
            }
            catch(Exception)
            {
                //log it
                throw;
            }
            
        }

        /// <summary>
        /// Creates a screenshot and saves it to screenshot directory next to exe file with selected name
        /// </summary>
        /// <param name="fileName">Name of screenshot in directory</param>
        public void MakeScreenShot(string fileName)
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                           Screen.PrimaryScreen.Bounds.Height,
                                           PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            Graphics g = Graphics.FromImage(bitmap);

            // Take the screenshot from the upper left corner to the right bottom corner.
            g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            try
            {
                if (!Directory.Exists("screenshots"))
                {
                    Directory.CreateDirectory("screenshots");
                }
                // Save the screenshot to the specified path and name from argument
                bitmap.Save($@"screenshots\{fileName}.png", ImageFormat.Png);
            }
            catch (Exception ex)
            {
                //turn of screenshot saving
                log.Error("Unknown error while saving screenshot", ex);
            }

        }
    }
}
