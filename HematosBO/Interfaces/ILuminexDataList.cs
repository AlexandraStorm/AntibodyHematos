using System.Collections.Generic;

namespace HematosBO
{
    public interface ILuminexDataList
    {
        List<ILuminexData> LuminexDataList { get; set; }

        void LoadAssignments();
    }
}