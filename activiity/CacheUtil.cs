using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsApplication.activiity
{
    class CacheUtil
    {
        private  static String userInfoFile = "\\userInfo.txt";

        public static void saveUserInfo(String str) {
            StreamWriter sw = new StreamWriter(Application.StartupPath + userInfoFile, false);
            sw.WriteLine(str);
            sw.Close();//写入
        }


        public static String readUserInfo()
        {
            String tempStr = "";
            try
            {
                StreamReader sr = new StreamReader(Application.StartupPath + userInfoFile, false);
                tempStr = sr.ReadLine().ToString();
                sr.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message.ToString());
            }
            return tempStr;
        }
          
    }
}
