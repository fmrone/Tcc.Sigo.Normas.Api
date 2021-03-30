using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Tcc.Sigo.Normas.CrossCutting.Assemblies
{
    [ExcludeFromCodeCoverage]
    public class AssemblyUtil
    {
        public static IEnumerable<Assembly> GetCurrentAssemblies()
        {
            return new Assembly[]
            {
                Assembly.Load("Tcc.Sigo.Normas.Api"),
                Assembly.Load("Tcc.Sigo.Normas.Application"),
                Assembly.Load("Tcc.Sigo.Normas.Domain"),
                Assembly.Load("Tcc.Sigo.Normas.MomAdapter"),
                Assembly.Load("Tcc.Sigo.Normas.Repository"),
                Assembly.Load("Tcc.Sigo.Normas.CrossCutting")
            };
        }
    }
}
