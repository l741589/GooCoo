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
        private int repvalue;

        public int Repvalue
        {
            get { return repvalue; }
            set { repvalue = value; }
        }

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
            return Object.Equals(lhs, rhs);
        }

        public static bool operator !=(User lhs, User rhs)
        {
            return !Object.Equals(lhs, rhs);
        }

        public override bool Equals(object obj)
        {
            if (Object.Equals(obj, null)) return false;
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