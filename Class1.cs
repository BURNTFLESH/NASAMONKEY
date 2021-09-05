using System;
using System.Reflection;
using GorillaLocomotion;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice;
using System.Collections;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using BepInEx.Logging;
using UnityEngine;
using Unity.Collections;
using Unity.IO;
using UnityEngine.XR;
using System.Collections.Generic;
using System.IO;

namespace NASAMONKEY
{
    [BepInPlugin("org.BURTFLESH.monkeytag.LOWGRAV", "LOWGRAV", "0.0.0.1")]
    public class MyPatcher : BaseUnityPlugin
    {
        public void Awake()
        {
            var harmony = new Harmony("com.YourNameGoesHere.monkeytag.RightTriggerNoGrav");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("Update", MethodType.Normal)]
    public class Class1
    {
        static bool noGrav = false;
        static void Postfix(GorillaLocomotion.Player __instance)
        {
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller, list);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out noGrav);

            if (noGrav)
            {
                __instance.bodyCollider.attachedRigidbody.useGravity = false;
            }
            else
            {
                __instance.bodyCollider.attachedRigidbody.useGravity = true;
            }
            if (!PhotonNetwork.CurrentRoom.IsVisible || !PhotonNetwork.InRoom)
            {

            }
        }
    }
}