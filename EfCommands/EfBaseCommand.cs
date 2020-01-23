using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfBaseCommand
    {
        protected readonly ChinookContext Context;

        public EfBaseCommand(ChinookContext context)
        {
            Context = context;
        }
    }
}
