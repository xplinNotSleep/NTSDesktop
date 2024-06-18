using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AG.COM.SDM.SystemUI
{
    public interface ICommandSubType
    {
        //
        // 摘要:
        //     The subtype of the command.
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetSubType([In] int SubType);

        //
        // 摘要:
        //     The number of commands defined with this CLSID.
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int GetCount();

    }
}
