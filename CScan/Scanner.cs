﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CScan.Components;

namespace CScan
{
    class Scanner
    {
        protected string[] components = {
            "Header",
            "Processes",
            "Drivers",
            "RegistryRun",
        };

        protected List<Component> initializedComponents = new List<Component>();

        public Scanner()
        {
            InitializeComponents();
        }

        public void Scan(ref System.Windows.Forms.TextBox status)
        {
            Report report = new Report();

            foreach (Component component in initializedComponents)
            {
                status.Text = "Running " + component.GetType().Name + "...";

                component.Run(ref report, new List<List<KeyValuePair<string, string>>>());
            }

            string path = report.WriteToFile();

            Process.Start("notepad.exe", path);

            status.Text = "Success!";
        }

        protected void InitializeComponents()
        {
            foreach (string component in components)
            {
                Type t = Type.GetType("CScan.Components." + component);

                Component initializedComponent = (Component) Activator.CreateInstance(t);

                initializedComponents.Add(initializedComponent);
            }
        }
    }
}
