using System.Runtime.Serialization;

namespace BG_Lib
{
    public partial class BG_Switcher
    {
        [DataContract]
        class SwitcherState
        {
            [DataMember] public string BG_Source { get; set; }
            [DataMember] public int Change_Interval { get; set; }
            [DataMember] public int Elapsed { get; set; }
            [DataMember] public int Image_Index { get; set; }
            [DataMember] public int AutoSave_Interval { get; set; }
        }
    }
}
