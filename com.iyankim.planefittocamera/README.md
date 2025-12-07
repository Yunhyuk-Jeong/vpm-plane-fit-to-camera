# Plane Fit To Camera Tool

**Version 1.0.6**

---

## 📖 English

### Overview

A Unity Editor tool that automatically fits one or more **Plane** meshes to the active Camera view, for both **Perspective** and **Orthographic** cameras.  
It positions, rotates, and scales the mesh so that it perfectly fills the camera frame in world space, without using a Canvas.

Designed for quick background cards, full-screen overlays, screen shaders, and VRChat avatar gimmicks.

### Features

-   🎯 **Exact Camera Fit**  
    Automatically matches Plane meshes to the camera’s frustum so they fill the entire view.
-   🧭 **Perspective & Orthographic Support**  
    Works with both perspective (FOV-based) and orthographic (orthographicSize-based) cameras.
-   📚 **Multi-Plane Support**  
    Configure and fit multiple Plane objects in a single operation.
-   🧱 **Plane-Oriented Design**  
    Optimized for Unity built-in **Plane (XZ, 10×10)**.  
    Other meshes are treated as Plane-like and scaled using their XZ size.
-   📐 **Flexible Distance Control**  
    Use the current distance from the camera, or specify a manual distance value.
-   ⬛ **Square Mode**  
    Option to force the mesh to be a perfect square (width = height) regardless of aspect ratio.
-   🔄 **View-space Rotation**  
    Extra rotation options (0° / 90° / 180° / 270°) that spin the image around the camera’s forward axis.
-   🌍 **Multi-Language UI**  
    Built-in support for **English, Korean, Japanese** (default: Korean).
-   ↩️ **Undo Support**  
    Fully integrated with Unity’s Undo system.

### Requirements

-   Unity **2022.3.22f1** or later
-   Any Render Pipeline (Built-in / URP / HDRP)
-   A mesh using Unity Plane (or a Plane-like mesh on XZ)

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
    - Add one or more **Plane Objects** (GameObjects that use a Plane-like mesh).
3. Configure options:
    - **Align Position** – Move each mesh in front of the camera.
    - **Align Rotation** – Rotate each mesh so it faces the camera correctly.
    - **Use Current Distance** – Use the current distance from the camera to each object.
        - Disable to use the **Manual Distance** value instead.
    - **Keep Square (Width = Height)** – Force the mesh to be a square.
    - **Image Rotation** – Extra rotation (0° / 90° / 180° / 270°) around the camera’s view direction.
4. Click **Fit Plane(s)**.
5. All assigned Plane objects will now perfectly match the camera view.

### Options

-   **Align Position**  
    When enabled, the object is moved to exactly `camera.position + camera.forward * distance`.
-   **Align Rotation**  
    Assumes a Plane aligned on the XZ plane (Unity built-in Plane).  
    Rotates the Plane so its local normal points toward the camera and fills the view.  
    A built-in 180° flip plus optional extra rotation is applied so textures can be oriented as needed.
-   **Use Current Distance / Manual Distance**
    -   **Use Current Distance**: Uses the current projection of the object along the camera’s forward axis.
    -   If the object is behind the camera or too close, it falls back to **Manual Distance**.
    -   **Manual Distance**: Always use this distance value from the camera.
-   **Keep Square (Width = Height)**  
    Uses the larger of width/height and applies it to both axes to make the plane square.
-   **Image Rotation (0° / 90° / 180° / 270°)**  
    Rotates around the **camera forward axis**, so the image appears to spin in place from the camera’s point of view.  
    Useful for flipping textures or rotating background cards without touching the material.
-   **Language / 언어 / 言語**  
    Choose the UI language at the bottom of the window:
    -   한국어 (default)
    -   English
    -   日本語

### Tips

-   For full-screen overlays that should always match the camera, you can:
    -   Parent the Plane under the camera.
    -   Run **Fit Plane(s)** once.
    -   Minor camera movements will still keep the plane aligned (as long as FOV and aspect don’t change).
-   **Unity Plane**
    -   Unity **Plane** is 10×10 units on the XZ plane.
    -   The tool assumes this layout; if you use custom meshes, scale behavior may differ slightly.
-   If you use a custom mesh:
    -   It is treated as a Plane-like mesh on XZ.
    -   You can scale it manually after fitting if needed.
-   Works great for:
    -   Background plates
    -   Gradient backdrops
    -   Screen-space FX using custom shaders
    -   VRChat avatar / world gimmicks (screen shaders, cut-ins, etc.)

### Troubleshooting

-   **The plane is not filling the camera view exactly**
    -   Check that you are using the same camera as in the Game view.
    -   Make sure the camera’s **FOV** or **aspect ratio** is not changed after clicking **Fit Plane(s)**.
-   **Texture appears upside-down or rotated**
    -   Use the **Image Rotation** dropdown to rotate by 90° / 180° / 270°.
    -   If still wrong, double-check the UV layout of your mesh.
