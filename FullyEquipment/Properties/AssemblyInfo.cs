using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(FullyEquipment.BuildInfo.Description)]
[assembly: AssemblyDescription(FullyEquipment.BuildInfo.Description)]
[assembly: AssemblyCompany(FullyEquipment.BuildInfo.Company)]
[assembly: AssemblyProduct(FullyEquipment.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + FullyEquipment.BuildInfo.Author)]
[assembly: AssemblyTrademark(FullyEquipment.BuildInfo.Company)]
[assembly: AssemblyVersion(FullyEquipment.BuildInfo.Version)]
[assembly: AssemblyFileVersion(FullyEquipment.BuildInfo.Version)]
[assembly: MelonInfo(typeof(FullyEquipment.FullyEquipment), FullyEquipment.BuildInfo.Name, FullyEquipment.BuildInfo.Version, FullyEquipment.BuildInfo.Author, FullyEquipment.BuildInfo.DownloadLink)]
[assembly: MelonColor()]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]