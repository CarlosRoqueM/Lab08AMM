using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using Lab08AMM.Droid;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    [assembly: Xamarin.Forms.Dependency(typeof(BatteryImplementation))]
    namespace Lab08AMM.Droid
    {
        public class BatteryImplementation : IBattery
        {
            public BatteryImplementation()
            {
            }

            public int RemainingChargePercent
            {
                get
                {
                    try
                    {
                        using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                        {
                            using (var battery = Application.Context.RegisterReceiver(null, filter))
                            {
                                var level = battery.GetIntExtra(BatteryManager.ExtraLevel, -1);
                                var scale = battery.GetIntExtra(BatteryManager.ExtraScale, -1);

                                return (int)Math.Floor(level * 100D / scale);
                            }
                        }
                    }
                    catch
                    {
                        System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                        throw;
                    }
                }
            }

            public Lab08AMM.BatteryStatus Status
            {
                get
                {
                    try
                    {
                        using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                        {
                            using (var battery = Application.Context.RegisterReceiver(null, filter))
                            {
                                int status = battery.GetIntExtra(BatteryManager.ExtraStatus, -1);
                                var isCharging = status == (int)BatteryStatus.Charging || status == (int)BatteryStatus.Full;

                                var chargePlug = battery.GetIntExtra(BatteryManager.ExtraPlugged, -1);
                                var usbCharge = chargePlug == (int)BatteryPlugged.Usb;
                                var acCharge = chargePlug == (int)BatteryPlugged.Ac;
                                bool wirelessCharge = false;
                                wirelessCharge = chargePlug == (int)BatteryPlugged.Wireless;

                                isCharging = (usbCharge || acCharge || wirelessCharge);
                                if (isCharging)
                                    return Lab08AMM.BatteryStatus.Charging;

                                switch (status)
                                {
                                    case (int)BatteryStatus.Charging:
                                        return Lab08AMM.BatteryStatus.Charging;
                                    case (int)BatteryStatus.Discharging:
                                        return Lab08AMM.BatteryStatus.Discharging;
                                    case (int)BatteryStatus.Full:
                                        return Lab08AMM.BatteryStatus.Full;
                                    case (int)BatteryStatus.NotCharging:
                                        return Lab08AMM.BatteryStatus.NotCharging;
                                    default:
                                        return Lab08AMM.BatteryStatus.Unknown;
                                }
                            }
                        }
                    }
                    catch
                    {
                        System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                        throw;
                    }
                }
            }

            public PowerSource PowerSource
            {
                get
                {
                    try
                    {
                        using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                        {
                            using (var battery = Application.Context.RegisterReceiver(null, filter))
                            {
                                int status = battery.GetIntExtra(BatteryManager.ExtraStatus, -1);
                                var isCharging = status == (int)BatteryStatus.Charging || status == (int)BatteryStatus.Full;

                                var chargePlug = battery.GetIntExtra(BatteryManager.ExtraPlugged, -1);
                                var usbCharge = chargePlug == (int)BatteryPlugged.Usb;
                                var acCharge = chargePlug == (int)BatteryPlugged.Ac;

                                bool wirelessCharge = false;
                                wirelessCharge = chargePlug == (int)BatteryPlugged.Wireless;

                                isCharging = (usbCharge || acCharge || wirelessCharge);

                                if (!isCharging)
                                    return Lab08AMM.PowerSource.Battery;
                                else if (usbCharge)
                                    return Lab08AMM.PowerSource.Usb;
                                else if (acCharge)
                                    return Lab08AMM.PowerSource.Ac;
                                else if (wirelessCharge)
                                    return Lab08AMM.PowerSource.Wireless;
                                else
                                    return Lab08AMM.PowerSource.Other;
                            }
                        }
                    }
                    catch
                    {
                        System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                        throw;
                    }
                }
            }
        }
    }


