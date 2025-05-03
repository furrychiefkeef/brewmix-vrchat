using Brewchat;
using Brewchat.Game.Wrappers;
using UnityEngine;
using VRC;
using System;
using System.Collections.Generic;
using Syrup.IMGUI;


internal class wallhacks : MonoBehaviour
{
    public GameObject canvas_nameplate { get; set; }
    public PlayerNameplate _nameplate { get; set; }
    public  Player _player { get; set; }
    private SkinnedMeshRenderer[] _avatarender;
    private SkinnedMeshRenderer _meshs;
 
    private void Awake()
    {
        enabled = Config.Render.player_wallhack;
    }

    void Start()
    {

        _player = transform.GetComponent<VRC.Player>();
        _avatarender = _player.GetComponentsInChildren<SkinnedMeshRenderer>(false);
        if (_avatarender == null) return;
        foreach (SkinnedMeshRenderer meshs in _avatarender)
            meshs.gameObject.layer = GameUtils.Gameplay.RenderLayers.InternalUI;

        if (_nameplate == null) return;
        _nameplate = _player._vrcplayer.field_Public_PlayerNameplate_0;
        _nameplate.field_Public_CanvasGroup_0.gameObject.layer = GameUtils.Gameplay.RenderLayers.InternalUI;
    }
}

internal class esp : MonoBehaviour
{
    public Player _player { get; set; }
    private SkinnedMeshRenderer[] _avatarender;
    private HighlightsFXStandalone FX;
    private void Start()
    {
        _player = transform.GetComponent<VRC.Player>();
        GetSelectRegion(_player);
    }

    void GetSelectRegion(VRC.Player player)
    {
        var obj = Instantiate(_player.transform.FindChild("SelectRegion"), player.transform);
        UnityEngine.Object.DontDestroyOnLoad(obj.GetComponent<PlayerSelector>());
        FX = FindObjectOfType<HighlightsFXStandalone>();
        FX.highlightColor = Color.red;
        FX.field_Protected_HashSet_1_MeshFilter_0.AddIfNotPresent(obj.GetComponent<MeshFilter>());
        FX.blurSize = 1f;
        obj.GetComponent<MeshRenderer>().enabled = true;
  
    }

   
}



    












