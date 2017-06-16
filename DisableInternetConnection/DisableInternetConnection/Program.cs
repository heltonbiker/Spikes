using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using ROOT.CIMV2.Win32;
using System.Diagnostics;

namespace DisableInternetConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var i in NetworkInterface.GetAllNetworkInterfaces())
            {
                Console.WriteLine("{0} ({1})", i.Name, i.OperationalStatus);
            }

            //Process disableprocess = new Process();
            //ProcessStartInfo psi = new ProcessStartInfo(
            //    "netsh",
            //    "interface set interface name=\"Conexão local\" admin=disabled"
            //);
            //disableprocess.StartInfo = psi;
            //disableprocess.Start();

            //Console.Write("pressione enter para encerrar...");
            //Console.ReadLine();

            //Process enableprocess = new Process();
            //ProcessStartInfo epsi = new ProcessStartInfo(
            //    "netsh",
            //    "interface set interface name=\"Conexão local\" admin=enabled"
            //);
            //enableprocess.StartInfo = epsi;
            //enableprocess.Start();
        }


        private static string GetLocalIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }


        private static void SetIP(string ip_address, string subnet_mask)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    try
                    {
                        ManagementBaseObject setIP = default(ManagementBaseObject);
                        ManagementBaseObject newIP = objMO.GetMethodParameters("EnableStatic");

                        newIP["IPAddress"] = new string[] { ip_address };
                        newIP["SubnetMask"] = new string[] { subnet_mask };

                        setIP = objMO.InvokeMethod("EnableStatic", newIP, null);
                        var i = setIP;
                    }
                    catch (Exception generatedExceptionName)
                    {
                        throw;
                    }
                }


            }


        }

        private static void SetDHCP()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                // Make sure this is a IP enabled device. Not something like memory card or VM Ware
                if ((bool)mo["IPEnabled"])
                {
                    ManagementBaseObject newDNS = mo.GetMethodParameters("SetDNSServerSearchOrder");
                    newDNS["DNSServerSearchOrder"] = null;
                    ManagementBaseObject enableDHCP = mo.InvokeMethod("EnableDHCP", null, null);
                    ManagementBaseObject setDNS = mo.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                }
            }


        }

        public static IPAddress GetDefaultGateway()
        {
            var card = NetworkInterface.GetAllNetworkInterfaces()
                                       .Where(o => o.OperationalStatus==OperationalStatus.Up)
                                       .FirstOrDefault();
            if (card == null)
                return null;
            var address = card.GetIPProperties().GatewayAddresses                                                            
                                                .FirstOrDefault();
            return address.Address;
        }

        private static void ListIP()
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if (!(bool)objMO["ipEnabled"])
                    continue;



                Console.WriteLine(objMO["Caption"] + "," + 
                    objMO["ServiceName"] + "," + objMO["MACAddress"]);
                string[] ipaddresses = (string[])objMO["IPAddress"];
                string[] subnets = (string[])objMO["IPSubnet"];
                string[] gateways = (string[])objMO["DefaultIPGateway"];

                var i = 0;

                //Console.WriteLine("Printing Default Gateway Info:");
                ////string s = objMO["DefaultIPGateway"].ToString();
                //Console.WriteLine(gateways[0]);

                //Console.WriteLine("Printing IPGateway Info:");
                //foreach (string sGate in gateways)
                //    Console.WriteLine(sGate);


                //Console.WriteLine("Printing Ipaddress Info:");

                //foreach (string sIP in ipaddresses)
                //    Console.WriteLine(sIP);

                //Console.WriteLine("Printing SubNet Info:");

                //foreach (string sNet in subnets)
                //    Console.WriteLine(sNet);
            }
        }


        private static void DisableInternet()
        {
            var wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter " +
                                       "WHERE NetConnectionId != null " +
                                       "AND Manufacturer != 'Microsoft' ");
            using (var searcher = new ManagementObjectSearcher(wmiQuery))
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    if (((String)item["NetConnectionId"]) == "Local Area Connection")
                    {
                        using (item)
                        {
                            item.InvokeMethod("Disable", null);
                        }
                    }
                }
            }
        }

        private static void DisableInternetWMI()
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                Console.WriteLine(objMO["NetConnectionId"]);
                if ((bool)objMO["IPEnabled"])
                {
                    try
                    {
                        var disabled = objMO.InvokeMethod("Disable", null);
                    }
                    catch (Exception generatedExceptionName)
                    {
                        //throw;
                    }
                }


            }

        }

        private static void DisableInternetWMIAlternative()
        {
            SelectQuery query = new SelectQuery("Win32_NetworkAdapter", "NetConnectionStatus=2");
            ManagementObjectSearcher search = new ManagementObjectSearcher(query);
            foreach (ManagementObject result in search.Get())
            {
                NetworkAdapter adapter = new NetworkAdapter(result);
                // Identify the adapter you wish to disable here. 
                // In particular, check the AdapterType and 
                // Description properties.
                // Here, we're selecting the LAN adapters.
                if (adapter.AdapterType.Contains("Ethernet 802.3"))
                {
                    adapter.Disable();
                }
            }
        }

        private static void DisableInternetNetsh()
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(
                "netsh",
                "interface set interface name=\"Conexão local\" admin=enabled"
            );
            p.StartInfo = psi;
            p.Start();
        }
    }
}
    
    



