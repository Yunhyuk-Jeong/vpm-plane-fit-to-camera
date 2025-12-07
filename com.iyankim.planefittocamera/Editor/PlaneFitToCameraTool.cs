using UnityEngine;
using UnityEditor;

public class PlaneFitToCameraTool : EditorWindow
{
    private Camera targetCamera;
    private Transform planeTransform;

    //*  Options
    private bool alignPosition = true;
    private bool alignRotation = true;
    private bool useCurrentDistance = true;
    private float manualDistance = 1f;
    private bool keepSquare = false;

    //*  Language selection
    private enum Language
    {
        English,
        Korean,
        Japanese
    }

    private Language selectedLanguage = Language.English;

    [MenuItem("Iyan-Kim/Tools/Plane Fit To Camera")]
    public static void ShowWindow()
    {
        var window = GetWindow<PlaneFitToCameraTool>();
        window.titleContent = new GUIContent("Plane Fit To Camera");
        window.minSize = new Vector2(320, 260);
        window.Show();
    }

    private void OnGUI()
    {
        //*  Update window title per language
        titleContent = new GUIContent(Tr("WindowTitle"));

        EditorGUILayout.LabelField(Tr("MainTitle"), EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        EditorGUILayout.HelpBox(
            Tr("HelpText"),
            MessageType.Info);

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

        //*  Language selector at the bottom
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField(Tr("LanguageHeader"), EditorStyles.boldLabel);
        selectedLanguage = (Language)EditorGUILayout.EnumPopup(selectedLanguage);
    }

    //*  ----------------------------------------------------------------------
    //*  Localization
    //*  ----------------------------------------------------------------------

    private string Tr(string key)
    {
        switch (selectedLanguage)
        {
            case Language.Korean:
                return TrKorean(key);
            case Language.Japanese:
                return TrJapanese(key);
            default:
                return TrEnglish(key);
        }
    }

    private string TrEnglish(string key)
    {
        switch (key)
        {
            case "WindowTitle": return "Plane Fit To Camera";
            case "MainTitle": return "Plane Fit To Camera";
            case "HelpText":
                return "Assign a Camera and a Plane/Quad, then press 'Fit Plane' to match\n" +
                       "its position/rotation/scale to the camera view.\n" +
                       "Supports Unity built-in Plane (XZ) and Quad (XY).";
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
            case "LanguageHeader": return "Language";
            case "LogAligned": return "[Plane Fit To Camera] Plane/Quad aligned to camera view.";
            case "LogMissing": return "[Plane Fit To Camera] Camera or Plane/Quad is not assigned.";
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
                       "카메라 화면에 맞도록 위치/회전/스케일이 자동으로 맞춰집니다.\n" +
                       "Unity 기본 Plane(XZ), Quad(XY)를 지원합니다.";
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
            case "LanguageHeader": return "언어";
            case "LogAligned": return "[Plane Fit To Camera] Plane/Quad가 카메라 화면에 맞게 정렬되었습니다.";
            case "LogMissing": return "[Plane Fit To Camera] Camera 또는 Plane/Quad가 지정되지 않았습니다.";
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
                return "カメラとPlane/Quadを指定して「Fit Plane」ボタンを押すと、\n" +
                       "カメラビューに合わせて位置・回転・スケールが自動調整されます。\n" +
                       "Unity標準のPlane(XZ)とQuad(XY)に対応しています。";
            case "CameraLabel": return "カメラ";
            case "PlaneLabel": return "Plane / Quad";
            case "OptionsHeader": return "オプション";
            case "AlignPosition": return "位置を合わせる";
            case "AlignRotation": return "回転を合わせる";
            case "UseCurrentDistance": return "現在の距離を使用";
            case "ManualDistance": return "手動距離";
            case "KeepSquare": return "正方形を維持 (幅=高さ)";
            case "FitButton": return "Fit Plane";
            case "WarnMissingCameraPlane": return "Camera と Plane / Quad を両方指定してください。";
            case "LanguageHeader": return "言語";
            case "LogAligned": return "[Plane Fit To Camera] Plane/Quad がカメラビューに合わせて調整されました。";
            case "LogMissing": return "[Plane Fit To Camera] Camera または Plane/Quad が設定されていません。";
        }
        return key;
    }

    //*  ----------------------------------------------------------------------
    //*  Fitting logic
    //*  ----------------------------------------------------------------------

    private enum MeshType
    {
        Unknown,
        Plane,
        Quad
    }

    private MeshType DetectMeshType(out float meshSize)
    {
        meshSize = 1f;

        var mf = planeTransform != null ? planeTransform.GetComponent<MeshFilter>() : null;
        if (mf == null || mf.sharedMesh == null)
            return MeshType.Unknown;

        string meshName = mf.sharedMesh.name.ToLower();

        if (meshName.Contains("plane"))
        {
            meshSize = 10f; //*  Unity built-in Plane is 10x10 units
            return MeshType.Plane;
        }

        if (meshName.Contains("quad"))
        {
            meshSize = 1f;  //*  Unity built-in Quad is 1x1 unit
            return MeshType.Quad;
        }

        //*  Default: treat as 1x1 like a Quad
        meshSize = 1f;
        return MeshType.Unknown;
    }

    private void FitPlane()
    {
        if (targetCamera == null || planeTransform == null)
        {
            Debug.LogWarning(Tr("LogMissing"));
            return;
        }

        Undo.RecordObject(planeTransform, "Fit Plane To Camera");

        //*  --- Distance from camera ---
        float distance;
        if (useCurrentDistance)
        {
            Vector3 camToPlane = planeTransform.position - targetCamera.transform.position;
            distance = Vector3.Dot(camToPlane, targetCamera.transform.forward);

            //*  If behind the camera or too close, fall back to manual distance
            if (distance <= 0.001f)
                distance = Mathf.Max(0.01f, manualDistance);
        }
        else
        {
            distance = Mathf.Max(0.01f, manualDistance);
        }

        //*  --- Size based on FOV and distance (perspective camera) ---
        float height = 2f * distance * Mathf.Tan(targetCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float width = height * targetCamera.aspect;

        if (keepSquare)
        {
            float max = Mathf.Max(width, height);
            width = height = max;
        }

        //*  --- Mesh type detection ---
        float meshSize;
        MeshType type = DetectMeshType(out meshSize);

        //*  --- Scale ---
        Vector3 scale = planeTransform.localScale;

        if (type == MeshType.Plane)
        {
            //*  Plane: XZ plane (10x10)
            scale.x = width / meshSize;
            scale.z = height / meshSize;
        }
        else
        {
            //*  Quad / Unknown: XY plane (1x1)
            scale.x = width / meshSize;
            scale.y = height / meshSize;
        }

        planeTransform.localScale = scale;

        //*  --- Position ---
        if (alignPosition)
        {
            planeTransform.position =
                targetCamera.transform.position + targetCamera.transform.forward * distance;
        }

        //*  --- Rotation ---
        if (alignRotation)
        {
            var camTr = targetCamera.transform;
            Vector3 R = camTr.right.normalized;
            Vector3 U = camTr.up.normalized;
            Vector3 F = camTr.forward.normalized;

            Quaternion rot;

            if (type == MeshType.Plane)
            {
                //*  Plane: XZ plane, local Y is the normal
                //*  local Y -> -F (facing camera)
                //*  local X -> R (horizontal)
                Vector3 Yp = -F;
                Vector3 Xp = R;
                Vector3 Zp = Vector3.Cross(Xp, Yp).normalized;

                Yp.Normalize();
                Xp.Normalize();

                rot = Quaternion.LookRotation(Zp, Yp); //*  Z = forward, Y = up
            }
            else
            {
                //*  Quad / Unknown: XY plane, local Z is the normal
                //*  local Z -> -F (facing camera)
                //*  local Y -> U (up)
                rot = Quaternion.LookRotation(-F, U);
            }

            //*  Flip 180 degrees around local normal so the texture is not upside down
            rot *= Quaternion.Euler(0f, 180f, 0f);

            planeTransform.rotation = rot;
        }

        EditorUtility.SetDirty(planeTransform);
        Debug.Log(Tr("LogAligned"));
    }
}
