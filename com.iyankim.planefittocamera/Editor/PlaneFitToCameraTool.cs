using UnityEngine;
using UnityEditor;

public class PlaneFitToCameraTool : EditorWindow
{
    private Camera targetCamera;
    private Transform planeTransform;

    // Options
    private bool alignPosition = true;
    private bool alignRotation = true;
    private bool useCurrentDistance = true;
    private float manualDistance = 1f;
    private bool keepSquare = false;

    // Extra in-plane rotation
    private enum ExtraRotation
    {
        Rot0,
        Rot90,
        Rot180,
        Rot270
    }

    private ExtraRotation extraRotation = ExtraRotation.Rot0;

    // Language selection (default: Korean)
    private enum Language
    {
        English,
        Korean,
        Japanese
    }

    private Language selectedLanguage = Language.Korean;

    [MenuItem("Iyan-Kim/Tools/Plane Fit To Camera")]
    public static void ShowWindow()
    {
        var window = GetWindow<PlaneFitToCameraTool>();
        window.titleContent = new GUIContent("Plane Fit To Camera");
        window.minSize = new Vector2(320, 280);
        window.Show();
    }

    private void OnGUI()
    {
        titleContent = new GUIContent(Tr("WindowTitle"));

        EditorGUILayout.LabelField(Tr("MainTitle"), EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        EditorGUILayout.HelpBox(Tr("HelpText"), MessageType.Info);
        EditorGUILayout.Space(5);

        targetCamera = (Camera)EditorGUILayout.ObjectField(Tr("CameraLabel"), targetCamera, typeof(Camera), true);
        planeTransform = (Transform)EditorGUILayout.ObjectField(Tr("PlaneLabel"), planeTransform, typeof(Transform), true);

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField(Tr("OptionsHeader"), EditorStyles.boldLabel);

        alignPosition = EditorGUILayout.Toggle(Tr("AlignPosition"), alignPosition);
        alignRotation = EditorGUILayout.Toggle(Tr("AlignRotation"), alignRotation);

        useCurrentDistance = EditorGUILayout.Toggle(Tr("UseCurrentDistance"), useCurrentDistance);
        using (new EditorGUI.DisabledScope(useCurrentDistance))
        {
            manualDistance = EditorGUILayout.FloatField(Tr("ManualDistance"), manualDistance);
        }

        keepSquare = EditorGUILayout.Toggle(Tr("KeepSquare"), keepSquare);

        EditorGUILayout.Space(8);

        // Rotation dropdown
        EditorGUILayout.LabelField(Tr("RotationHeader"), EditorStyles.boldLabel);
        extraRotation = (ExtraRotation)EditorGUILayout.Popup(
            (int)extraRotation,
            GetRotationLabels()
        );

        EditorGUILayout.Space(10);

        using (new EditorGUI.DisabledScope(targetCamera == null || planeTransform == null))
        {
            if (GUILayout.Button(Tr("FitButton"), GUILayout.Height(30)))
            {
                FitPlane();
            }
        }

        if (targetCamera == null || planeTransform == null)
        {
            EditorGUILayout.HelpBox(Tr("WarnMissingCameraPlane"), MessageType.Warning);
        }

        // Language selector (Header shows all 3 languages)
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Language / 언어 / 言語", EditorStyles.boldLabel);
        selectedLanguage = (Language)EditorGUILayout.EnumPopup(selectedLanguage);
    }

    // Localization
    private string Tr(string key)
    {
        switch (selectedLanguage)
        {
            case Language.Korean: return TrKorean(key);
            case Language.Japanese: return TrJapanese(key);
            default: return TrEnglish(key);
        }
    }

    private string[] GetRotationLabels()
    {
        return new[]
        {
            Tr("Rotation0"),
            Tr("Rotation90"),
            Tr("Rotation180"),
            Tr("Rotation270")
        };
    }

    private string TrEnglish(string key)
    {
        switch (key)
        {
            case "WindowTitle": return "Plane Fit To Camera";
            case "MainTitle": return "Plane Fit To Camera";
            case "HelpText":
                return "Assign a Camera and Plane/Quad, then press 'Fit Plane' to match\n" +
                       "its position/rotation/scale to the camera view.\nSupports Unity Plane (XZ) and Quad (XY).";
            case "CameraLabel": return "Camera";
            case "PlaneLabel": return "Plane / Quad";
            case "OptionsHeader": return "Options";
            case "AlignPosition": return "Align Position";
            case "AlignRotation": return "Align Rotation";
            case "UseCurrentDistance": return "Use Current Distance";
            case "ManualDistance": return "Manual Distance";
            case "KeepSquare": return "Keep Square (Width = Height)";
            case "FitButton": return "Fit Plane";
            case "WarnMissingCameraPlane": return "You must assign both Camera and Plane / Quad.";
            case "RotationHeader": return "Image Rotation";
            case "Rotation0": return "0° (default)";
            case "Rotation90": return "90°";
            case "Rotation180": return "180°";
            case "Rotation270": return "270°";
            case "LogAligned": return "[Plane Fit To Camera] Plane aligned to camera.";
            case "LogMissing": return "[Plane Fit To Camera] Camera or Plane missing.";
        }
        return key;
    }

    private string TrKorean(string key)
    {
        switch (key)
        {
            case "WindowTitle": return "Plane Fit To Camera";
            case "MainTitle": return "Plane Fit To Camera";
            case "HelpText":
                return "카메라와 Plane/Quad를 지정한 뒤 'Fit Plane' 버튼을 누르면\n" +
                       "카메라 화면에 딱 맞도록 위치/회전/스케일이 자동 조정됩니다.\nUnity 기본 Plane(XZ), Quad(XY) 지원.";
            case "CameraLabel": return "카메라";
            case "PlaneLabel": return "Plane / Quad";
            case "OptionsHeader": return "옵션";
            case "AlignPosition": return "위치 맞추기";
            case "AlignRotation": return "회전 맞추기";
            case "UseCurrentDistance": return "현재 거리 사용";
            case "ManualDistance": return "수동 거리";
            case "KeepSquare": return "정사각형 유지 (가로=세로)";
            case "FitButton": return "Fit Plane";
            case "WarnMissingCameraPlane": return "Camera와 Plane / Quad를 모두 지정해야 합니다.";
            case "RotationHeader": return "이미지 회전";
            case "Rotation0": return "0° (기본)";
            case "Rotation90": return "90°";
            case "Rotation180": return "180°";
            case "Rotation270": return "270°";
            case "LogAligned": return "[Plane Fit To Camera] 정렬 완료.";
            case "LogMissing": return "[Plane Fit To Camera] Camera 또는 Plane이 없습니다.";
        }
        return key;
    }

    private string TrJapanese(string key)
    {
        switch (key)
        {
            case "WindowTitle": return "Plane Fit To Camera";
            case "MainTitle": return "Plane Fit To Camera";
            case "HelpText":
                return "カメラとPlane/Quadを指定して「Fit Plane」を押すと、\n" +
                       "カメラビューに合わせて自動で位置・回転・スケールを調整します。";
            case "CameraLabel": return "カメラ";
            case "PlaneLabel": return "Plane / Quad";
            case "OptionsHeader": return "オプション";
            case "AlignPosition": return "位置を合わせる";
            case "AlignRotation": return "回転を合わせる";
            case "UseCurrentDistance": return "現在の距離を使用";
            case "ManualDistance": return "手動距離";
            case "KeepSquare": return "正方形に固定";
            case "FitButton": return "Fit Plane";
            case "WarnMissingCameraPlane": return "Camera と Plane / Quad を指定してください。";
            case "RotationHeader": return "画像回転";
            case "Rotation0": return "0°（デフォルト）";
            case "Rotation90": return "90°";
            case "Rotation180": return "180°";
            case "Rotation270": return "270°";
            case "LogAligned": return "[Plane Fit To Camera] 整列しました。";
            case "LogMissing": return "[Plane Fit To Camera] Camera または Plane がありません。";
        }
        return key;
    }

    // Mesh detect
    private enum MeshType { Unknown, Plane, Quad }

    private MeshType DetectMeshType(out float meshSize)
    {
        meshSize = 1f;

        var mf = planeTransform?.GetComponent<MeshFilter>();
        if (mf == null || mf.sharedMesh == null)
            return MeshType.Unknown;

        string name = mf.sharedMesh.name.ToLower();

        if (name.Contains("plane"))
        {
            meshSize = 10f;
            return MeshType.Plane;
        }
        if (name.Contains("quad"))
        {
            meshSize = 1f;
            return MeshType.Quad;
        }

        return MeshType.Unknown;
    }

    // Main logic
    private void FitPlane()
    {
        if (targetCamera == null || planeTransform == null)
        {
            Debug.LogWarning(Tr("LogMissing"));
            return;
        }

        Undo.RecordObject(planeTransform, "Fit Plane");

        float distance;
        if (useCurrentDistance)
        {
            Vector3 camToPlane = planeTransform.position - targetCamera.transform.position;
            distance = Vector3.Dot(camToPlane, targetCamera.transform.forward);
            if (distance <= 0.001f) distance = Mathf.Max(0.01f, manualDistance);
        }
        else distance = Mathf.Max(0.01f, manualDistance);

        float height = 2f * distance * Mathf.Tan(targetCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float width = height * targetCamera.aspect;

        if (keepSquare)
        {
            float max = Mathf.Max(width, height);
            width = height = max;
        }

        float meshSize;
        MeshType type = DetectMeshType(out meshSize);

        Vector3 scale = planeTransform.localScale;

        if (type == MeshType.Plane)
        {
            scale.x = width / meshSize;
            scale.z = height / meshSize;
        }
        else
        {
            scale.x = width / meshSize;
            scale.y = height / meshSize;
        }

        planeTransform.localScale = scale;

        if (alignPosition)
        {
            planeTransform.position = targetCamera.transform.position +
                                      targetCamera.transform.forward * distance;
        }

        if (alignRotation)
        {
            var camTr = targetCamera.transform;

            Quaternion rot;
            if (type == MeshType.Plane)
            {
                Vector3 Yp = -camTr.forward;
                Vector3 Xp = camTr.right;
                Vector3 Zp = Vector3.Cross(Xp, Yp).normalized;
                rot = Quaternion.LookRotation(Zp, Yp);
            }
            else
            {
                rot = Quaternion.LookRotation(-camTr.forward, camTr.up);
            }

            rot *= Quaternion.Euler(0, 180, 0);

            float angle = extraRotation switch
            {
                ExtraRotation.Rot90 => 90f,
                ExtraRotation.Rot180 => 180f,
                ExtraRotation.Rot270 => 270f,
                _ => 0f
            };

            if (angle != 0f)
                rot = Quaternion.AngleAxis(angle, camTr.forward) * rot;

            planeTransform.rotation = rot;
        }

        EditorUtility.SetDirty(planeTransform);
        Debug.Log(Tr("LogAligned"));
    }
}
