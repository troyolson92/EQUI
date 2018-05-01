//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.PJV.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProcessResult
    {
        public int id { get; set; }
        public Nullable<int> isDead { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<System.DateTime> C_lasttimestamp { get; set; }
        public Nullable<int> joiningPointDataId { get; set; }
        public Nullable<int> robotPointId { get; set; }
        public string signature { get; set; }
        public Nullable<int> isMissingOnRobot { get; set; }
        public Nullable<int> isOnTwoRobots { get; set; }
        public Nullable<int> isDuplicated { get; set; }
        public Nullable<int> rev_created { get; set; }
        public Nullable<int> rev_modified { get; set; }
        public Nullable<int> isMissingJointPointData { get; set; }
        public Nullable<int> isWrongProcessDataId { get; set; }
        public Nullable<int> isMatched { get; set; }
        public Nullable<int> isJpdMultipleDefined { get; set; }
        public Nullable<int> isTwiceInSequence { get; set; }
        public Nullable<int> isPathReversed { get; set; }
        public Nullable<int> isMismatchInGeometry { get; set; }
        public Nullable<int> areBothPaths { get; set; }
        public Nullable<double> deltaWX { get; set; }
        public Nullable<double> deltaWY { get; set; }
        public Nullable<double> deltaWZ { get; set; }
        public Nullable<double> deltaWRX { get; set; }
        public Nullable<double> deltaWRY { get; set; }
        public Nullable<double> deltaWRZ { get; set; }
        public Nullable<double> deltaWSum { get; set; }
        public string dirWX { get; set; }
        public string dirWY { get; set; }
        public string dirWZ { get; set; }
        public Nullable<double> deltaTX { get; set; }
        public Nullable<double> deltaTY { get; set; }
        public Nullable<double> deltaTZ { get; set; }
        public Nullable<double> deltaTRX { get; set; }
        public Nullable<double> deltaTRY { get; set; }
        public Nullable<double> deltaTRZ { get; set; }
        public Nullable<double> deltaTSum { get; set; }
        public Nullable<double> deltaPathLen { get; set; }
    
        public virtual JoiningPointData JoiningPointData { get; set; }
        public virtual RobotPoint RobotPoint { get; set; }
    }
}
