namespace Believer_Loader
{
    #region

    using System;
    using System.IO;
    using System.Reflection;
	
    using Aimtec.SDK.Events;

    #endregion

    public static class MyLoader
    {
        public static void Main()
        {
            GameEvents.GameStart += () =>
            {
                var loader = new Loader();
            };
        }

        private class Loader
        {
            public Loader()
            {
                var link = Environment.GetEnvironmentVariable("LocalAppData");
                var filePath = link + @"\AimtecLoader\Data\System\BelieverCore.dll";

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                var prdll = Properties.Resources.Believer;
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    fs.Write(prdll, 0, prdll.Length);
                }

                var a = Assembly.LoadFrom(filePath);
                var myType = a.GetType("Believer.BelieverLoader");
                var main = myType.GetMethod("Main", BindingFlags.Public | BindingFlags.Static);

                if (main != null)
                {
                    main.Invoke(null, null);
                }
                else
                {
                    Console.WriteLine(@"Error in Core Files, Please Clean the DLL Data, find it in AimtecLoader\Data\System\BelieverCore.dll");
                }
            }
        }
    }
}
