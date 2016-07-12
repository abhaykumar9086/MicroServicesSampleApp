using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Global;

namespace UserGateway
{
    public class Gateway : IGateway
    {
        IGateway proxy;

        public Gateway(String dllNamePath)
        {
            LoadIGhostAssembly(dllNamePath);
        }

        void LoadIGhostAssembly(String dllNamePath)
        {
            Assembly asm = Assembly.LoadFrom(dllNamePath);
            Type[] typesInAssembly = asm.GetTypes();

            foreach (Type t in typesInAssembly)
            {
                if (t.GetInterface(typeof(IGateway).FullName) != null)
                {
                    proxy = (IGateway)Activator.CreateInstance(t);
                }
            }
        }

        #region IGateway Members

        public bool ValidateUser(Userobj user, string url)
        {
            return proxy.ValidateUser(user, url);
        }

        #endregion
    }
}
