using MakeItHighLight.Models;
using MakeItHighLight.ViewModels;
using System;
using System.Threading.Tasks;

namespace MakeItHighLight.Services
{
    internal class MappingService
    {
        internal static async Task<Settings> MapFromSettingVMToSettings(SettingsViewModel vm)
        {
            Settings settings = new Settings();
            settings.DestinationFolderPersistent = vm.DestinationFolderPersistent;
            settings.Shutdown = vm.Shutdown;
            settings.Genres = vm.Genres;
            settings.FadeOut = vm.FadeOut;
            settings.FadeIn = vm.FadeIn;
            settings.FadeinSecondsIn = vm.FadeInSecondsIn;
            settings.FadeinSecondsOut = vm.FadeInSecondsOut;
            settings.GenreRepl = vm.GenreRepl;
            settings.GenreReplStr = vm.GenreReplStr;
            return settings;
        }
    }
}