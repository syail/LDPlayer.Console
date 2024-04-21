namespace LDConsoleCLI.Dto
{
    public class LDPlayerInfo
    {
        public int Index { get; set; }
        public required string Name { get; set; }
        public int BaseProcessHandle { get; set; }
        public int RendererProcessHandle { get; set; }
        public LDPlayerState State { get; set; }

        /// <summary>
        /// LDPlayer frame (dnplayer.exe) pid
        /// </summary>
        public int BoxerPID { get; set; }

        /// <summary>
        /// VirtualBox VM pid
        /// </summary>
        public int VirtualBoxPID { get; set; }

        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public int DPI { get; set; }
        
        public enum LDPlayerState
        {
            Stopped = 0,
            Running,
            Starting,
        }
    }
}
