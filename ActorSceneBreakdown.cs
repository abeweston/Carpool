using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
//using System.Data.OleDb;
using Microsoft.VisualBasic.FileIO;


namespace Carpool
{

    class ActorSceneBreakdown
    {
        string strInputFile;
        int intStartingRow;

        List<string[]> listInputRows = new List<string[]>();
        ArrayList alRolesPerScene = new ArrayList();

        public ActorSceneBreakdown(string _sInputFile, int _iStartingRow=4)
        {
            this.strInputFile = _sInputFile;
            this.intStartingRow = _iStartingRow;
        }

        public List<string[]> InputRows
        {
            get
            {
                return this.listInputRows;
            }
        }

        public ArrayList RolesPerScene
        {
            get
            {
                return this.alRolesPerScene;
            }
        }


        public int GetRolesPerScene()
        {

            int iReturnStatus = -1;

            try
            {


                for (int i = 0; i < this.InputRows.Count; i++)
                {
                    ArrayList alRoles = new ArrayList();
                    foreach (string[] saRoles in this.InputRows)
                    {
                        int j = 0;
                        foreach (string sRole in saRoles)
                        {
                            if (i == j && sRole != "")
                            {
                                alRoles.Add(sRole);
                            }
                            j++;
                        }  
                    }
                    this.alRolesPerScene.Add(alRoles);
                    i++;
                }

                iReturnStatus = 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return iReturnStatus;
        }

        public int ReadFile()
        {

            int iReturnStatus = -1;
            TextFieldParser parser=null;
            bool bHasError = false;

            try
            {
                int iColumnCount = -1;
                bool bAfterFirstRow = false;

                parser = new TextFieldParser(this.strInputFile);
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                int i = 1;
                while (!parser.EndOfData)
                {

                    string[] fields = parser.ReadFields();

                    if (bAfterFirstRow == false)
                    {
                        iColumnCount = fields.Length;
                    }

                    if (i >= this.intStartingRow)
                    {

                        if (iColumnCount == fields.Length)
                        {
                            this.listInputRows.Add(fields);
                        }
                        else
                        {
                            bHasError = true;
                            Console.WriteLine("Number of columns in row number: " + i + " does not equal iColumnCount: " + iColumnCount + " which means that you probably have extra commas for fields: " + fields);
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
                    if (this.GetRolesPerScene() == 0)
                    {
                        iReturnStatus = 0;
                    }
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
