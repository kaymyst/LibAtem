﻿using LibAtem.Common;

namespace LibAtem.Commands
{
    [CommandName("AuxS")]
    public class AuxSourceGetCommand : ICommand
    {
        public uint Id { get; set; }
        public VideoSource Source { get; set; }

        public void Serialize(CommandBuilder cmd)
        {
            cmd.AddUInt8(Id);
            cmd.Pad();
            cmd.AddVideoSource(Source);
        }

        public void Deserialize(ParsedCommand cmd)
        {
            Id = cmd.GetUInt8();
            cmd.Skip();
            Source = cmd.GetVideoSource();
        }
    }
}