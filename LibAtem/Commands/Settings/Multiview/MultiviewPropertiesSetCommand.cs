using System;
using LibAtem.Common;
using LibAtem.Serialization;

namespace LibAtem.Commands.Settings.Multiview
{
    [CommandName("CMvP", 8)]
    public class MultiviewPropertiesSetCommand : SerializableCommandBase
    {
        [Flags]
        public enum MaskFlags
        {
            Layout = 1 << 0,
            SafeAreaEnabled = 1 << 1,
            ProgramPreviewSwapped = 1 << 2,
        }

        [Serialize(0), Enum8]
        public MaskFlags Mask { get; set; }
        [Serialize(1), UInt8]
        public uint MultiviewIndex { get; set; }
        [Serialize(2), Enum8]
        public MultiViewLayout Layout { get; set; }
        [Serialize(3), Bool]
        public bool SafeAreaEnabled { get; set; }
        [Serialize(4), Bool]
        public bool ProgramPreviewSwapped { get; set; }
    }
}