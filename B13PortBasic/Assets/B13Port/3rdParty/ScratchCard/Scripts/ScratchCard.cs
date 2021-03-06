using DG.Tweening;
using B13Port.Common;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ScratchCard : MonoBehaviour
{
    public enum Quality
    {
        Low = 4,
        Medium = 2,
        High = 1
    }

    public enum ScratchMode
    {
        Erase,
        Restore
    }

    public Camera MainCamera;
    public Transform Surface;
    public Quality RenderTextureQuality = Quality.High;
    public Material Eraser;
    public Material Progress;
    public Material ScratchSurface;
    public RenderTexture RenderTexture;
    public Vector2 BrushScale = Vector2.one;
    public bool IsPause = false;
    private ScratchMode _mode = ScratchMode.Erase;
    public ScratchMode Mode
    {
        get { return _mode; }
        set
        {
            _mode = value;
            var blendOp = _mode == ScratchMode.Erase ? (int)BlendOp.Add : (int)BlendOp.ReverseSubtract;
            Eraser.SetInt(BlendOpShaderParam, blendOp);
        }
    }

    public bool IsScratching
    {
        get
        {
            foreach (var scratching in isScratching)
            {
                if (scratching)
                    return true;
            }
            return false;
        }
    }

    private Mesh mesh;
    private Mesh quadMesh;
    private CommandBuffer commandBuffer;
    private RenderTargetIdentifier rti;
    private Renderer scratchRenderer;
    private RectTransform rectTransform;
    private Vector2 boundsSize;
    private Vector2 halfBoundsSize;
    private Vector2 imageSize;
    private Vector2[] eraseStartPositions;
    private Vector2[] eraseEndPositions;
    private Vector2 erasePosition;
    private bool isCanvasOverlay;
    private bool isFirstFrame = true;
    private bool[] isScratching;
    private bool[] isStartPosition;
    private int lastFrameId;

    private const string MaskTexProperty = "_MaskTex";
    private const string MainTexProperty = "_MainTex";
    private const string SourceTexProperty = "_SourceTex";
    private const string BlendOpShaderParam = "_BlendOpValue";
    private const int TouchMaxCount = 10;

    void Start()
    {
        eraseStartPositions = new Vector2[TouchMaxCount];
        eraseEndPositions = new Vector2[TouchMaxCount];
        isScratching = new bool[TouchMaxCount];
        isStartPosition = new bool[TouchMaxCount];
        for (int i = 0; i < isStartPosition.Length; i++)
        {
            isStartPosition[i] = true;
        }

        commandBuffer = new CommandBuffer { name = "ScratchCard" };

        quadMesh = new Mesh
        {
            vertices = new Vector3[4],
            uv = new[]
            {
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0),
                new Vector2(0, 0),
            },
            triangles = new[]
            {
                0, 1, 2,
                2, 3, 0
            },
            colors = new[]
            {
                Color.white,
                Color.white,
                Color.white,
                Color.white
            }
        };

        GetScratchBounds();
        CreateRenderTexture();
        FirstFrame();
    }
    private float timer = 0;
    public void FirstFrame()
    {
        commandBuffer.SetRenderTarget(rti);
        commandBuffer.ClearRenderTarget(false, true, Color.clear);
        Graphics.ExecuteCommandBuffer(commandBuffer);
    }
    void Update()
    {
        if (IsPause) return;

        if (lastFrameId == Time.frameCount)
        {
            return;
        }

        UpdateInput();

        if (isFirstFrame)
        {
            commandBuffer.SetRenderTarget(rti);
            commandBuffer.ClearRenderTarget(false, true, Color.clear);
            Graphics.ExecuteCommandBuffer(commandBuffer);
            isFirstFrame = false;
        }

        for (int i = 0; i < isScratching.Length; i++)
        {
            var scratching = isScratching[i];
            if (scratching)
            {
                if (eraseStartPositions[i] == eraseEndPositions[i])
                {
                    ScratchHole();
                }
                else
                {
                    ScratchLine(i);
                }
            }
        }
        lastFrameId = Time.frameCount;
    }

    private void UpdateInput()
    {
        if (!Input.touchSupported || Input.mousePresent)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isScratching[0] = false;
                isStartPosition[0] = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isScratching[0] = false;
            }
            if (Input.GetMouseButton(0))
            {
                timer += Time.deltaTime;
                if (timer >= 0.2f)
                {
                    timer = 0;
                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                }
                OnScratch(0, Input.mousePosition);
            }
        }
        if (Input.touchSupported)
        {
            foreach (Touch touch in Input.touches)
            {
                var fingerId = touch.fingerId + 1;
                if (touch.phase == TouchPhase.Began)
                {
                    isScratching[fingerId] = false;
                    isStartPosition[fingerId] = true;
                }

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    if (touch.phase == TouchPhase.Moved)
                    {
                        timer += Time.deltaTime;
                    }
                    else
                    {
                        GameMain.Ins.audioMgr.Stop(MusicPath.ScratchCard);
                    }
                    if (timer >= 0.2f)
                    {
                        timer = 0;
                        MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                    }
                    OnScratch(fingerId, touch.position);
                }

                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    //GameMain.Ins.audioMgr.Stop(MusicPath.ScratchCard);
                    isScratching[fingerId] = false;
                }
            }
        }
    }

    private void OnScratch(int fingerId, Vector2 position)
    {
        var clickPosition = isCanvasOverlay ? (Vector3)position : MainCamera.ScreenToWorldPoint(position);
        var clickLocalPosition = Vector2.Scale(Surface.InverseTransformPoint(clickPosition), Surface.lossyScale) + halfBoundsSize;
        var pixelsPerInch = new Vector2(imageSize.x / boundsSize.x / Surface.lossyScale.x, imageSize.y / boundsSize.y / Surface.lossyScale.y);
        erasePosition = Vector2.Scale(Vector2.Scale(clickLocalPosition, Surface.lossyScale), pixelsPerInch);

        if (isStartPosition[fingerId])
        {
            eraseEndPositions[fingerId] = eraseStartPositions[fingerId];
            eraseStartPositions[fingerId] = erasePosition;
        }
        else
        {
            eraseEndPositions[fingerId] = erasePosition;
        }
        isStartPosition[fingerId] = !isStartPosition[fingerId];

        if (!isScratching[fingerId])
        {
            eraseEndPositions[fingerId] = eraseStartPositions[fingerId];
            isScratching[fingerId] = true;
        }
    }
    public void AutoScratchCard(System.Collections.Generic.List<Vector2> pos, Action callBack)
    {
        StartCoroutine(SetPos(pos, callBack));
    }
    IEnumerator SetPos(System.Collections.Generic.List<Vector2> pos, Action callBack)
    {
        System.Collections.Generic.List<Vector2> curPos = pos;
        BrushScale = new Vector2(2f, 2f);
        eraseStartPositions[0] = curPos[0];
      
            GameMain.Ins.audioMgr.Play(MusicPath.ScratchCard, false, AudioMgr.PlayType.OnlyOne, AudioMgr.AudioType.effect);
        
        var startpos = curPos[0];
        for (int i = 1; i < curPos.Count; i++)
        {
            float time = Vector2.Distance(startpos, curPos[i]) / 6500;
            DOTween.To(() => startpos, (_) =>
            {
                eraseEndPositions[0] = _;
                ScratchLine(0);
                eraseStartPositions[0] = _;
            }, curPos[i], time).SetEase(Ease.Linear);
            yield return new WaitForSeconds(time);
            startpos = curPos[i];
        }
        callBack?.Invoke();

    }
    private void CreateRenderTexture()
    {
        var renderTextureSize = new Vector2(imageSize.x / (float)RenderTextureQuality, imageSize.y / (float)RenderTextureQuality);
        RenderTexture = new RenderTexture((int)renderTextureSize.x, (int)renderTextureSize.y, 0, RenderTextureFormat.ARGB32);
        RenderTexture.Create();
        ScratchSurface.SetTexture(MaskTexProperty, RenderTexture);
        Progress.SetTexture(MainTexProperty, RenderTexture);
        if (Progress.HasProperty(SourceTexProperty))
        {
            Progress.SetTexture(SourceTexProperty, ScratchSurface.mainTexture);
        }
        rti = new RenderTargetIdentifier(RenderTexture);
    }

    private void GetScratchBounds()
    {
        scratchRenderer = Surface.GetComponent<Renderer>();
        rectTransform = Surface.GetComponent<RectTransform>();
        if (scratchRenderer != null)
        {
            imageSize = new Vector2(scratchRenderer.sharedMaterial.mainTexture.width, scratchRenderer.sharedMaterial.mainTexture.height);
            boundsSize = scratchRenderer.bounds.size;
            halfBoundsSize = boundsSize / 2f;
        }
        else if (rectTransform != null)
        {
            imageSize = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
            boundsSize = new Vector2(rectTransform.rect.size.x * rectTransform.lossyScale.x,
                                            rectTransform.rect.size.y * rectTransform.lossyScale.y);
            halfBoundsSize = boundsSize / 2f;

            var canvas = Surface.transform.GetComponentInParent<Canvas>();
            if (canvas != null)
            {
                isCanvasOverlay = canvas.renderMode == RenderMode.ScreenSpaceOverlay;
            }
        }
        else
        {
            Debug.LogError("Can't find Renderer or RectTransform Component!");
        }
    }

    private void ScratchHole()
    {
        var positionRect = new Rect(
            (erasePosition.x - 0.5f * Eraser.mainTexture.width * BrushScale.x) / imageSize.x,
            (erasePosition.y - 0.5f * Eraser.mainTexture.height * BrushScale.y) / imageSize.y,
            Eraser.mainTexture.width * BrushScale.x / imageSize.x,
            Eraser.mainTexture.height * BrushScale.y / imageSize.y
            );

        quadMesh.vertices = new[]
        {
            new Vector3(positionRect.xMin, positionRect.yMax, 0),
            new Vector3(positionRect.xMax, positionRect.yMax, 0),
            new Vector3(positionRect.xMax, positionRect.yMin, 0),
            new Vector3(positionRect.xMin, positionRect.yMin, 0)
        };

        GL.LoadOrtho();
        commandBuffer.Clear();
        commandBuffer.SetRenderTarget(rti);
        commandBuffer.DrawMesh(quadMesh, Matrix4x4.identity, Eraser);
        Graphics.ExecuteCommandBuffer(commandBuffer);
    }

    private void ScratchLine(int fingerId)
    {
        var holesCount = (int)Vector2.Distance(eraseStartPositions[fingerId], eraseEndPositions[fingerId]) / (int)RenderTextureQuality;
        var positions = new Vector3[holesCount * 4];
        var colors = new Color[holesCount * 4];
        var indices = new int[holesCount * 6];
        var uv = new Vector2[holesCount * 4];

        for (int i = 0; i < holesCount; i++)
        {
            var holePosition = eraseStartPositions[fingerId] + (eraseEndPositions[fingerId] - eraseStartPositions[fingerId]) / holesCount * i;
            var positionRect = new Rect(
                (holePosition.x - 0.5f * Eraser.mainTexture.width * BrushScale.x) / imageSize.x,
                (holePosition.y - 0.5f * Eraser.mainTexture.height * BrushScale.y) / imageSize.y,
                Eraser.mainTexture.width * BrushScale.x / imageSize.x,
                Eraser.mainTexture.height * BrushScale.y / imageSize.y
                );

            positions[i * 4 + 0] = new Vector3(positionRect.xMin, positionRect.yMax, 0);
            positions[i * 4 + 1] = new Vector3(positionRect.xMax, positionRect.yMax, 0);
            positions[i * 4 + 2] = new Vector3(positionRect.xMax, positionRect.yMin, 0);
            positions[i * 4 + 3] = new Vector3(positionRect.xMin, positionRect.yMin, 0);

            colors[i * 4 + 0] = Color.white;
            colors[i * 4 + 1] = Color.white;
            colors[i * 4 + 2] = Color.white;
            colors[i * 4 + 3] = Color.white;

            uv[i * 4 + 0] = Vector2.up;
            uv[i * 4 + 1] = Vector2.one;
            uv[i * 4 + 2] = Vector2.right;
            uv[i * 4 + 3] = Vector2.zero;

            indices[i * 6 + 0] = 0 + i * 4;
            indices[i * 6 + 1] = 1 + i * 4;
            indices[i * 6 + 2] = 2 + i * 4;
            indices[i * 6 + 3] = 2 + i * 4;
            indices[i * 6 + 4] = 3 + i * 4;
            indices[i * 6 + 5] = 0 + i * 4;
        }

        if (positions.Length > 0)
        {
            if (mesh != null)
            {
                mesh.Clear(false);
            }
            else
            {
                mesh = new Mesh();
            }

            mesh.vertices = positions;
            mesh.uv = uv;
            mesh.triangles = indices;
            mesh.colors = colors;
            GL.LoadOrtho();
            commandBuffer.Clear();
            commandBuffer.SetRenderTarget(rti);
            commandBuffer.DrawMesh(mesh, Matrix4x4.identity, Eraser);
            Graphics.ExecuteCommandBuffer(commandBuffer);
        }
    }

    public void FillInstantly()
    {
        commandBuffer.SetRenderTarget(rti);
        commandBuffer.ClearRenderTarget(false, true, Color.white);
        Graphics.ExecuteCommandBuffer(commandBuffer);
    }

    public void ClearInstantly()
    {
        commandBuffer.SetRenderTarget(rti);
        commandBuffer.ClearRenderTarget(false, true, Color.clear);
        Graphics.ExecuteCommandBuffer(commandBuffer);
    }

    public void Clear()
    {
        isFirstFrame = true;
    }

    public void Reset()
    {
        CreateRenderTexture();
        isFirstFrame = true;
    }
}