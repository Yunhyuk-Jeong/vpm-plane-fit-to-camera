# Plane Fit To Camera Tool

**Version 1.0.3**

---

## 📖 English

### Overview

A Unity Editor tool that automatically fits a Plane or Quad to the active Camera view, for both **Perspective** and **Orthographic** cameras.  
It positions, rotates, and scales the mesh so that it perfectly fills the camera frame in world space, without using a Canvas.

Designed for quick background cards, full-screen overlays, screen shaders, and VRChat avatar gimmicks. :contentReference[oaicite:0]{index=0}

### Features

-   🎯 **Exact Camera Fit**  
    Automatically matches the Plane / Quad to the camera’s frustum so it fills the entire view.
-   🧭 **Perspective & Orthographic Support**  
    Works with both perspective (FOV-based) and orthographic (orthographicSize-based) cameras.
-   🧱 **Plane & Quad Support**  
    Supports Unity built-in **Plane (XZ, 10×10)** and **Quad (XY, 1×1)** automatically.
-   📐 **Flexible Distance Control**  
    Use the current distance from the camera, or specify a manual distance value.
-   ⬛ **Square Mode**  
    Option to force the mesh to be a perfect square (width = height) regardless of aspect ratio.
-   🔄 **View-space Rotation**  
    Extra rotation options (0° / 90° / 180° / 270°) that spin the image around the camera’s forward axis.
-   🌍 **Multi-Language UI**  
    Built-in support for **English, Korean, Japanese**.
-   ↩️ **Undo Support**  
    Fully integrated with Unity’s Undo system.

### Requirements

-   Unity **2022.3.22f1** or later
-   Any Render Pipeline (Built-in / URP / HDRP)
-   A mesh using Unity Plane or Quad (or compatible size)

### Installation

1. Download `PlaneFitToCameraTool.cs`.
2. Place it under an `Editor` folder in your Unity project, for example:
    - `Assets/Iyan-Kim/PlaneFitToCameraTool/Editor/PlaneFitToCameraTool.cs`
3. After compilation, the tool will appear in the menu:  
   **Iyan-Kim > Tools > Plane Fit To Camera**

### How to Use

#### Basic Usage

1. Open the window from  
   **Iyan-Kim > Tools > Plane Fit To Camera**
2. In the tool window:
    - Assign a **Camera** (usually your main or scene camera).
    - Assign a **Plane Object** (a GameObject that has a Plane or Quad mesh).
3. Configure options:
    - **Align Position** – Move the mesh in front of the camera.
    - **Align Rotation** – Rotate the mesh so it faces the camera.
    - **Use Current Distance** – Use the current distance from the camera to the object.
        - Disable to use the **Manual Distance** value instead.
    - **Keep Square (Width = Height)** – Force the mesh to be a square.
    - **Image Rotation** – Extra rotation (0° / 90° / 180° / 270°) around the camera’s view direction.
4. Click **Fit Plane**.
5. The selected Plane / Quad will now perfectly match the camera view.

### Options

-   **Align Position**  
    When enabled, the object is moved to exactly `camera.position + camera.forward * distance`.
-   **Align Rotation**
    -   For **Plane** (XZ): rotates so its local normal points toward the camera and fills the view.
    -   For **Quad** (XY): rotates so it faces the camera directly.  
        Additionally, a built-in 180° flip is applied so textures are not upside-down.
-   **Use Current Distance / Manual Distance**
    -   **Use Current Distance**: Uses the current projection of the object along the camera’s forward axis.
    -   If the object is behind the camera or too close, it falls back to **Manual Distance**.
    -   **Manual Distance**: Always use this distance value from the camera.
-   **Keep Square (Width = Height)**  
    Uses the larger of width/height and applies it to both axes to make the plane square.
-   **Image Rotation (0° / 90° / 180° / 270°)**  
    Rotates around the **camera forward axis**, so the image appears to spin in place from the camera’s point of view.  
    Useful for flipping textures or rotating background cards without touching the material.
