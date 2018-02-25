using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleTrip.Localization
{
    public class LocalizedStrings
    {
        private static readonly AppStrings _strings = new AppStrings();
        public AppStrings Strings => _strings;
    }
}
