﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace Carpool
{
    public partial class formCarpool : Form
    {
        public formCarpool()
        {
            InitializeComponent();
            
        
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string sSourceAddress = "177 Williams Ct.";
            string sSourceCity = "Fremont";
            string sSourceState = "CA";
            string sDestinationAddress = "38623 Cherry Ln";
            string sDestinationCity = "Fremont";
            string sDestinationState = "CA";

            string sActorSceneBreakdownFile = @"D:\abe\My Documents\StarStruck\Carpooling\seussical actor scene breakdown 9.25.14_without_commas.csv";
            ArrayList alPhrasesToRemove = new ArrayList();
            alPhrasesToRemove.Add(" in the hat");
            alPhrasesToRemove.Add(":");
            alPhrasesToRemove.Add("Isabel  Sophia  Nicole");
            alPhrasesToRemove.Add("Natalie  Jaezali  Amabel");
            alPhrasesToRemove.Add("FULL CAST");
            alPhrasesToRemove.Add("?");

            ActorSceneBreakdown breakdown = new ActorSceneBreakdown(sActorSceneBreakdownFile, _alPhrasesToRemove:alPhrasesToRemove);
            breakdown.Run();

            Distance dist = new Distance(sSourceAddress, sSourceCity, sSourceState, sDestinationAddress, sDestinationCity, sDestinationState);
            dist.Run();
            double dNumMiles = dist.NumMiles;
        }
    }
}
