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

    class ActorSceneBreakdown
    {
        string strInputFile;
        int intStartingRow;

        int intNumScenes = 0;
        List<string[]> listInputRows = new List<string[]>();
        ArrayList alRolesPerScene = new ArrayList();
        ArrayList alUniqueRoles = new ArrayList();
        ArrayList alPhrasesToRemove = new ArrayList();
        Dictionary<string,int> dicCounts = new Dictionary<string,int>();
        Dictionary<string, int> dicTotals = new Dictionary<string, int>();
        Dictionary<string, double> dicRatios = new Dictionary<string, double>();

        public ActorSceneBreakdown(string _sInputFile, int _iStartingRow=4, ArrayList _alPhrasesToRemove=null)
        {
            this.strInputFile = _sInputFile;
            this.intStartingRow = _iStartingRow;
            this.alPhrasesToRemove = _alPhrasesToRemove;
        }

        private List<string[]> InputRows
        {
            get
            {
                return this.listInputRows;
            }
        }

        private Dictionary<string, int> Counts
        {
            get
            {
                return this.dicCounts;
            }
        }

        public Dictionary<string, double> Ratios
        {
            get
            {
                return this.dicRatios;
            }
        }

        private Dictionary<string, int> Totals
        {
            get
            {
                return this.dicTotals;
            }
        }


        private int NumScenes
        {
            get
            {
                return this.intNumScenes;
            }

        }

        private ArrayList RolesPerScene
        {
            get
            {
                return this.alRolesPerScene;
            }
        }

        private ArrayList UniqueRoles
        {
            get
            {
                return this.alUniqueRoles;
            }
        }

        private int CalculateCounts()
        {

            int iReturnStatus = -1;
            int iSuccessCount = 0;

            try
            {

                foreach (string sRole in this.UniqueRoles)
                {

                    if (this.CalculateCountsForRole(sRole) == 0)
                    {
                        iSuccessCount = iSuccessCount + 1;
                    }

                }

                if (iSuccessCount == this.UniqueRoles.Count)
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

        private int CalculateCountsForRole(string _sRole)
        {

            int iReturnStatus = -1;

            try
            {

                foreach (ArrayList alRoles in this.RolesPerScene)
                {

                    if (alRoles.Contains(_sRole))
                    {
                        foreach (string sRoleDestination in alRoles)
                        {
                            if (_sRole != sRoleDestination)
                            {
                                string sKey = _sRole + "|" + sRoleDestination;
                                if (this.Counts.ContainsKey(sKey))
                                {
                                    int iCount = this.Counts[sKey];
                                    this.Counts[sKey] = iCount + 1;
                                }

                                else
                                {
                                    this.Counts[sKey] = 1;
                                }
                            }
                        } 
                    }
                }

                iReturnStatus = 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return iReturnStatus;
        }

        private int CalculateRatios()
        {

            int iReturnStatus = -1;

            try
            {

                foreach (string sRoleSource in this.UniqueRoles)
                {
                    foreach (string sRoleDestination in this.UniqueRoles)
                    {
                        if (this.CalculateRatiosForRole(sRoleSource, sRoleDestination) == 0)
                        {
                            iReturnStatus = 0;
                        }
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return iReturnStatus;
        }

        private int CalculateRatiosForRole(string _sRoleSource, string _sRoleDestination)
        {

            int iReturnStatus = -1;

            try
            {
                string sKeyRoles = _sRoleSource + "|" + _sRoleDestination;

                if (_sRoleSource == _sRoleDestination)
                {
                    this.Ratios[sKeyRoles] = 1;
                    iReturnStatus = 0;
                }
                else
                {
                    int iTotal = this.Totals[_sRoleSource];
                    int iCount = 0;

                    if (this.Counts.ContainsKey(sKeyRoles))
                    {
                        iCount = this.Counts[sKeyRoles];
                    }

                    double dRatio = Math.Round(Convert.ToDouble(iCount) / Convert.ToDouble(iTotal), 2);
                    this.Ratios[sKeyRoles] = dRatio;

                    iReturnStatus = 0;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return iReturnStatus;
        }

        private int CalculateTotals()
        {

            int iReturnStatus = -1;
            int iSuccessCount = 0;

            try
            {

                foreach (string sRole in this.UniqueRoles)
                {

                    if (this.CalculateTotalsForRole(sRole) == 0)
                    {
                        iSuccessCount = iSuccessCount + 1;
                    }

                }

                if (iSuccessCount == this.UniqueRoles.Count)
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

        private int CalculateTotalsForRole(string _sRole)
        {

            int iReturnStatus = -1;

            try
            {

                foreach (KeyValuePair<string, int> item in this.Counts)
                {

                    string sKey = item.Key;
                    char[] splitter = { '|' };
                    string[] sRoleSourceDestination = sKey.Split(splitter);
                    string sRoleSource = sRoleSourceDestination[0];

                    if (sRoleSource == _sRole)
                    {
                        int iCount = item.Value;
                        if (this.Totals.ContainsKey(_sRole))
                        {
                            int iTotal = this.Totals[_sRole];
                            this.Totals[_sRole] = iTotal + iCount;
                        }

                        else
                        {
                            this.Totals[_sRole] = iCount;
                        }

                    }
                }

                iReturnStatus = 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return iReturnStatus;
        }

        private int FormatRoles(string _sInputRole, ref string _sOutputRole)
        {

            int iReturnStatus = -1;

            try
            {
                _sOutputRole = _sInputRole.ToLower();

                if (this.alPhrasesToRemove != null)
                {
                    foreach (string sPhrase in this.alPhrasesToRemove)
                    {
                        _sOutputRole = _sOutputRole.Replace(sPhrase.ToLower(), "");
                    }
                }
                iReturnStatus = 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return iReturnStatus;
        }

        public int GetGreatestRatio(ArrayList _alSourceRoles, ArrayList _alDestinationRoles, ref double dGreatestRatio)
        {

            int iReturnStatus = -1;
            dGreatestRatio = -1;

            try
            {
                int iSuccessCount = 0;
                int iExpectedCount = (_alSourceRoles.Count * _alDestinationRoles.Count);
                dGreatestRatio = -1;
                foreach (string sSourceRole in _alSourceRoles)
                {
                    foreach (string sDestinationRole in _alDestinationRoles)
                    {
                        double dRatio = -1;
                        if (this.GetRatio(sSourceRole,sDestinationRole,ref dRatio) == 0)
                        {
                            if (dRatio > dGreatestRatio)
                            {
                                dGreatestRatio = dRatio;
                            }
                            iSuccessCount = iSuccessCount + 1;
                        }
                    }
                }

                if (iSuccessCount == iExpectedCount)
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

        private int GetRatio(string _sSourceRole, string _sDestinationRole,ref double dRatio)
        {

            int iReturnStatus = -1;

            try
            {
                dRatio = -1;
                string sFormattedSourceRole = "";
                if (this.FormatRoles(_sSourceRole, ref sFormattedSourceRole) == 0)
                {
                    if (this.UniqueRoles.Contains(sFormattedSourceRole))
                    {
                        string sFormattedDestinationRole = "";
                        if (this.FormatRoles(_sDestinationRole, ref sFormattedDestinationRole) == 0)
                        {
                            if (this.UniqueRoles.Contains(sFormattedDestinationRole))
                            {
                                string sKey = sFormattedSourceRole + "|" + sFormattedDestinationRole;
                                if (this.Ratios.ContainsKey(sKey))
                                {
                                    dRatio = this.Ratios[sKey];
                                    iReturnStatus = 0;
                                }
                                else
                                {
                                    dRatio = 0;
                                    iReturnStatus = 0;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Unknown sFormattedDestinationRole: " + sFormattedDestinationRole);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unknown sFormattedSourceRole: " + sFormattedSourceRole);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return iReturnStatus;
        }

        private int GetRolesPerScene()
        {

            int iReturnStatus = -1;

            try
            {
                for (int i = 0; i < this.NumScenes; i++)
                {
                    ArrayList alRoles = new ArrayList();
                    foreach (string[] saRoles in this.InputRows)
                    {
                        int j = 0;
                        foreach (string sRole in saRoles)
                        {
                            string sRoleFormatted = "";
                            if (this.FormatRoles(sRole, ref sRoleFormatted) == 0)
                            {

                                if (this.UniqueRoles.Contains(sRoleFormatted) == false && sRoleFormatted != "")
                                {
                                    this.UniqueRoles.Add(sRoleFormatted);
                                }

                                if (i == j && sRoleFormatted != "")
                                {
                                    alRoles.Add(sRoleFormatted);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error when calling this.FormatRoles for sRole: " + sRole);
                            }
                            j++;
                        }
                    }
                    this.alRolesPerScene.Add(alRoles);
                }

                iReturnStatus = 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return iReturnStatus;
        }

        private int ReadFile()
        {

            int iReturnStatus = -1;
            TextFieldParser parser=null;
            bool bHasError = false;

            try
            {
                
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
                        this.intNumScenes = fields.Length;
                    }

                    if (i >= this.intStartingRow)
                    {

                        if (this.intNumScenes == fields.Length)
                        {
                            this.listInputRows.Add(fields);
                        }
                        else
                        {
                            bHasError = true;
                            Console.WriteLine("fields.Length: " + fields.Length + " in row number: " + i + " does not equal this.intNumScenes: " + this.intNumScenes + " which means that you probably have extra commas for fields: " + fields);
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
                        if (this.CalculateCounts() == 0)
                        {
                            if (this.CalculateTotals() == 0)
                            {
                                if (this.CalculateRatios() == 0)
                                {
                                    iReturnStatus = 0;
                                }
                            }
                        }
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
