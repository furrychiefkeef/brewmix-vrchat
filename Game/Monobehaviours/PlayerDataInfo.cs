using Brewchat.Game.Wrappers;
using UnityEngine;
using VRC.SDKBase;

namespace brewchat.hybridxcore.bep.Game.Monobehaviours;

public class PlayerDataInfo : UnityEngine.MonoBehaviour
{
    public void Start()
    {
       
    }

    void LateUpdate()
    {
        /*var playerHeadBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.Head);
        var playerNeckBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.Neck);
        var playerChestBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.Chest);
        var playerSpineBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.Spine);
        var playerHipsBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.Hips);
        var playerLeftUpperArmBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.LeftUpperArm);
        var playerRightUpperArmBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.RightUpperArm);
        var playerLeftLowerArmBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        var playerRightLowerArmBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.RightLowerArm);
        var playerLeftHandBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.LeftHand);
        var playerRightHandBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.RightHand);
        var playerLeftUpperLegBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
        var playerRightUpperLegBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.RightUpperLeg);
        var playerLeftLowerLegBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
        var playerRightLowerLegBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.RightLowerLeg);
        var playerLeftFootBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.LeftFoot);
        var playerRightFootBone = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneTransform(HumanBodyBones.RightFoot);
        
        var playerHeadBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.Head);
        var playerNeckBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.Neck);
        var playerChestBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.Chest);
        var playerSpineBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.Spine);
        var playerHipsBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.Hips);
        var playerLeftUpperArmBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.LeftUpperArm);
        var playerRightUpperArmBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.RightUpperArm);
        var playerLeftLowerArmBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.LeftLowerArm);
        var playerRightLowerArmBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.RightLowerArm);
        var playerLeftHandBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.LeftHand);
        var playerRightHandBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.RightHand);
        var playerLeftUpperLegBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.LeftUpperLeg);
        var playerRightUpperLegBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.RightUpperLeg);
        var playerLeftLowerLegBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.LeftLowerLeg);
        var playerRightLowerLegBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.RightLowerLeg);
        var playerLeftFootBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.LeftFoot);
        var playerRightFootBoneR = GameUtils.PlayerModel.Get().VRCPlayerApi.GetBoneRotation(HumanBodyBones.RightFoot);
        
        var vrcPlayer = transform.GetComponent<VRC.Player>();
        var vrcPlayerApi = vrcPlayer.VRCPlayerApi;
        
       
        
        playerHeadBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.Head).localRotation;
        playerNeckBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.Neck).localRotation;
        playerChestBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.Chest).localRotation;
        playerSpineBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.Spine).localRotation;
        playerHipsBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.Hips).localRotation;
        playerLeftUpperArmBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.LeftUpperArm).localRotation;
        playerRightUpperArmBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.RightUpperArm).localRotation;
        playerLeftLowerArmBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.LeftLowerArm).localRotation;
        playerRightLowerArmBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.RightLowerArm).localRotation;
        playerLeftHandBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.LeftHand).localRotation;
        playerRightHandBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.RightHand).localRotation;
        playerLeftUpperLegBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.LeftUpperLeg).localRotation;
        playerRightUpperLegBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.RightUpperLeg).localRotation;
        playerLeftLowerLegBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.LeftLowerLeg).localRotation;
        playerRightLowerLegBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.RightLowerLeg).localRotation;
        playerLeftFootBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.LeftFoot).localRotation;
        playerRightFootBone.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.RightFoot).localRotation;
        
        playerHeadBoneR.= vrcPlayerApi.GetRotation(HumanBodyBones.Head);
        playerNeckBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.Neck).localRotation;
        playerChestBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.Chest).localRotation;
        playerSpineBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.Spine).localRotation;
        playerHipsBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.Hips).localRotation;
        playerLeftUpperArmBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.LeftUpperArm).localRotation;
        playerRightUpperArmBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.RightUpperArm).localRotation;
        playerLeftLowerArmBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.LeftLowerArm).localRotation;
        playerRightLowerArmBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.RightLowerArm).localRotation;
        playerLeftHandBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.LeftHand).localRotation;
        playerRightHandBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.RightHand).localRotation;
        playerLeftUpperLegBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.LeftUpperLeg).localRotation;
        playerRightUpperLegBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.RightUpperLeg).localRotation;
        playerLeftLowerLegBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.LeftLowerLeg).localRotation;
        playerRightLowerLegBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.RightLowerLeg).localRotation;
        playerLeftFootBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.LeftFoot).localRotation;
        playerRightFootBoneR.localRotation = vrcPlayerApi.GetBoneTransform(HumanBodyBones.RightFoot).localRotation;*/

    }   
}