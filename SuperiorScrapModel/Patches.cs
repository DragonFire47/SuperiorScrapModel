using HarmonyLib;
using System;
using UnityEngine;

namespace SuperiorScrapModel
{
    [HarmonyPatch(typeof(PLScrapCargo), MethodType.Constructor, new Type[] { typeof(int) })]
    internal class ScrapPatch
    {
        static void Postfix(PLScrapCargo __instance)
        {
            __instance.CargoVisualPrefabID = 10000;
        }
    }
    [HarmonyPatch(typeof(PLShipInfo), "SetupCOD")]
    class ShipSetupCODPatch
    {
        static bool Prefix(PLShipInfo __instance, CargoObjectDisplay cod, PLShipComponent shipComp, bool hidden)
        {
            if(shipComp.CargoVisualPrefabID != 10000)
            {
                return true;
            }
            if (shipComp != null)
            {
                AccessTools.Method(typeof(PLShipInfo), "TeleportNearbyPlayers").Invoke(__instance, new object[] { cod.RootObj.transform });
            }
            if (cod.DisplayedItem != shipComp)
            {
                UnityEngine.Object.Destroy(cod.DisplayObj);
                cod.DisplayObj = null;
            }
            cod.DisplayedItem = shipComp;
            cod.DisplayObj = UnityEngine.Object.Instantiate<GameObject>(Mod.ScrapAsset, cod.RootObj.transform.position, cod.RootObj.transform.rotation);
            cod.DisplayObj.layer = 11;
            foreach (object obj in cod.DisplayObj.transform)
            {
                ((Transform)obj).gameObject.layer = 11;
            }
            cod.DisplayObj.transform.parent = cod.RootObj.transform;
            cod.DisplayObj.transform.localPosition = Vector3.zero;
            cod.DisplayObj.transform.localScale = Vector3.one;
            cod.DisplayObj.transform.localRotation = Quaternion.identity;
            cod.DisplayObjRenderers = cod.DisplayObj.GetComponentsInChildren<Renderer>(true);
            cod.RootObjRenderer = cod.RootObj.GetComponent<Renderer>();
            return false;
        }
    }
}
