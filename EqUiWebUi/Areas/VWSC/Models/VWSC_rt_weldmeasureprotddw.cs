//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.VWSC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VWSC_rt_weldmeasureprotddw
    {
        public int id { get; set; }
        public Nullable<int> timerId { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<int> protRecord_ID { get; set; }
        public Nullable<System.DateTime> dateTime { get; set; }
        public Nullable<short> progNo { get; set; }
        public Nullable<int> spotId { get; set; }
        public Nullable<decimal> wear { get; set; }
        public Nullable<decimal> wearPerCent { get; set; }
        public Nullable<int> monitorState { get; set; }
        public string monitorState_txt { get; set; }
        public Nullable<int> regulationState { get; set; }
        public string regulationState_txt { get; set; }
        public Nullable<int> measureState { get; set; }
        public string measureState_txt { get; set; }
        public Nullable<int> powerState { get; set; }
        public string powerState_txt { get; set; }
        public Nullable<int> sequenceState { get; set; }
        public string sequenceState_txt { get; set; }
        public Nullable<int> sequenceStateAdd { get; set; }
        public string sequenceStateAdd_txt { get; set; }
        public Nullable<int> sequenceRepeat { get; set; }
        public string sequenceRepeat_txt { get; set; }
        public Nullable<int> monitorMode { get; set; }
        public string monitorMode_txt { get; set; }
        public Nullable<decimal> iDemandStd { get; set; }
        public Nullable<decimal> ilsts { get; set; }
        public Nullable<int> regulationStd { get; set; }
        public string regulationStd_txt { get; set; }
        public Nullable<decimal> iDemand1 { get; set; }
        public Nullable<decimal> iActual1 { get; set; }
        public Nullable<int> regulation1 { get; set; }
        public string regulation1_txt { get; set; }
        public Nullable<decimal> iDemand2 { get; set; }
        public Nullable<decimal> iActual2 { get; set; }
        public Nullable<int> regulation2 { get; set; }
        public string regulation2_txt { get; set; }
        public Nullable<decimal> iDemand3 { get; set; }
        public Nullable<decimal> iActual3 { get; set; }
        public Nullable<int> regulation3 { get; set; }
        public string regulation3_txt { get; set; }
        public Nullable<decimal> phaStd { get; set; }
        public Nullable<decimal> pha1 { get; set; }
        public Nullable<decimal> pha2 { get; set; }
        public Nullable<decimal> pha3 { get; set; }
        public Nullable<decimal> t_iDemandStd { get; set; }
        public Nullable<decimal> tActualStd { get; set; }
        public string partIdentString { get; set; }
        public Nullable<int> tipDressCounter { get; set; }
        public Nullable<int> electrodeNo { get; set; }
        public Nullable<decimal> voltageActualValue { get; set; }
        public Nullable<decimal> voltageRefValue { get; set; }
        public Nullable<decimal> currentActualValue { get; set; }
        public Nullable<decimal> currentReferenceValue { get; set; }
        public Nullable<int> weldTimeActualValue { get; set; }
        public Nullable<int> weldTimeRefValue { get; set; }
        public Nullable<float> energyActualValue { get; set; }
        public Nullable<float> energyRefValue { get; set; }
        public Nullable<float> powerActualValue { get; set; }
        public Nullable<float> powerRefValue { get; set; }
        public Nullable<int> resistanceActualValue { get; set; }
        public Nullable<int> resistanceRefValue { get; set; }
        public Nullable<decimal> pulseWidthActualValue { get; set; }
        public Nullable<decimal> pulseWidthRefValue { get; set; }
        public Nullable<decimal> stabilisationFactorActValue { get; set; }
        public Nullable<decimal> stabilisationFactorRefValue { get; set; }
        public Nullable<decimal> thresholdStabilisationFactor { get; set; }
        public Nullable<decimal> wldEffectStabilisationFactor { get; set; }
        public Nullable<int> uipActualValue { get; set; }
        public Nullable<int> uipRefValue { get; set; }
        public Nullable<int> uirExpulsionTime { get; set; }
        public Nullable<int> uirMeasuringActive { get; set; }
        public string uirMeasuringActive_txt { get; set; }
        public Nullable<int> uirRegulationActive { get; set; }
        public string uirRegulationActive_txt { get; set; }
        public Nullable<int> uirMonitoringActive { get; set; }
        public string uirMonitoringActive_txt { get; set; }
        public Nullable<int> uirWeldTimeProlongationActive { get; set; }
        public string uirWeldTimeProlongActive_txt { get; set; }
        public Nullable<int> uirQStoppRefCntValue { get; set; }
        public Nullable<int> uirQStoppActCntValue { get; set; }
        public Nullable<decimal> uirUipUpperTol { get; set; }
        public Nullable<decimal> uirUipLowerTol { get; set; }
        public Nullable<decimal> uirUipCondTol { get; set; }
        public Nullable<decimal> uirPsfLowerTol { get; set; }
        public Nullable<decimal> uirPsfCondTol { get; set; }
    
        public virtual VWSC_c_Timer Timer { get; set; }
    }
}