-   **Plane appears behind other geometry**
    -   Increase the **Manual Distance** so the plane is in front of the objects you want to cover.
    -   Also check the material’s **Render Queue** and **ZWrite**/`ZTest` settings.
-   **Nothing happens when I click “Fit Plane(s)”**
    -   Ensure a **Camera** is assigned and at least one **Plane** slot has a valid Transform.
    -   Check the Console for warnings if the mesh has no `MeshFilter` or `sharedMesh`.

---

## 📖 한국어

### 개요

카메라의 화면에 맞춰 **여러 개의 Plane**을 자동으로 정렬해주는 Unity 에디터 툴입니다.  
Canvas를 사용하지 않고, 메쉬를 카메라 앞에 정확히 맞춰 전체 화면을 채우도록 배치할 수 있으며 **원근/직교(Orthographic) 카메라 모두 지원**합니다.

배경 카드, 풀스크린 오버레이, 스크린 쉐이더, VRChat 기믹 등에 사용하기 좋습니다.

### 기능

-   🎯 **카메라 화면에 딱 맞게 정렬**  
    Plane을 카메라 프러스텀에 맞춰 정확히 스케일/위치/회전 설정
-   🧭 **원근 & 직교 카메라 지원**  
    원근 카메라(FOV 기반)와 직교 카메라(orthographicSize 기반) 모두에서 동일하게 화면을 채우도록 동작
-   📚 **다중 Plane 지원**  
    여러 개의 Plane 오브젝트를 한 번에 등록하고, 한 번에 정렬
-   🧱 **Plane 전용 설계**  
    Unity 기본 **Plane(XZ, 10×10)** 에 최적화  
    다른 메쉬도 Plane처럼 취급되지만, XZ 기준 크기를 사용해 스케일을 계산
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
-   Unity Plane 또는 Plane과 유사한 XZ 메쉬

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
    - Plane 슬롯에 하나 이상의 **Plane 오브젝트**를 추가
3. 옵션을 설정합니다:
    - **Align Position** – 카메라 앞 지정된 거리 위치로 이동
    - **Align Rotation** – 카메라를 향하도록 회전
    - **Use Current Distance** – 현재 카메라와의 거리 사용
        - 끄면 **Manual Distance** 값 사용
    - **Keep Square (Width = Height)** – 항상 정사각형으로 유지
    - **Image Rotation** – 카메라 앞에서 0° / 90° / 180° / 270° 회전
4. **Fit Plane(s)** 버튼을 클릭
5. 등록된 모든 Plane이 카메라 화면을 꽉 채우도록 정렬됩니다.

### 옵션 설명

-   **Align Position**  
    `camera.position + camera.forward * distance` 위치로 이동합니다.
-   **Align Rotation**  
    Plane(XZ)을 전제로, 로컬 Y축(법선)이 카메라를 향하도록 회전한 뒤 화면을 채우도록 맞춥니다.  
    텍스처 방향을 맞추기 위한 180° 보정 + 추가 회전(0/90/180/270°)을 함께 적용합니다.
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
    -   Plane을 카메라의 자식으로 둔 뒤
    -   한 번 **Fit Plane(s)** 을 실행해 준 후 사용하면 편합니다.
-   **Unity Plane**
    -   **Plane**: XZ 평면 기준 10×10 유닛
    -   툴은 이 레이아웃을 기준으로 스케일을 계산합니다.
-   커스텀 메쉬를 사용할 경우:
    -   기본적으로 XZ 기준 Plane처럼 취급됩니다.
    -   필요하면 Fit 후에 수동으로 스케일을 조정해도 됩니다.
-   활용 예시:
    -   단색/그라디언트 배경
    -   스크린 쉐이더용 오브젝트
    -   컷씬/카메라 연출용 패널
    -   VRChat 아바타 스크린 연출, 피격 연출 등

### 문제 해결

-   **Plane이 화면을 완전히 채우지 않습니다**
    -   Game 뷰에서 사용하는 카메라와 툴에 지정한 카메라가 같은지 확인하세요.
    -   **FOV** 또는 **Aspect Ratio** 를 바꿨다면 다시 **Fit Plane(s)** 을 실행하세요.
-   **텍스처가 거꾸로 또는 옆으로 보입니다**
    -   **Image Rotation** 옵션으로 90° / 180° / 270° 회전을 시도해 보세요.
    -   그래도 이상하다면 메쉬의 UV가 올바른지 확인하세요.
-   **오브젝트가 다른 오브젝트 뒤에 가려집니다**
    -   **Manual Distance** 값을 키워서 더 앞쪽으로 이동시키세요.
    -   머티리얼의 Render Queue, ZWrite, ZTest 설정도 함께 확인해 주세요.
-   **Fit Plane(s) 버튼을 눌러도 변화가 없습니다**
    -   **Camera** 와 최소 하나의 **Plane** 슬롯이 올바르게 지정되어 있는지 확인하세요.
    -   Console에 경고가 출력되는지 확인하세요 (MeshFilter / sharedMesh 없음 등).

---

## 📖 日本語

### 概要

