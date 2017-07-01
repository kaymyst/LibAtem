using System;
using LibAtem.Common;
using LibAtem.Serialization;

namespace LibAtem.Commands.MixEffects.Key
{
    [CommandName("CKMs", 12)]
    public class MixEffectKeyMaskSetCommand : SerializableCommandBase
    {
        [Flags]
        public enum MaskFlags
        {
            Enabled = 1 << 0,
            MaskTop = 1 << 1,
            MaskBottom = 1 << 2,
            MaskLeft = 1 << 3,
            MaskRight = 1 << 4,
        }

        [Serialize(0), Enum8]
        public MaskFlags Mask { get; set; }
        [Serialize(1), Enum8]
        public MixEffectBlockId MixEffectIndex { get; set; }
        [Serialize(2), UInt8]
        public uint KeyerIndex { get; set; }
        [Serialize(3), Bool]
        public bool MaskEnabled { get; set; }
        [Serialize(4), Int16D(1000, -9000, 9000)]
        public double MaskTop { get; set; }
        [Serialize(6), Int16D(1000, -9000, 9000)]
        public double MaskBottom { get; set; }
        [Serialize(8), Int16D(1000, -16000, 16000)]
        public double MaskLeft { get; set; }
        [Serialize(10), Int16D(1000, -16000, 16000)]
        public double MaskRight { get; set; }
    }
}