/*class skeletonesp : MonoBehaviour
{
    private Player _player;
    public Color Theme = Config.Appearance.Theme;
    private void Start()
    {
        _player = transform.GetComponent<VRC.Player>();
    }

    void OnGUI()
    {
        Camera? cam = GameUtils.Camera.Get;
        if (cam == null) return;

        Dictionary<HumanBodyBones, Vector3> bonePositions = new Dictionary<HumanBodyBones, Vector3>();
        Dictionary<HumanBodyBones, Vector2> boneScreenPositions = new Dictionary<HumanBodyBones, Vector2>();

        // Get all bone positions
        foreach (HumanBodyBones bone in Enum.GetValues(typeof(HumanBodyBones)))
        {
            // Skip LastBone as it's just an enum marker
            if (bone == HumanBodyBones.LastBone) continue;

            try
            {
                Vector3 pos = _player.VRCPlayerApi.GetBonePosition(bone);

                // Skip invalid positions
                if (pos == Vector3.zero) continue;

                bonePositions[bone] = pos;

                // Convert to screen space
                Vector3 screenPos = cam.WorldToScreenPoint(pos);

                // Skip if behind camera
                if (screenPos.z <= 0) continue;

                // Invert Y for screen coordinates
                screenPos.y = Screen.height - screenPos.y;
                boneScreenPositions[bone] = new Vector2(screenPos.x, screenPos.y);
            }
            catch
            {
                // Some bones might not exist on all avatars
                continue;
            }
        }

        // Draw head circle if the head bone exists
        if (boneScreenPositions.ContainsKey(HumanBodyBones.Head))
        {
            // Adjust radius based on distance between head and neck if available
            float headRadius = 3f; // Default radius
            if (boneScreenPositions.ContainsKey(HumanBodyBones.Neck))
            {
                headRadius = Vector2.Distance(boneScreenPositions[HumanBodyBones.Head], 
                                             boneScreenPositions[HumanBodyBones.Neck]) * 2f;
            }
            DrawCircle(boneScreenPositions[HumanBodyBones.Head], headRadius, Color.green, 3f);
        }

        // Define connections between bones to form a skeleton
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.Head, HumanBodyBones.Neck, Color.green);
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.Neck, HumanBodyBones.Chest, Color.green);
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.Chest, HumanBodyBones.Spine, Color.green);
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.Spine, HumanBodyBones.Hips, Color.green);

        // Arms
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.Chest, HumanBodyBones.LeftShoulder, Color.blue);
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.LeftShoulder, HumanBodyBones.LeftUpperArm, Color.blue);
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.LeftUpperArm, HumanBodyBones.LeftLowerArm, Color.blue);
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.LeftLowerArm, HumanBodyBones.LeftHand, Color.blue);

        DrawBoneConnection(boneScreenPositions, HumanBodyBones.Chest, HumanBodyBones.RightShoulder, Color.red);
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.RightShoulder, HumanBodyBones.RightUpperArm, Color.red);
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.RightUpperArm, HumanBodyBones.RightLowerArm, Color.red);
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.RightLowerArm, HumanBodyBones.RightHand, Color.red);

        // Legs
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.Hips, HumanBodyBones.LeftUpperLeg, Color.blue);
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.LeftUpperLeg, HumanBodyBones.LeftLowerLeg, Color.blue);
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.LeftLowerLeg, HumanBodyBones.LeftFoot, Color.blue);
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.LeftFoot, HumanBodyBones.LeftToes, Color.blue);

        DrawBoneConnection(boneScreenPositions, HumanBodyBones.Hips, HumanBodyBones.RightUpperLeg, Color.red);
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.RightUpperLeg, HumanBodyBones.RightLowerLeg, Color.red);
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.RightLowerLeg, HumanBodyBones.RightFoot, Color.red);
        DrawBoneConnection(boneScreenPositions, HumanBodyBones.RightFoot, HumanBodyBones.RightToes, Color.red);

        // Fingers - Left Hand
        DrawFingers(boneScreenPositions, true, Color.cyan);

        // Fingers - Right Hand
        DrawFingers(boneScreenPositions, false, Color.yellow);

        // Draw bone points
        foreach (var bonePos in boneScreenPositions)
        {
            DrawPoint(bonePos.Value, Color.white, 3f);
        }
    }

    void DrawBoneConnection(Dictionary<HumanBodyBones, Vector2> positions, HumanBodyBones bone1, HumanBodyBones bone2,
        Color color)
    {
        if (positions.ContainsKey(bone1) && positions.ContainsKey(bone2))
        {
            DrawLine(positions[bone1], positions[bone2], color, 3f);
        }
    }

    void DrawFingers(Dictionary<HumanBodyBones, Vector2> positions, bool isLeft, Color color)
    {
        string side = isLeft ? "Left" : "Right";

        // Thumb
        DrawBoneConnection(positions,
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "Hand"),
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "ThumbProximal"),
            color);
        DrawBoneConnection(positions,
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "ThumbProximal"),
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "ThumbIntermediate"),
            color);
        DrawBoneConnection(positions,
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "ThumbIntermediate"),
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "ThumbDistal"),
            color);

        // Index finger
        DrawBoneConnection(positions,
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "Hand"),
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "IndexProximal"),
            color);
        DrawBoneConnection(positions,
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "IndexProximal"),
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "IndexIntermediate"),
            color);
        DrawBoneConnection(positions,
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "IndexIntermediate"),
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "IndexDistal"),
            color);

        // Middle finger
        DrawBoneConnection(positions,
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "Hand"),
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "MiddleProximal"),
            color);
        DrawBoneConnection(positions,
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "MiddleProximal"),
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "MiddleIntermediate"),
            color);
        DrawBoneConnection(positions,
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "MiddleIntermediate"),
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "MiddleDistal"),
            color);

        // Ring finger
        DrawBoneConnection(positions,
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "Hand"),
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "RingProximal"),
            color);
        DrawBoneConnection(positions,
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "RingProximal"),
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "RingIntermediate"),
            color);
        DrawBoneConnection(positions,
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "RingIntermediate"),
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "RingDistal"),
            color);

        // Little finger
        DrawBoneConnection(positions,
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "Hand"),
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "LittleProximal"),
            color);
        DrawBoneConnection(positions,
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "LittleProximal"),
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "LittleIntermediate"),
            color);
        DrawBoneConnection(positions,
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "LittleIntermediate"),
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), side + "LittleDistal"),
            color);
    }

    void DrawPoint(Vector2 position, Color color, float size)
    {
        GUI.color = color;
        GUI.DrawTexture(new Rect(position.x - size / 2, position.y - size / 2, size, size), Texture2D.whiteTexture);
    }

    void DrawLine(Vector2 start, Vector2 end, Color color, float thickness)
    {
        // Save the current GUI color
        Color originalColor = GUI.color;

        // Set the color for drawing
        GUI.color = color;

        // Calculate the direction and length of the line
        Vector2 direction = end - start;
        float length = direction.magnitude;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Create a rect for the line
        GUIUtility.RotateAroundPivot(angle, start);
        GUI.DrawTexture(new Rect(start.x, start.y - thickness / 2, length, thickness), Texture2D.whiteTexture);
        GUIUtility.RotateAroundPivot(-angle, start);

        // Restore the original GUI color
        GUI.color = originalColor;
    }

    // New method to draw a circle
    void DrawCircle(Vector2 center, float radius, Color color, float thickness)
    {
        // Save the current GUI color
        Color originalColor = GUI.color;
        
        // Set the color for drawing
        GUI.color = color;
        
        // Number of segments to make the circle (more segments = smoother circle)
        int segments = 32;
        
        // Draw the circle as a series of lines
        float angleStep = 2f * Mathf.PI / segments;
        
        for (int i = 0; i < segments; i++)
        {
            float angle1 = i * angleStep;
            float angle2 = (i + 1) * angleStep;
            
            Vector2 point1 = new Vector2(
                center.x + radius * Mathf.Cos(angle1),
                center.y + radius * Mathf.Sin(angle1)
            );
            
            Vector2 point2 = new Vector2(
                center.x + radius * Mathf.Cos(angle2),
                center.y + radius * Mathf.Sin(angle2)
            );
            
            DrawLine(point1, point2, color, thickness);
        }
        
        // Restore the original GUI color
        GUI.color = originalColor;
    }
}*/