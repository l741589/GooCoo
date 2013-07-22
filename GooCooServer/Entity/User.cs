using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace GooCooServer.Entity
{
    //实体集，表示一个用户
    public class User
    {
        public enum EAuthority { USER, ADMIN, SUPERADMIN}
        private String id;//primay key
        private String name;
        private String password;
        private EAuthority authority;
        
        public EAuthority Authority
        {
            get { return authority; }
            set { authority = value; }
        }
        
        public String Id
        {
            get { return id; }
            set { id = value; }
        }
        
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        public static bool operator ==(User lhs, User rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(User lhs, User rhs)
        {
            return !lhs.Equals(rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj is User)
            {
                return Id == (obj as User).Id;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}