カメラビューに合わせて **複数の Plane** を自動的にフィットさせる Unity エディター用ツールです。  
Canvas を使わず、メッシュをカメラの前に配置して画面いっぱいに表示でき、**パースペクティブ／オーソグラフィック両方のカメラに対応**しています。

背景カード、フルスクリーンエフェクト、スクリーンシェーダー、VRChat ギミックなどに便利です。

### 機能

-   🎯 **カメラビューに完全フィット**  
    Plane をカメラのフラスタムに合わせて位置・回転・スケールを自動調整
-   🧭 **パースペクティブ & オーソグラフィック対応**  
    パースペクティブカメラ（FOV）とオーソグラフィックカメラ（orthographicSize）の両方で画面を完全に埋めるように調整
-   📚 **複数 Plane 対応**  
    複数の Plane オブジェクトを一度に登録し、まとめてフィット
-   🧱 **Plane 向け設計**  
    Unity 標準 **Plane(XZ, 10×10)** 用に最適化  
    その他のメッシュも Plane として扱い、XZ サイズを用いてスケール計算
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
-   Unity Plane または XZ ベースの Plane 互換メッシュ

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
    - Plane スロットに 1 つ以上の **Plane オブジェクト** を追加
3. オプションを設定:
    - **Align Position** – カメラ前の指定距離に移動
    - **Align Rotation** – カメラの方を向くように回転
    - **Use Current Distance** – 現在の距離を使用
        - オフにすると **Manual Distance** の値を使用
    - **Keep Square (Width = Height)** – 正方形に固定
    - **Image Rotation** – カメラ前で 0° / 90° / 180° / 270° 回転
4. **Fit Plane(s)** ボタンをクリック
5. 登録されたすべての Plane がカメラビューにぴったり合うように調整されます。

### オプション詳細

-   **Align Position**  
    位置を `camera.position + camera.forward * distance` に設定します。
-   **Align Rotation**  
    Plane(XZ) を前提とし、ローカル Y 軸（法線）がカメラの方向を向くように回転し、画面を埋めるように調整します。  
    テクスチャ向きを合わせるための 180° 補正 + 追加回転(0/90/180/270°) も適用されます。
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
    -   Plane をカメラの子オブジェクトにして
    -   一度 **Fit Plane(s)** を実行しておくと便利です。
-   **Unity Plane**
    -   **Plane**: XZ 平面、サイズは 10×10 ユニット
    -   ツールはこのレイアウトを前提にスケールを計算します。
-   カスタムメッシュを使う場合:
    -   基本的に XZ ベースの Plane として扱われます。
    -   必要なら Fit 後に手動でスケール調整してください。

### トラブルシューティング

-   **画面を完全に埋めない**
    -   Game ビューで使っているカメラと、ツールで指定したカメラが同じか確認してください。
    -   カメラの FOV や Aspect を変更した場合は、再度 **Fit Plane(s)** を押してください。
-   **テクスチャが逆さ・横向きになる**
    -   **Image Rotation** の値を変えて 90°/180°/270° を試してください。
    -   それでもおかしい場合は、メッシュの UV レイアウトを確認してください。
-   **オブジェクトが他のオブジェクトの裏に隠れる**
    -   **Manual Distance** を大きくして、より手前に配置してください。
    -   マテリアルの Render Queue や ZWrite / ZTest も確認しましょう。
-   **“Fit Plane(s)” を押しても変化がない**
    -   **Camera** と、少なくとも 1 つの **Plane** スロットが正しく設定されているか確認してください。
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

### **1.0.6 – Comment Cleanup & Code Documentation Update**

-   Updated and refined code comments to improve clarity and maintainability.
-   Removed outdated or misleading descriptions.
-   Added additional inline documentation for better readability during development.

### **1.0.5 – Package Metadata Fix & Compatibility Update**

-   Updated package.json to use the correct author object format required by the VRChat VPM specification.
    This resolves installation failures in VCC where the package could not be deserialized correctly.
-   Improved overall compatibility with the VRChat Creator Companion package installer.
-   No functional changes to runtime/editor behavior; this update focuses on fixing package metadata for proper distribution.

### **1.0.4 – Multi-Plane Support & Plane-Only Refactor**

-   Added support for assigning and fitting **multiple Plane objects** at once.
-   Updated UI to manage a list of Plane slots (add/remove/clear).
-   Removed official Quad-specific handling and simplified logic around **Plane(XZ)** usage.
-   Updated documentation to reflect Plane-only design and multi-plane workflow.

### **1.0.3 – Parent Scale Compensation Added**

-   Fixed an issue where fitting was incorrect when the Plane had a parent object  
    with non-uniform or very small scale.
-   Plane scaling now fully compensates for parent `lossyScale`, ensuring accurate  
    world-space size in all hierarchy structures.
-   Also preserves local scale sign (negative scale / flipped mesh support).

### **1.0.2 – Orthographic Camera Support**

-   Added support for orthographic cameras.
-   Plane now scales correctly using `orthographicSize`.
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
