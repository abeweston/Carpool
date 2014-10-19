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
    class CastRelationship
    {
        CastMember memberSource;
        CastMember memberDestination;
        double dDistance;
        double dRatio;
        int intRankDistance;
        int intRankRatio;
        int intRankCombined;
        public CastRelationship(CastMember _memberSource, CastMember _memberDestination,double _dDistance, double _dRatio)
        {
            this.memberSource = _memberSource;
            this.memberDestination = _memberDestination;
            this.dDistance = _dDistance;
            this.dRatio = _dRatio;
            this.intRankDistance = 999;
            this.intRankRatio = 999;
            this.intRankCombined = 999;
        }

        public string CSVRow
        {
            get
            {
                string sRow = this.MemberDestination.ChildName + "," + this.MemberDestination.ParentName + "," + this.memberDestination.Address +
                    "," + this.memberDestination.City + "," + this.memberDestination.RolesInCSV + "," + this.memberDestination.Telephone + "," +
                    this.RankDistance + "," + this.RankRatio + "," + this.RankCombined + "\r\n";
                return sRow;
            }
        }

        public double Distance
        {
            get
            {
                return this.dDistance;
            }
        }

        public CastMember MemberDestination
        {
            get
            {
                return this.memberDestination;
            }
        }


        public CastMember MemberSource
        {
            get
            {
                return this.memberSource;
            }
        }

        public double Ratio
        {
            get
            {
                return this.dRatio;
            }
        }

        public int RankDistance
        {
            get
            {
                return this.intRankDistance;
            }
            set
            {
                this.intRankDistance = value;
            }
        }

        public int RankRatio
        {
            get
            {
                return this.intRankRatio;
            }
            set
            {
                this.intRankRatio = value;
            }
        }

        public int RankCombined
        {
            get
            {
                return (this.RankDistance + this.RankRatio) / 2;
            }
        }

    }
}
