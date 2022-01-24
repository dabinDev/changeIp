using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WindowsApplication.activiity
{
    [DataContract]
    class UserInfo
    {
        [DataMember]
        private String name;
        [DataMember]
        private String adddress;
        [DataMember]
        private String password;

        public UserInfo(string name, string adddress, string password)
        {
            this.name = name;
            this.adddress = adddress;
            this.password = password;
        }

        public string Name { get => name; set => name = value; }
        public string Adddress { get => adddress; set => adddress = value; }
        public string Password { get => password; set => password = value; }

       
    }
}
