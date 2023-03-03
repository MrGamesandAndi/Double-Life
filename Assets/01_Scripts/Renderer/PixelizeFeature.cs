using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PixelizeFeature : ScriptableRendererFeature
{
    [Serializable]
    public class CustomPassSettings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        public int screenHeight = 144;
    }

    [SerializeField] CustomPassSettings _settings;
    PixelizePass _customPass;

    public override void Create()
    {
        _customPass = new PixelizePass(_settings);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
#if UNITY_EDITOR
        if (renderingData.cameraData.isSceneViewCamera)
        {
            return;
        }
#endif
        renderer.EnqueuePass(_customPass);
    }
}