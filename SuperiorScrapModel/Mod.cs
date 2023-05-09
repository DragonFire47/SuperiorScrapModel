using PulsarModLoader;
using System.Reflection;
using UnityEngine;

namespace SuperiorScrapModel
{
    public class Mod : PulsarMod
    {
        static AssetBundle AssetBundle = null;
        public static GameObject ScrapAsset = null;

        public Mod()
        {
            AssetBundle = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("SuperiorScrapModel.scrapmodel.scrap"));
            ScrapAsset = AssetBundle.LoadAsset<GameObject>("Assets/PrefabInstance/waste.prefab");
        }

        public override string Version => "1.0.0";

        public override string Author => "Dragon, GrimWolf";

        public override string Name => "SuperiorScrapModel";

        public override string HarmonyIdentifier()
        {
            return $"Dragon.{Name}";
        }
    }
}