-   **Language**  
    Choose the UI language:
    -   한국어 (default)
    -   English
    -   日本語

### Tips

-   For full-screen overlays that should always match the camera, you can:
    -   Parent the Plane / Quad under the camera.
    -   Run **Fit Plane** once.
    -   Minor camera movements will still keep the plane aligned (as long as FOV and aspect don’t change).
-   **Unity Plane vs Quad**
    -   Unity **Plane** is 10×10 units on the XZ plane.
    -   Unity **Quad** is 1×1 units on the XY plane.  
        The tool automatically adjusts scale based on detected mesh type.
-   If you use a custom mesh:
    -   It will be treated like a Quad with size 1×1 by default.
    -   You can scale it manually after fitting if needed.
-   Works great for:
    -   Background plates
    -   Gradient backdrops
    -   Screen-space FX using custom shaders
    -   VRChat avatar / world gimmicks (screen shaders, cut-ins, etc.)

### Troubleshooting

-   **The plane is not filling the camera view exactly**
    -   Check that you are using the same camera as in the Game view.
    -   Make sure the camera’s **FOV** or **aspect ratio** is not changed after clicking **Fit Plane**.
-   **Texture appears upside-down or rotated**
    -   Use the **Image Rotation** dropdown to rotate by 90° / 180° / 270°.
    -   If still wrong, double-check the UV layout of your mesh.
-   **Plane appears behind other geometry**
    -   Increase the **Manual Distance** so the plane is in front of the objects you want to cover.
    -   Also check the material’s **Render Queue** and **ZWrite**/`ZTest` settings.
-   **Nothing happens when I click “Fit Plane”**
    -   Ensure both **Camera** and **Plane Object** are assigned.
    -   Check the Console for warnings if the mesh has no `MeshFilter` or `sharedMesh`.

---

## 📖 한국어

### 개요

카메라의 화면에 맞춰 Plane 또는 Quad를 자동으로 정렬해주는 Unity 에디터 툴입니다.  
Canvas를 사용하지 않고, 메쉬를 카메라 앞에 정확히 맞춰 전체 화면을 채우도록 배치할 수 있으며 **원근/직교(Orthographic) 카메라 모두 지원**합니다.

배경 카드, 풀스크린 오버레이, 스크린 쉐이더, VRChat 기믹 등에 사용하기 좋습니다.

### 기능

-   🎯 **카메라 화면에 딱 맞게 정렬**  
    Plane / Quad를 카메라 프러스텀에 맞춰 정확히 스케일/위치/회전 설정
-   🧭 **원근 & 직교 카메라 지원**  
    원근 카메라(FOV 기반)와 직교 카메라(orthographicSize 기반) 모두에서 동일하게 화면을 채우도록 동작
-   🧱 **Plane & Quad 자동 지원**  
    Unity 기본 **Plane(XZ, 10×10)** 과 **Quad(XY, 1×1)** 를 자동 인식
-   📐 **유연한 거리 제어**  
    현재 카메라와의 거리를 사용하거나, 직접 입력한 수동 거리 값 사용
-   ⬛ **정사각형 모드**  
    화면 비율과 상관없이 가로=세로인 정사각형으로 맞추기 옵션
-   🔄 **뷰 기준 회전**  
    카메라 정면 축 기준으로 0° / 90° / 180° / 270° 회전
-   🌍 **다국어 UI 지원**  
    **한국어, 영어, 일본어** 지원 (기본 언어: 한국어)
-   ↩️ **Undo 지원**  
    Unity Undo 시스템과 완전히 통합

### 요구 사항

-   Unity **2022.3.22f1** 이상
-   렌더 파이프라인 무관 (Built-in / URP / HDRP)
-   Unity Plane 또는 Quad(또는 호환 메쉬)

### 설치 방법

1. `PlaneFitToCameraTool.cs` 파일을 다운로드합니다.
2. Unity 프로젝트의 `Editor` 폴더 아래에 배치합니다. 예:
    - `Assets/Iyan-Kim/PlaneFitToCameraTool/Editor/PlaneFitToCameraTool.cs`
