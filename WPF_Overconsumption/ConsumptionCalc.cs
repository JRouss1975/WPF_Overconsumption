using MathNet.Numerics.Interpolation;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Overconsumption
{

    public enum FlowmeterType
    {
        Volume,
        Mass
    }

    public class ConsumptionCalc : Observable
    {
        //Engine Data
        public static double[] RPM_Data = new double[] { 5357, 9089, 11125, 11898, 12458 };
        public static double[] Power_Data = new double[] { 3390, 6780, 10170, 12204, 13560 };
        public static double[] ISO_FOC_Data = new double[] { 16.33, 31.11, 44.05, 51.92, 57.96 };

        public ConsumptionCalc()
        {

        }

        public static string VesselName { get; set; }

        public static double RPMLower
        {
            get
            {
                return RPM_Data[0];
            }
        }

        public static double RPMHigher
        {
            get
            {
                return RPM_Data[RPM_Data.Length - 1];
            }
        }

     
        private DateTime date = DateTime.Today;
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (value != date)
                {
                    date = value;
                    NotifyChange("");
                }
            }
        }


        private FlowmeterType flowmeter;
        public FlowmeterType Flowmeter
        {
            get { return flowmeter; }
            set
            {
                if (value != flowmeter)
                {
                    flowmeter = value;
                    NotifyChange("");
                }
            }
        }

    
        private double turboRPM;
        public double TurboRPM
        {
            get { return turboRPM; }
            set
            {
                if (value != turboRPM)
                {
                    turboRPM = value;
                    NotifyChange("");
                }
            }
        }

        public double CalculatedPower
        {
            get
            {
                // Create spline
                CubicSpline CSpline = CubicSpline.InterpolateNatural(RPM_Data, Power_Data);
                // Calculate
                if (TurboRPM >= RPM_Data[0] && TurboRPM <= RPM_Data[RPM_Data.Length - 1])
                    return CSpline.Interpolate(TurboRPM);
                return 0;
            }
        }

        public double ISOConsumption
        {
            get
            {
                // Create spline
                CubicSpline CSpline = CubicSpline.InterpolateNatural(RPM_Data, ISO_FOC_Data);
                // Calculate
                if (TurboRPM >= RPM_Data[0] && TurboRPM <= RPM_Data[RPM_Data.Length - 1])
                    return CSpline.Interpolate(TurboRPM);
                return 0;
            }
        }


        private double fuelDensity15deg;
        public double FuelDensity15deg
        {
            get { return fuelDensity15deg; }
            set
            {
                if (value != fuelDensity15deg)
                {
                    fuelDensity15deg = value;
                    NotifyChange("");
                }
            }
        }


        private double fuelEngineInletTemp;
        public double FuelEngineInletTemp
        {
            get { return fuelEngineInletTemp; }
            set
            {
                if (value != fuelEngineInletTemp)
                {
                    fuelEngineInletTemp = value;
                    NotifyChange("");
                }
            }
        }


        public double FuelDensityInletTemp
        {
            get
            {
                return FuelDensity15deg - 0.00065 * (FuelEngineInletTemp - 15);
            }
        }


        private double fuelInletVolume;
        public double FuelInletVolume
        {
            get { return fuelInletVolume; }
            set
            {
                if (value != fuelInletVolume)
                {
                    fuelInletVolume = value;
                    NotifyChange("");
                }
            }
        }


        private double fuelInletWeight;
        public double FuelInletWeight 
        {
            get { return fuelInletWeight; }
            set
            {
                if (value != fuelInletWeight)
                {
                    fuelInletWeight = value;
                    NotifyChange("");
                }
            }
        }


        private double fuelReturnVolume;
        public double FuelReturnVolume
        {
            get { return fuelReturnVolume; }
            set
            {
                if (value != fuelReturnVolume)
                {
                    fuelReturnVolume = value;
                    NotifyChange("");
                }
            }
        }









        private double fuelReturnWeight;
        public double FuelReturnWeight
        {
            get { return fuelReturnWeight; }
            set
            {
                if (value != fuelReturnWeight)
                {
                    fuelReturnWeight = value;
                    NotifyChange("");
                }
            }
        }

        public double Time { get; set; }

        public double CalorificValue { get; set; }

        public double InletBlowerTemp { get; set; }

        public double AmbientPressure { get; set; }

        public double SeaWaterTempInlet { get; set; }

        public double CorrectedISOConsumption { get; }

    }
}
