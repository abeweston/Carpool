using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using Microsoft.VisualBasic.FileIO;


namespace Carpool
{

    class CastMembers
    {
        string strInputFile;
        ArrayList alMembers;
        int intNumColumns = 6;
        public CastMembers(string _sInputFile)
        {
            this.strInputFile = _sInputFile;
            this.alMembers = new ArrayList();
        }

        public ArrayList Members
        {
            get
            {
                return this.alMembers ;
            }

        }

        public ArrayList IDs
        {
            get
            {
                ArrayList alIDs = new ArrayList();
                foreach (CastMember member in this.Members)
                {
                    alIDs.Add(member.ID);
                }

                return alIDs;
            }
        }

        private int ReadFile()
        {

            int iReturnStatus = -1;
            TextFieldParser parser=null;
            bool bHasError = false;

            try
            {

                parser = new TextFieldParser(this.strInputFile);
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                int i = 1;
                while (!parser.EndOfData)
                {

                    string[] fields = parser.ReadFields();

                    if (i >= 2)
                    {

                        if (this.intNumColumns == fields.Length)
                        {
                            //this.listInputRows.Add(fields);
                            string sChildName = fields[0];
                            string sParentName = fields[1];
                            string sAddress = fields[2];
                            string sCity = fields[3];
                            ArrayList alRoles = new ArrayList();
                            alRoles.AddRange(fields[4].Split('|'));
                            string sTelephone = fields[5];
                            CastMember member = new CastMember(sChildName, sParentName, sAddress, sCity, alRoles, sTelephone);

                            this.Members.Add(member);
                        }
                        else
                        {
                            bHasError = true;
                            Console.WriteLine("fields.Length: " + fields.Length + " in row number: " + i + " does not equal this.intNumScenes: " + this.intNumColumns + " which means that you probably have extra commas for fields: " + fields);
                        }
                    }

                    i++;
                }

                if (bHasError == false)
                {
                    iReturnStatus = 0;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                parser.Close();
            }

            return iReturnStatus;
        }

        public int Run()
        {
            int iReturnStatus = 0;

            try
            {
                if (this.ReadFile() == 0)
                {
                    iReturnStatus = 0;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return iReturnStatus;
        }

    }
}