3. 컴파일이 끝나면 메뉴에 툴이 추가됩니다:  
   **Iyan-Kim > Tools > Plane Fit To Camera**

### 사용 방법

#### 기본 사용법

1. 메뉴에서  
   **Iyan-Kim > Tools > Plane Fit To Camera** 를 엽니다.
2. 툴 창에서:
    - **Camera** 에 사용할 카메라를 지정 (주로 메인/씬 카메라)
    - **Plane Object** 에 Plane 또는 Quad 메쉬가 있는 GameObject 지정
3. 옵션을 설정합니다:
    - **Align Position** – 카메라 앞 지정된 거리 위치로 이동
    - **Align Rotation** – 카메라를 향하도록 회전
    - **Use Current Distance** – 현재 카메라와의 거리 사용
        - 끄면 **Manual Distance** 값 사용
    - **Keep Square (Width = Height)** – 항상 정사각형으로 유지
    - **Image Rotation** – 카메라 앞에서 0° / 90° / 180° / 270° 회전
4. **Fit Plane** 버튼을 클릭
5. 선택한 Plane / Quad가 카메라 화면을 꽉 채우도록 정렬됩니다.

### 옵션 설명

-   **Align Position**  
    `camera.position + camera.forward * distance` 위치로 이동합니다.
-   **Align Rotation**
    -   **Plane(XZ)**: 로컬 법선이 카메라를 향하도록 회전 후 화면을 채우도록 맞춤
    -   **Quad(XY)**: 카메라 정면을 바라보도록 회전  
        추가로 텍스처가 거꾸로 보이지 않도록 180° 보정 회전이 적용됩니다.
-   **Use Current Distance / Manual Distance**
    -   **Use Current Distance**: 현재 오브젝트의 위치를 카메라 forward 축에 투영한 거리 사용
    -   오브젝트가 카메라 뒤쪽이면 **Manual Distance** 값으로 대체
    -   **Manual Distance**: 항상 이 값을 거리로 사용
-   **Keep Square (Width = Height)**  
    계산된 가로/세로 중 더 큰 값을 양쪽에 동일하게 적용하여 정사각형으로 만듭니다.
-   **Image Rotation (0° / 90° / 180° / 270°)**  
    카메라의 **forward 축**을 기준으로 회전하므로, 카메라에서 보면 화면에서 빙글빙글 도는 느낌으로 회전합니다.  
    텍스처 방향을 맞추거나, 수직/수평을 쉽게 바꾸고 싶을 때 편리합니다.
-   **Language / 언어 / 言語**  
    창 하단에서 UI 언어를 선택할 수 있습니다:
    -   한국어 (기본값)
    -   English
    -   日本語

### 팁

-   카메라에 붙는 고정 배경을 만들고 싶다면:
    -   Plane / Quad를 카메라의 자식으로 둔 뒤
    -   한 번 **Fit Plane** 을 실행해 준 후 사용하면 편합니다.
-   **Unity Plane vs Quad**
    -   **Plane**: XZ 평면 기준 10×10 유닛
    -   **Quad**: XY 평면 기준 1×1 유닛  
        툴에서 자동으로 판별 후, 그에 맞게 스케일을 계산합니다.
-   커스텀 메쉬를 사용할 경우:
    -   기본적으로 1×1 Quad로 가정됩니다.
    -   필요하면 Fit 후에 수동으로 스케일을 조정해도 됩니다.
-   활용 예시:
    -   단색/그라디언트 배경
    -   스크린 쉐이더용 오브젝트
    -   컷씬/카메라 연출용 패널
    -   VRChat 아바타 스크린 연출, 피격 연출 등

### 문제 해결

-   **Plane이 화면을 완전히 채우지 않습니다**
    -   Game 뷰에서 사용하는 카메라와 툴에 지정한 카메라가 같은지 확인하세요.
    -   **FOV** 또는 **Aspect Ratio** 를 바꿨다면 다시 **Fit Plane** 을 실행하세요.
