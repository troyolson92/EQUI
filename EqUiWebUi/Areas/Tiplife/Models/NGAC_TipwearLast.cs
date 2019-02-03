//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.Tiplife.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NGAC_TipwearLast
    {
        public string controller_name { get; set; }
        public string LocationTree { get; set; }
        public int id { get; set; }
        public Nullable<int> rt_csv_file_id { get; set; }
        public Nullable<System.DateTime> Date_Time { get; set; }
        public short Tool_Nr { get; set; }
        public Nullable<short> Dress_Num { get; set; }
        public Nullable<short> Weld_Counter { get; set; }
        public string Dress_Reason { get; set; }
        public string Weld_Result { get; set; }
        public string Length_Fixed_Result { get; set; }
        public string Length_Move_Result { get; set; }
        public Nullable<double> Max_Wear_Fixed { get; set; }
        public Nullable<double> Wear_Fixed { get; set; }
        public Nullable<double> DiffFrLastWear_Fixed { get; set; }
        public Nullable<double> Max_Wear_Move { get; set; }
        public Nullable<double> Wear_Move { get; set; }
        public Nullable<double> DiffFrLastWear_Move { get; set; }
        public Nullable<double> MaxDiffFrLastMeas { get; set; }
        public Nullable<double> Current_TipWear { get; set; }
        public Nullable<double> TipWearRatio { get; set; }
        public Nullable<double> Dress_Time1 { get; set; }
        public Nullable<double> Dress_Pressure1 { get; set; }
        public Nullable<double> Dress_Time2 { get; set; }
        public Nullable<double> Dress_Pressure2 { get; set; }
        public Nullable<double> CleanDress_Time { get; set; }
        public Nullable<double> CleanDress_Pressure { get; set; }
        public Nullable<double> Time_DressCycleTime { get; set; }
        public string ErrorType { get; set; }
        public string ExtraInfo { get; set; }
        public Nullable<double> GunTCP_X { get; set; }
        public Nullable<double> GunTCP_Y { get; set; }
        public Nullable<double> GunTCP_Z { get; set; }
        public Nullable<double> GunRefTCP_X { get; set; }
        public Nullable<double> GunRefTCP_Y { get; set; }
        public Nullable<double> GunRefTCP_Z { get; set; }
        public Nullable<double> NomTCP_X { get; set; }
        public Nullable<double> NomTCP_Y { get; set; }
        public Nullable<double> NomTCP_Z { get; set; }
        public string Tool_NrHs { get; set; }
        public string ChkDrWear_Fixed_Result { get; set; }
        public string ChkDrWear_Move_Result { get; set; }
        public Nullable<double> FxSens_SetupVal { get; set; }
        public Nullable<double> FxSens_StartVal { get; set; }
        public Nullable<double> FxSens_PrevVal { get; set; }
        public Nullable<double> FxSens_PrevWare { get; set; }
        public Nullable<double> FxSens_DiffValue { get; set; }
        public Nullable<double> FxSens_MaxSensZComp { get; set; }
        public Nullable<double> FxSens_WarmSensZComp { get; set; }
        public Nullable<double> FxSens_FlPinPrevVal { get; set; }
        public Nullable<double> FxSens_FlPinSetupVal { get; set; }
        public Nullable<double> FxSens_FlPinPhysActVal { get; set; }
        public Nullable<double> FxSens_FlPinPhysSetupVal { get; set; }
        public string Internal_Arg { get; set; }
        public Nullable<double> DeltaRef { get; set; }
        public Nullable<double> DeltaNom { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<long> rnDesc { get; set; }
        public Nullable<double> avgESTnSpotsFixedWearBefore100 { get; set; }
        public Nullable<double> avgESTnSpotsMoveWearBefore100 { get; set; }
        public Nullable<double> maxWearInCalc { get; set; }
        public Nullable<double> minWearInCalc { get; set; }
        public Nullable<int> countWearInCalc { get; set; }
        public Nullable<double> ESTremainingspotsFixed { get; set; }
        public Nullable<double> ESTremainingspotsMove { get; set; }
        public Nullable<double> ESTremainingCarsFixed { get; set; }
        public Nullable<double> ESTremainingsCarsMove { get; set; }
        public Nullable<System.DateTime> LastTipchange { get; set; }
        public Nullable<double> avgDeltaNomAfterchange { get; set; }
        public Nullable<int> TotWearComponent { get; set; }
        public Nullable<double> Last_FixedWearBeforeChange { get; set; }
        public Nullable<double> Last_MovWearBeforeChange { get; set; }
        public Nullable<int> c_controller_id { get; set; }
        public Nullable<short> PrevDress_Num { get; set; }
    }
}