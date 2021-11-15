﻿using System;
using Exiled.API.Features;
using System.IO;
using static BetterMute.EventHandlers.ServerHandlers;

namespace BetterMute
{
    using EventHandlers;
    public class Plugin : Plugin<Config>
    {
    	public override string Author { get; } = "Killla";
		public override string Name { get; } = "Better Mute";
		public override string Prefix { get; } = "BetterMute";
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);

        public ServerHandlers ServerHandlers;


        public override void OnEnabled()
        {
            ServerHandlers = new ServerHandlers(this);
            Exiled.Events.Handlers.Server.RoundEnded += ServerHandlers.OnRoundEnded;
            Exiled.Events.Handlers.Player.Verified += ServerHandlers.OnVerified;
            path = Config.DataDir;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, "MuteList.txt");
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            MuteList = GetMuteList();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RoundEnded -= ServerHandlers.OnRoundEnded;
            Exiled.Events.Handlers.Player.Verified -= ServerHandlers.OnVerified;
            base.OnDisabled();
        }
    }
}