using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Data.OleDb;
using Microsoft.VisualBasic.FileIO;


namespace Carpool
{

    class ActorSceneBreakdown
    {
        string strInputFile;

        List<string[]> listRows = new List<string[]>();

        public ActorSceneBreakdown(string _sInputFile)
        {
            this.strInputFile = _sInputFile;
        }

        public List<string[]> ParsedData
        {
            get
            {
                return this.listRows;
            }
        }


        public int ReadFile()
        {

            int iReturnStatus = -1;
            TextFieldParser parser=null;

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

                    if (iColumnCount == fields.Length)
                    {
                        this.listRows.Add(fields);
                    }
                    else
                    {
                        Console.WriteLine("Number of columns in row number: " + i + " does not equal iColumnCount: " + iColumnCount + " which means that you probably have extra commas for fields: " + fields);
                    }

                    i++;
                }

                iReturnStatus = 0;

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
                if (ReadFile() == 0)
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
