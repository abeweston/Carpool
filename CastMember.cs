using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Xml.XPath;
using System.Collections;

namespace Carpool
{
    class CastMember
    {
        string strChildName;
        string strParentName;
        string strAddress;
        string strCity;
        ArrayList alRoles;
        string strTelephone;

        public CastMember(string _sChildName, string _sParentName, string _sAddress, string _sCity, ArrayList _alRoles,string _sTelephone)
        {
            this.strChildName = _sChildName;
            this.strParentName = _sParentName;
            this.strAddress = _sAddress;
            this.strCity = _sCity;
            this.alRoles = _alRoles;
            this.strTelephone = _sTelephone;
        }

        public string Address
        {
            get
            {
                return this.strAddress;
            }
        }

        public string ChildName
        {
            get
            {
                return this.strChildName;
            }
        }

        public string City
        {
            get
            {
                return this.strCity;
            }
        }

        public string ID
        {
            get
            {
                return this.ChildName + "_" + this.Address;
            }
        }

        public string ParentName
        {
            get
            {
                return this.strParentName;
            }
        }

        public ArrayList Roles
        {
            get
            {
                return this.alRoles;
            }
        }

        public string RolesInCSV
        {
            get
            {
                string sRoles = "";
                int i = 0;
                foreach (string sRole in this.Roles)
                {
                    if (i == 0)
                    {
                        sRoles = sRole;
                    }
                    else
                    {
                        sRoles = sRoles + "|" + sRole;
                    }
                    i++;
                }

                return sRoles;
            }
        }

        public string Telephone
        {
            get
            {
                return this.strTelephone;
            }
        }

    }
}
