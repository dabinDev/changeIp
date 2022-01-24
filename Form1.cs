using System;
using Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsApplication.activiity;
using System.Runtime.Serialization.Json;
using System.IO;

namespace WindowsApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

    private void Button1_Click(object sender, EventArgs e)
        {
            connect();
        }

        private void connect()
        {
            String name = input_name.Text;
            String adddress = input_address.Text;
            String password = input_password.Text;
            String message = "测试链接";
            if (name==null||name.Equals("")) {
                MessageBox.Show("宽带名称不能为空，通常为'默认宽带'");
                return;
            }
            if (adddress == null || adddress.Equals(""))
            {
                MessageBox.Show("链接地址不能为空！");
                return;
            }
            if (password == null || password.Equals(""))
            {
                MessageBox.Show("链接密码不能为空！");
                return;
            }
          bool isConnect=Adsl.Connect(name, adddress, password, ref message);
            if (isConnect) {
                UserInfo info = new UserInfo(name, adddress, password);
                //序列化json
                DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(UserInfo));
                using (MemoryStream stream = new MemoryStream())
                {
                    formatter.WriteObject(stream, info);
                    string result = System.Text.Encoding.UTF8.GetString(stream.ToArray());
                    CacheUtil.saveUserInfo(result);
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Adsl.Disconnect();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UserInfo info = null;
            String oriStr=CacheUtil.readUserInfo();
            if (oriStr!=null && oriStr.Length!=0&& oriStr.Contains("{")) {
                DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(UserInfo));
                using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(oriStr)))
                {
                    info = formatter.ReadObject(stream) as UserInfo;
                }
            }
            String name= info == null?"": info.Name;
            String adddress = info == null?"": info.Adddress;
            String password = info == null?"": info.Password;
            this.input_name.Text = name;
            this.input_address.Text = adddress;
            this.input_password.Text = password;
            //注册热键Shift+S，Id号为100。HotKey.KeyModifiers.Ctrl也可以直接使用数字4来表示。   
            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Ctrl, Keys.F11);
            //注册热键Ctrl+B，Id号为101。HotKey.KeyModifiers.Ctrl也可以直接使用数字2来表示。   
            HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Ctrl, Keys.F12);
        }

        
        protected override void WndProc(ref Message m)   
        {   
            const int WM_HOTKEY = 0x0312;   
            //按快捷键    
            switch (m.Msg)   
            {   
                case WM_HOTKEY:   
                    switch (m.WParam.ToInt32())   
                    {   
                        case 100:    //按下的是Shift+S   
                            connect();
                            break;   
                        case 101:    //按下的是Ctrl+B   
                            //此处填写快捷键响应代码   
            Adsl.Disconnect();
                            break;     
                    }   
                    break;   
            }   
            base.WndProc(ref m);   
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }
    }
}
