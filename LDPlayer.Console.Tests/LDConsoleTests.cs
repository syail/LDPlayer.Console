using LDPlayer.Console.Dto;

namespace LDPlayer.Console.Tests
{
    [TestClass()]
    public class LDConsoleTests
    {
        LDConsole console = new("C:\\LDPlayer\\LDPlayer9\\ldconsole.exe");

        [TestMethod(), Priority(2)]
        public async Task GetListTest()
        {
            await console.GetList();
        }

        [TestMethod(), Priority(1)]
        public async Task RunAdbCommand()
        {
            var players = await console.GetList();
            LDPlayerInfo player = players.Find(x => x.Name == "zetatest");

            string result = await console.RunAdbCommand(player.Index, "shell echo 1");

            Assert.AreEqual("1", result.Trim());
        }
    }
}