-   **텍스처가 거꾸로 또는 옆으로 보입니다**
    -   **Image Rotation** 옵션으로 90° / 180° / 270° 회전을 시도해 보세요.
    -   그래도 이상하다면 메쉬의 UV가 올바른지 확인하세요.
-   **오브젝트가 다른 오브젝트 뒤에 가려집니다**
    -   **Manual Distance** 값을 키워서 더 앞쪽으로 이동시키세요.
    -   머티리얼의 Render Queue, ZWrite, ZTest 설정도 함께 확인해 주세요.
-   **Fit Plane 버튼을 눌러도 변화가 없습니다**
    -   **Camera** 와 **Plane Object** 가 모두 지정되어 있는지 확인하세요.
    -   Console에 경고가 출력되는지 확인하세요 (MeshFilter / sharedMesh 없음 등).

---

## 📖 日本語

### 概要

カメラビューに合わせて Plane または Quad を自動的にフィットさせる Unity エディター用ツールです。  
Canvas を使わず、メッシュをカメラの前に配置して画面いっぱいに表示でき、**パースペクティブ／オーソグラフィック両方のカメラに対応**しています。

背景カード、フルスクリーンエフェクト、スクリーンシェーダー、VRChat ギミックなどに便利です。

### 機能

-   🎯 **カメラビューに完全フィット**  
    Plane / Quad をカメラのフラスタムに合わせて位置・回転・スケールを自動調整
-   🧭 **パースペクティブ & オーソグラフィック対応**  
    パースペクティブカメラ（FOV）とオーソグラフィックカメラ（orthographicSize）の両方で画面を完全に埋めるように調整
-   🧱 **Plane & Quad 対応**  
    Unity 標準の **Plane(XZ, 10×10)** と **Quad(XY, 1×1)** を自動判定
-   📐 **距離コントロール**  
    現在の距離を使用するか、任意の距離を手動入力可能
-   ⬛ **正方形モード**  
    画面比率に関係なく、幅＝高さの正方形に強制
-   🔄 **ビュー空間回転**  
    カメラの forward 軸を中心に 0° / 90° / 180° / 270° 回転
-   🌍 **多言語 UI**  
    **日本語・韓国語・英語** に対応（デフォルトは韓国語）
-   ↩️ **Undo サポート**  
    Unity の Undo システムと完全統合

### 必要環境

-   Unity **2022.3.22f1** 以降
-   任意のレンダーパイプライン（Built-in / URP / HDRP）
-   Unity Plane または Quad（または互換メッシュ）

### インストール方法

1. `PlaneFitToCameraTool.cs` をダウンロードします。
2. Unity プロジェクトの `Editor` フォルダ内に配置します。例:  
   `Assets/Iyan-Kim/PlaneFitToCameraTool/Editor/PlaneFitToCameraTool.cs`
3. スクリプトのコンパイル後、メニューにツールが表示されます:  
   **Iyan-Kim > Tools > Plane Fit To Camera**

### 使い方

#### 基本手順

1. メニューから  
   **Iyan-Kim > Tools > Plane Fit To Camera** を開く
2. ウィンドウ内で:
    - **Camera** に使用するカメラを指定
    - **Plane Object** に Plane または Quad メッシュを持つ GameObject を指定
3. オプションを設定:
    - **Align Position** – カメラ前の指定距離に移動
    - **Align Rotation** – カメラの方を向くように回転
    - **Use Current Distance** – 現在の距離を使用
        - オフにすると **Manual Distance** の値を使用
    - **Keep Square (Width = Height)** – 正方形に固定
    - **Image Rotation** – カメラ前で 0° / 90° / 180° / 270° 回転
4. **Fit Plane** ボタンをクリック
5. 選択した Plane / Quad がカメラビューにぴったり合うように調整されます。

### オプション詳細

-   **Align Position**  
    位置を `camera.position + camera.forward * distance` に設定します。
-   **Align Rotation**
    -   **Plane(XZ)**: ローカル法線がカメラの方向を向くように回転し、画面を埋めるように調整
    -   **Quad(XY)**: カメラ正面を向くように回転  
        テクスチャが逆さにならないように 180° の補正回転も含まれています。
