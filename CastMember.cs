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
        string strName;
        string strAddress;
        string strCity;
        ArrayList alRoles;

        public CastMember(string _sName, string _sAddress, string _sCity, ArrayList _alRoles)
        {
            this.strName = _sName;
            this.strAddress = _sAddress;
            this.strCity = _sCity;
            this.alRoles = _alRoles;
        }

        public string Address
        {
            get
            {
                return this.strAddress;
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
                return this.Name + "_" + this.Address;
            }
        }


        public string Name
        {
            get
            {
                return this.strName;
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

    }
}
