using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Models
{
    public class DiffQuery
    {
        GADATAEntities gADATAEntities = new GADATAEntities();
        //consturctor that takes a name to name the instance
        public DiffQuery()
        {
//constructor 
        }
        //configuration of querysnapshots
        public List<c_querySnapshots> c_querySnapshots
        {
            get
            {
                //  GADATAEntities gADATAEntities = new GADATAEntities();
                return gADATAEntities.c_querySnapshots.ToList();
            }
        }
        //querysnapshots instance
        public List<h_querySnapshots> h_querySnapshots
        {
            get
            {
                //  GADATAEntities gADATAEntities = new GADATAEntities();
                return gADATAEntities.h_querySnapshots.ToList();
            }
        }
        //querysnapshot html result 
        public List<l_querySnapshots> l_querySnapshots
        {
            get
            {
                // GADATAEntities gADATAEntities = new GADATAEntities();
                return gADATAEntities.l_querySnapshots.ToList();
            }
        }

        //selection set from ui 
        public string SelectedQuery { get; set; }
        public int SelectedSnapshotID1 { get; set; }
        public int SelectedSnapshotID2 { get; set; }
    }

}