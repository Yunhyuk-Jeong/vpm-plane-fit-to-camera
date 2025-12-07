using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PlaneFitToCameraTool : EditorWindow
{
    private Camera targetCamera;

    //*  Multiple planes support
    private List<Transform> planeTransforms = new List<Transform>();

    //*  Options
    private bool alignPosition = true;
    private bool alignRotation = true;
    private bool useCurrentDistance = true;
    private float manualDistance = 1f;
    private bool keepSquare = false;

    //*  Extra in-plane rotation
    private enum ExtraRotation
    {
        Rot0,
        Rot90,
        Rot180,
        Rot270
    }

    private ExtraRotation extraRotation = ExtraRotation.Rot0;

    //*  Language selection (default: Korean)
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
        window.minSize = new Vector2(360, 320);
        window.Show();
    }

    private void OnGUI()
    {
        if (planeTransforms == null)
            planeTransforms = new List<Transform>();

        titleContent = new GUIContent(Tr("WindowTitle"));

        EditorGUILayout.LabelField(Tr("MainTitle"), EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        EditorGUILayout.HelpBox(Tr("HelpText"), MessageType.Info);
        EditorGUILayout.Space(5);

        //*  Camera
        targetCamera = (Camera)EditorGUILayout.ObjectField(
            Tr("CameraLabel"),
            targetCamera,
            typeof(Camera),
            true);

        EditorGUILayout.Space(5);

        //*  Plane list header
        EditorGUILayout.LabelField(Tr("PlaneLabel"), EditorStyles.boldLabel);

        //*  Plane list
        EditorGUILayout.BeginVertical("box");
        if (planeTransforms.Count == 0)
        {
            planeTransforms.Add(null);
        }

        for (int i = 0; i < planeTransforms.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            planeTransforms[i] = (Transform)EditorGUILayout.ObjectField(
                $"{Tr("PlaneSlotLabel")} {i + 1}",
                planeTransforms[i],
                typeof(Transform),
                true);

            if (GUILayout.Button("X", GUILayout.Width(25)))
            {
                planeTransforms.RemoveAt(i);
                i--;
                continue;
            }

            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(Tr("AddPlaneButton")))
        {
            planeTransforms.Add(null);
        }
        if (GUILayout.Button(Tr("ClearPlaneButton")))
        {
            planeTransforms.Clear();
        }
        EditorGUILayout.EndHorizontal();

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

        //*  Rotation dropdown
        EditorGUILayout.LabelField(Tr("RotationHeader"), EditorStyles.boldLabel);
        extraRotation = (ExtraRotation)EditorGUILayout.Popup(
            (int)extraRotation,
            GetRotationLabels()
        );

        EditorGUILayout.Space(10);

        bool hasAnyPlane = false;
        foreach (var t in planeTransforms)
        {
            if (t != null)
            {
                hasAnyPlane = true;
                break;
            }
        }

        using (new EditorGUI.DisabledScope(targetCamera == null || !hasAnyPlane))
        {
            if (GUILayout.Button(Tr("FitButton"), GUILayout.Height(30)))
            {
                FitAllPlanes();
            }
        }

        if (targetCamera == null || !hasAnyPlane)
        {
            EditorGUILayout.HelpBox(Tr("WarnMissingCameraPlane"), MessageType.Warning);
        }

        //*  Language selector (Header shows all 3 languages)
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Language / 언어 / 言語", EditorStyles.boldLabel);
        selectedLanguage = (Language)EditorGUILayout.EnumPopup(selectedLanguage);
    }

    //*  Localization
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
                return "Assign a Camera and one or more Plane objects, then press 'Fit Plane' to match\n" +
                       "their position/rotation/scale to the camera view.\n" +
                       "Designed for Unity built-in Plane (XZ, 10x10).\n" +
                       "Works with both Perspective and Orthographic cameras.";
            case "CameraLabel": return "Camera";
            case "PlaneLabel": return "Plane (Multiple)";
            case "PlaneSlotLabel": return "Plane";
            case "AddPlaneButton": return "Add Plane Slot";
            case "ClearPlaneButton": return "Clear All";
            case "OptionsHeader": return "Options";
            case "AlignPosition": return "Align Position";
            case "AlignRotation": return "Align Rotation";
            case "UseCurrentDistance": return "Use Current Distance";
            case "ManualDistance": return "Manual Distance";
            case "KeepSquare": return "Keep Square (Width = Height)";
            case "FitButton": return "Fit Plane(s)";
            case "WarnMissingCameraPlane": return "You must assign a Camera and at least one Plane.";
            case "RotationHeader": return "Image Rotation";
            case "Rotation0": return "0° (default)";
            case "Rotation90": return "90°";
            case "Rotation180": return "180°";
            case "Rotation270": return "270°";
            case "LogAligned": return "[Plane Fit To Camera] Plane(s) aligned to camera.";
            case "LogMissing": return "[Plane Fit To Camera] Camera or Plane(s) missing.";
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
                return "카메라와 하나 이상의 Plane을 지정한 뒤 'Fit Plane' 버튼을 누르면\n" +
                       "카메라 화면에 딱 맞도록 위치/회전/스케일이 자동 조정됩니다.\n" +
                       "Unity 기본 Plane(XZ, 10x10)에 맞춰 설계되었습니다.\n" +
                       "원근/직교(Orthographic) 카메라 모두 지원합니다.";
            case "CameraLabel": return "카메라";
            case "PlaneLabel": return "Plane (여러 개 지원)";
            case "PlaneSlotLabel": return "Plane";
            case "AddPlaneButton": return "Plane 슬롯 추가";
            case "ClearPlaneButton": return "모든 Plane 제거";
            case "OptionsHeader": return "옵션";
            case "AlignPosition": return "위치 맞추기";
            case "AlignRotation": return "회전 맞추기";
            case "UseCurrentDistance": return "현재 거리 사용";
            case "ManualDistance": return "수동 거리";
            case "KeepSquare": return "정사각형 유지 (가로=세로)";
            case "FitButton": return "Fit Plane(s)";
            case "WarnMissingCameraPlane": return "Camera와 최소 하나의 Plane을 지정해야 합니다.";
            case "RotationHeader": return "이미지 회전";
            case "Rotation0": return "0° (기본)";
            case "Rotation90": return "90°";
            case "Rotation180": return "180°";
            case "Rotation270": return "270°";
            case "LogAligned": return "[Plane Fit To Camera] Plane(들) 정렬 완료.";
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
                return "カメラと1つ以上のPlaneを指定して「Fit Plane」を押すと、\n" +
                       "カメラビューに合わせて自動で位置・回転・スケールを調整します。\n" +
                       "Unity標準のPlane(XZ, 10x10)向けに設計されています。\n" +
                       "パースペクティブ／オーソグラフィック両カメラに対応。";
            case "CameraLabel": return "カメラ";
            case "PlaneLabel": return "Plane (複数対応)";
            case "PlaneSlotLabel": return "Plane";
            case "AddPlaneButton": return "Planeスロットを追加";
            case "ClearPlaneButton": return "すべて削除";
            case "OptionsHeader": return "オプション";
            case "AlignPosition": return "位置を合わせる";
            case "AlignRotation": return "回転を合わせる";
            case "UseCurrentDistance": return "現在の距離を使用";
            case "ManualDistance": return "手動距離";
            case "KeepSquare": return "正方形に固定";
            case "FitButton": return "Fit Plane(s)";
            case "WarnMissingCameraPlane": return "Camera と少なくとも1つの Plane を指定してください。";
            case "RotationHeader": return "画像回転";
            case "Rotation0": return "0°（デフォルト）";
            case "Rotation90": return "90°";
            case "Rotation180": return "180°";
            case "Rotation270": return "270°";
            case "LogAligned": return "[Plane Fit To Camera] Plane がカメラに整列しました。";
            case "LogMissing": return "[Plane Fit To Camera] Camera または Plane がありません。";
        }
        return key;
    }

    //*  Mesh detect (Plane only)
    private enum MeshType { Unknown, Plane }

    private MeshType DetectMeshType(Transform plane, out float meshSize)
    {
        meshSize = 10f; //*  기본 Plane 가정

        var mf = plane != null ? plane.GetComponent<MeshFilter>() : null;
        if (mf == null || mf.sharedMesh == null)
            return MeshType.Unknown;

        string name = mf.sharedMesh.name.ToLower();
        if (name.Contains("plane"))
        {
            meshSize = 10f;
            return MeshType.Plane;
        }

        //*  이름이 달라도 Plane처럼 10x10으로 쓴다거나, 필요하면 여기서 변경 가능
        return MeshType.Unknown;
    }

    //*  Fit all planes
    private void FitAllPlanes()
    {
        if (targetCamera == null)
        {
            Debug.LogWarning(Tr("LogMissing"));
            return;
        }

        bool anyProcessed = false;

        foreach (var plane in planeTransforms)
        {
            if (plane == null) continue;

            FitPlane(plane);
            anyProcessed = true;
        }

        if (!anyProcessed)
        {
            Debug.LogWarning(Tr("LogMissing"));
        }
        else
        {
            Debug.Log(Tr("LogAligned"));
        }
    }

    //*  Main logic for a single plane
    private void FitPlane(Transform planeTransform)
    {
        if (targetCamera == null || planeTransform == null)
            return;

        Undo.RecordObject(planeTransform, "Fit Plane");

        //*  --- Distance from camera (used for positioning only) ---
        float distance;
        if (useCurrentDistance)
        {
            Vector3 camToPlane = planeTransform.position - targetCamera.transform.position;
            distance = Vector3.Dot(camToPlane, targetCamera.transform.forward);
            if (distance <= 0.001f) distance = Mathf.Max(0.01f, manualDistance);
        }
        else
        {
            distance = Mathf.Max(0.01f, manualDistance);
        }

        //*  --- Size in view space ---
        float width, height;

        if (targetCamera.orthographic)
        {
            //*  Orthographic: size is independent of distance
            height = 2f * targetCamera.orthographicSize;
            width = height * targetCamera.aspect;
        }
        else
        {
            //*  Perspective: size depends on distance and FOV
            height = 2f * distance * Mathf.Tan(targetCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            width = height * targetCamera.aspect;
        }

        if (keepSquare)
        {
            float max = Mathf.Max(width, height);
            width = height = max;
        }

        float meshSize;
        DetectMeshType(planeTransform, out meshSize); //*  Plane 기준, meshSize는 보정용

        //*  --- Scale (parent scale compensated, XZ 기준) ---
        Vector3 localScale = planeTransform.localScale;
        Vector3 parentScale = planeTransform.parent != null
            ? planeTransform.parent.lossyScale
            : Vector3.one;

        float parentX = Mathf.Abs(parentScale.x) < 1e-6f ? 1f : Mathf.Abs(parentScale.x);
        float parentZ = Mathf.Abs(parentScale.z) < 1e-6f ? 1f : Mathf.Abs(parentScale.z);

        float signX = Mathf.Sign(localScale.x);
        float signZ = Mathf.Sign(localScale.z);
        if (Mathf.Approximately(signX, 0f)) signX = 1f;
        if (Mathf.Approximately(signZ, 0f)) signZ = 1f;

        localScale.x = signX * (width / (meshSize * parentX));
        localScale.z = signZ * (height / (meshSize * parentZ));

        planeTransform.localScale = localScale;

        //*  --- Position ---
        if (alignPosition)
        {
            planeTransform.position = targetCamera.transform.position +
                                      targetCamera.transform.forward * distance;
        }

        //*  --- Rotation (XZ Plane 기준) ---
        if (alignRotation)
        {
            var camTr = targetCamera.transform;

            //*  Plane: XZ plane, local Y is normal
            Vector3 Yp = -camTr.forward;
            Vector3 Xp = camTr.right;
            Vector3 Zp = Vector3.Cross(Xp, Yp).normalized;

            Quaternion rot = Quaternion.LookRotation(Zp, Yp);

            //*  Flip so texture is not upside down (필요 없으면 이 줄 빼도 됨)
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
    }
}
