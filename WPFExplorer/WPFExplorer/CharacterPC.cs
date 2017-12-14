using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;


namespace WPFExplorer
{
    /// <summary>
    /// class for read parameters PC
    /// </summary>
    static class CharecterPC
    {
        /// <summary>
        /// method of obtaining information videoController
        /// </summary>
        /// <returns>information videoController</returns>
        public static void VideoController()
        {
            string videoConroller = null;
            ManagementObjectSearcher mos = new ManagementObjectSearcher(@"root\CIMV2",
                "SELECT * FROM Win32_VideoController");
            foreach (ManagementObject mo in mos.Get())
            {
                videoConroller = String.Format("{0}", mo["Caption"]);
            }
            MediatreInfoWin.VideoController(videoConroller);
        }

        /// <summary>
        ///  method of obtaining information Processor
        /// </summary>
        /// <returns>information Processor</returns>
        public static void Proccesor()
        {
            string proccesor = null;
            ManagementObjectSearcher mos = new ManagementObjectSearcher(@"root\CIMV2",
                "SELECT * FROM Win32_Processor");
            foreach (ManagementObject mo in mos.Get())
            {
                proccesor = string.Format("{0}", mo["Name"]);
            }
            MediatreInfoWin.Proccesor(proccesor);
        }

        /// <summary>
        ///  method of obtaining information RAM
        /// </summary>
        /// <returns>information Processor</returns>
        public static void RAM()
        {
            double ram = 0;
            ManagementObjectSearcher mos = new ManagementObjectSearcher(@"root\CIMV2",
            "SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject mo in mos.Get())
            {
                ram += Math.Round(System.Convert.ToDouble(mo["Capacity"]) / 1024 / 1024 / 1024, 2);
            }
            MediatreInfoWin.RAM(string.Format("{0},00 GB", ram.ToString()));
        }

        /// <summary>
        ///  method of obtaining information Windows
        /// </summary>
        /// <returns>information Processor</returns>
        public static void Windows()
        {
            string windows = null;
            ManagementObjectSearcher mos = new ManagementObjectSearcher(@"root\CIMV2",
           "SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject mo in mos.Get())
            {
                windows = string.Format("{0}", mo["Caption"]);
            }
            MediatreInfoWin.Windows(windows);
        }
    }
}
