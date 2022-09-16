using FullyEquipment.UI;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using FullyEquipment.Helper;
using Il2CppSystem.Collections.Generic;
using UnityEngine.SceneManagement;

namespace FullyEquipment
{
    public static class BuildInfo
    {
        public const string Name = "BasicWeaponEquipment";
        public const string Description = "This mod will force command villager to seek weapon and armor";
        public const string Author = "donimuzur"; // Author of the Mod.  (MUST BE SET)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)    
    }
    public class FullyEquipment : MelonMod
    {
        public bool isFinish; 
        public GameManager gameManager = null;
        public bool isVillagerModExist = false;
        public InputManager inputManager = null;
        public GameObject selectedBuilding = null;
        public override void OnApplicationStart()
        {
            MelonLogger.Msg("BasicWeaponEquipment Started");
            var getModList = MelonHandler.Mods;
            foreach(var mod in getModList)
            {
                if(mod.Info.Name == "VillagerSpawnerMod")
                {
                    isVillagerModExist = true;
                }
            }
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            isFinish = false;
        }
        public override void OnUpdate()
        {
            var gameManagerObj = GameObject.Find("GameManager");
            if (gameManagerObj != null)
            {
                inputManager = gameManagerObj.GetComponent<InputManager>();
                selectedBuilding = inputManager.selectedObject;
                if (selectedBuilding != null && selectedBuilding.tag == "TownCenter" && !isFinish)
                {
                    GameObject.Destroy(GameObject.Find("ForceEquipWeaponButton"));
                    var getUIpausWindowList = Resources.FindObjectsOfTypeAll(Il2CppType.From(typeof(UIPauseWindow)));

                    if (getUIpausWindowList != null && getUIpausWindowList.Count > 0)
                    {
                        var getUIpausWindow = getUIpausWindowList[0].TryCast<UIPauseWindow>();
                        var getResumeButton = getUIpausWindow.gameObject.transform.FindChild("Pivot").FindChild("Main Panel").FindChild("Button_Resume");
                        if (getResumeButton != null)
                        {
                            var getUITownCenterOverview = GameObject.FindObjectOfType<UITownCenterOverview>();
                            if (getUITownCenterOverview != null)
                            {
                                Sprite btnSprite = getResumeButton.GetComponent<Image>().sprite;
                                GameObject uiButton = UIControls.CreateButton(new UIControls.Resources { standard = btnSprite });
                                uiButton.name = "ForceEquipWeaponButton";
                                GameObject gameObject3 = new GameObject("ForceEquipWeaponButtonIcon1");
                                var component31 = gameObject3.AddComponent<RectTransform>();

                                var component32 = gameObject3.AddComponent<Image>();
                                component32.sprite = GlobalAssets.uiAssetMap.villagerIcon;
                                component32.preserveAspect = true;
                                gameObject3.transform.SetParent(uiButton.transform, false);

                                var buttonActionuiButton = uiButton.GetComponent<Button>();
                                buttonActionuiButton.onClick.AddListener(delegate
                                {
                                    EquipBasicWeapon();
                                });

                                GameObject gameObject2 = new GameObject("ForceEquipWeaponButtonIcon2");
                                var component21 = gameObject2.AddComponent<RectTransform>();

                                var component22 = gameObject2.AddComponent<Image>();
                                component22.sprite = GlobalAssets.uiAssetMap.weaponIcon;
                                component22.preserveAspect = true;
                                gameObject2.transform.SetParent(uiButton.transform, false);

                                var component3 = uiButton.AddComponent<HorizontalLayoutGroup>();
                                component3.padding.top = 10;
                                component3.padding.bottom = 5;
                                component3.padding.left = 20;
                                component3.padding.right = 20;

                                var uiButtonlayoutElement = uiButton.AddComponent<LayoutElement>();
                                uiButtonlayoutElement.ignoreLayout = true;

                                var uiButtonContentSizeFitter = uiButton.AddComponent<ContentSizeFitter>();
                                uiButtonlayoutElement.ignoreLayout = true;

                                uiButton.transform.SetParent(getUITownCenterOverview.gameObject.transform.FindChild("TownProgression").FindChild("TownCenterProgression").gameObject.transform, false);
                                var uiButtonRectTransofrm = uiButton.GetComponent<RectTransform>();
                                uiButtonRectTransofrm.sizeDelta = new Vector2(80, 35);

                                uiButtonRectTransofrm.localPosition = new Vector3(400, -70, 0);
                                if (isVillagerModExist)
                                {
                                    uiButtonRectTransofrm.localPosition = new Vector3(400, -30, 0);
                                }
                                isFinish = true;
                            }
                        }
                    }
                }
                else if (selectedBuilding == null)
                {
                    isFinish = false;
                }
            }
        }
        void EquipBasicWeapon()
        {
            var villagerList = GameObject.FindObjectsOfType<Villager>();
            foreach(var villager in villagerList)
            {
                var getUnusedItemsList = villager.occupation.unusedItemsToFree;
                getUnusedItemsList.Contains(new ItemWeapon());
                
                var listToRemove = new List<Item>();
                foreach (var item in getUnusedItemsList)
                {
                    if (item.itemID == ItemID.Weapon || item.itemID == ItemID.Bow || item.itemID == ItemID.Crossbow || item.itemID == ItemID.Arrow)
                    {
                        listToRemove.Add(item);
                    }
                }

                foreach (var toRemove in listToRemove)
                {
                    villager.occupation.unusedItemsToFree.Remove(toRemove);
                }

                var seekItemList1 = new List<SeekItemEntry>();
                seekItemList1.Add(new SeekItemEntry(ItemID.Weapon, 1, 0));
                var seekItemGroup1 = new SeekItemGroup() { interaction = SeekItemGroup.GroupInteraction.Upgrade, entries = seekItemList1 };

                var seekItemList2 = new List<SeekItemEntry>();
                seekItemList2.Add(new SeekItemEntry(ItemID.Bow, 1, 0));
                seekItemList2.Add(new SeekItemEntry(ItemID.Crossbow, 1, 0));
                var seekItemGroup2 = new SeekItemGroup() { interaction = SeekItemGroup.GroupInteraction.Upgrade, entries = seekItemList2 };

                var seekItemList3 = new List<SeekItemEntry>();
                seekItemList3.Add(new SeekItemEntry(ItemID.Arrow, 20, 0));
                var seekItemGroup3 = new SeekItemGroup() { interaction = SeekItemGroup.GroupInteraction.RequireAll, entries = seekItemList3 };

                var seekItemGroupList = new List<SeekItemGroup>();
                seekItemGroupList.Add(seekItemGroup1);
                seekItemGroupList.Add(seekItemGroup2);
                seekItemGroupList.Add(seekItemGroup3);

                villager.itemRequester.SetItemCriteriaToSeek(seekItemGroupList);
            }
        }
    }
}