-   **Use Current Distance / Manual Distance**
    -   **Use Current Distance**: オブジェクト位置を camera.forward に投影した距離を使用
    -   オブジェクトがカメラの後ろにある場合は Manual Distance にフォールバック
    -   **Manual Distance**: 常にこの距離を使用
-   **Keep Square (Width = Height)**  
    計算された幅・高さのうち大きい方を両方に適用し、正方形にします。
-   **Image Rotation (0° / 90° / 180° / 270°)**  
    カメラの **forward 軸** を中心に回転するため、画面上でカードがその場で回転しているように見えます。  
    テクスチャの向きを合わせたいときに便利です。
-   **Language / 언어 / 言語**  
    ウィンドウ下部のドロップダウンで UI 言語を選択できます:
    -   한국어（デフォルト）
    -   English
    -   日本語

### ヒント

-   カメラに追従する背景カードを作る場合:
    -   Plane / Quad をカメラの子オブジェクトにして
    -   一度 **Fit Plane** を実行しておくと便利です。
-   **Unity Plane と Quad の違い**
    -   **Plane**: XZ 平面、サイズは 10×10 ユニット
    -   **Quad**: XY 平面、サイズは 1×1 ユニット  
        ツール側で自動判別し、適切なスケールを計算します。
-   カスタムメッシュを使う場合:
    -   デフォルトでは 1×1 Quad として扱われます。
    -   必要なら Fit 後に手動でスケール調整してください。

### トラブルシューティング

-   **画面を完全に埋めない**
    -   Game ビューで使っているカメラと、ツールで指定したカメラが同じか確認してください。
    -   カメラの FOV や Aspect を変更した場合は、再度 **Fit Plane** を押してください。
-   **テクスチャが逆さ・横向きになる**
    -   **Image Rotation** の値を変えて 90°/180°/270° を試してください。
    -   それでもおかしい場合は、メッシュの UV レイアウトを確認してください。
-   **オブジェクトが他のオブジェクトの裏に隠れる**
    -   **Manual Distance** を大きくして、より手前に配置してください。
    -   マテリアルの Render Queue や ZWrite / ZTest も確認しましょう。
-   **“Fit Plane” を押しても変化がない**
    -   **Camera** と **Plane Object** が両方設定されているか確認してください。
    -   Console に警告が出ていないか確認してください（MeshFilter / sharedMesh 不足など）。

---

## 📝 License

This tool is free to use in personal and commercial Unity / VRChat projects.  
Please keep the original author credit if you redistribute or modify the script.

## 🤝 Contributing

Suggestions, improvements, and pull requests are welcome.

## 📧 Support

If you encounter issues or have feature requests, please contact the author or open an issue in the repository where this tool is distributed.

## 📌 Changelog

### **1.0.3 – Parent Scale Compensation Added**

-   Fixed an issue where fitting was incorrect when the Plane/Quad had a parent object  
    with non-uniform or very small scale.
-   Plane/Quad scaling now fully compensates for parent `lossyScale`, ensuring accurate  
    world-space size in all hierarchy structures.
-   Also preserves local scale sign (negative scale / flipped mesh support).

### **1.0.2 – Orthographic Camera Support**

-   Added support for orthographic cameras.
-   Plane/Quad now scales correctly using `orthographicSize`.
-   Behavior is fully unified between Perspective / Orthographic modes.

### **1.0.1 – Multi-Language UI Update**

-   Added full localization for English, Korean, and Japanese.
-   Default language set to Korean.
-   Language selector UI improved (three-language label).

### **1.0.0 – Initial Release**

-   Base Plane Fit To Camera functionality.
-   Support for Unity Plane (XZ) and Quad (XY).
-   Automatic position/rotation/scale fitting for perspective cameras.
-   Extra in-plane rotation (0° / 90° / 180° / 270°).
-   Undo system support.

---

**Created with ❤️ for VRChat & Unity creators.**
