﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class IEProxy : Component
    {
        public bool Run(ref Report report, List<List<KeyValuePair<string, string>>> list)
        {
            string registryPath = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";

            RegistryKey keyHKCU = Registry.CurrentUser;
            RegistryKey key = keyHKCU.OpenSubKey(registryPath);

            string enabled = key.GetValue("ProxyEnable").ToString();

            if (enabled == "1")
            {
                list.Add(new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("token", "Prx"),
                    new KeyValuePair<string, string>("server", key.GetValue("ProxyServer").ToString()),
                });

                report.Add(list);
            }

            return true;
        }
    }
}