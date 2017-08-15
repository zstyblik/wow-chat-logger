namespace ChatLogger
{
    public class ChatLogger : HBPlugin
    {
        public override string Author { get { return "zstyblik"; } }
        public override string Name { get { return "ChatLogger"; } }
        public override void OnButtonPress() { }
        public override Version Version { get { return new Version(0, 1); } }
        public override bool WantButton { get { return false; } }

        public override void OnEnable() {
            Chat.Say += Chat_Say;
            Chat.Yell += Chat_Yell;
            Chat.Whisper += Chat_Whisper;
            Chat.Party += Chat_Party;
            Chat.PartyLeader += Chat_Party;
            Chat.Guild += Chat_Guild;
            Chat.Emote += Chat_Emote;
            Chat.Battleground += Chat_BG;
            Chat.BattlegroundLeader += Chat_BG;
            Chat.Raid += Chat_Raid;
            Chat.RaidLeader += Chat_Raid;
            Chat.Officer += Chat_Officer;

            Lua.Events.AttachEvent("CHAT_MSG_BN_WHISPER", BNetWhisper);
            Lua.Events.AttachEvent("GMRESPONSE_RECEIVED", GMResponse);
        }

        public override void OnDisable()
        {
            Chat.Say -= Chat_Say;
            Chat.Yell -= Chat_Yell;
            Chat.Whisper -= Chat_Whisper;
            Chat.Party -= Chat_Party;
            Chat.PartyLeader -= Chat_Party;
            Chat.Guild -= Chat_Guild;
            Chat.Emote -= Chat_Emote;
            Chat.Battleground -= Chat_BG;
            Chat.BattlegroundLeader -= Chat_BG;
            Chat.Raid -= Chat_Raid;
            Chat.RaidLeader -= Chat_Raid;
            Chat.Officer -= Chat_Officer;

            Lua.Events.DetachEvent("CHAT_MSG_BN_WHISPER", BNetWhisper);
            Lua.Events.DetachEvent("GMRESPONSE_RECEIVED", GMResponse);
        }

        private void BNetWhisper(object sender, LuaEventArgs args)
        {
            if (((string)args.Args[0]).StartsWith("OQ,0Z,")) {
                return
            }
            //string friendname = Styx.WoWInternals.Lua.GetReturnVal<string>(string.Format("return BNGetFriendInfoByID({0})", author), 4);
            //string chat_from = string.Format("({0}){1}", author,friendname.ToString());
            PrintChatMessage((string)args.Args[0], (string)args.Args[12].ToString(), "BNet");
        }

        private void GMResponse(object sender, LuaEventArgs args)
        {
            PrintChatMessage((string)args.Args[0], (string)args.Args[1], args.ToString());
        }

        private void Chat_Officer(Chat.ChatLanguageSpecificEventArgs e)
        {
            PrintChatMessage(e.Message, e.Author, e.EventName);
        }
        private void Chat_Raid(Styx.CommonBot.Chat.ChatLanguageSpecificEventArgs e)
        {
            PrintChatMessage(e.Message, e.Author, e.EventName);
        }

        private void Chat_BG(Styx.CommonBot.Chat.ChatLanguageSpecificEventArgs e)
        {
            PrintChatMessage(e.Message, e.Author, e.EventName);
        }

        private void Chat_Emote(Styx.CommonBot.Chat.ChatAuthoredEventArgs e)
        {
            PrintChatMessage(e.Message, e.Author, e.EventName);
        }

        private void Chat_Party(Styx.CommonBot.Chat.ChatLanguageSpecificEventArgs e)
        {
            PrintChatMessage(e.Message, e.Author, e.EventName);
        }

        private void Chat_Whisper(Styx.CommonBot.Chat.ChatWhisperEventArgs e)
        {
            PrintChatMessage(e.Message, e.Author, e.EventName);
        }

        private void Chat_Yell(Styx.CommonBot.Chat.ChatLanguageSpecificEventArgs e)
        {
            PrintChatMessage(e.Message, e.Author, e.EventName);
        }

        private void Chat_Say(Styx.CommonBot.Chat.ChatLanguageSpecificEventArgs e)
        {
            PrintChatMessage(e.Message, e.Author, e.EventName);
        }

        private void Chat_Guild(Styx.CommonBot.Chat.ChatGuildEventArgs e)
        {
            PrintChatMessage(e.Message, e.Author, e.EventName);
        }

        private void PrintChatMessage(string message, string author = "", string type = "")
        {
            Logging.Write(Colors.Green,
                    string.Format("[@ChatLogger] Chat From: {0} Message: {1} Type: {2} EOM",
                        author, message, type));
        }
    }
}
