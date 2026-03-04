using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ActivatePostProcessing : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Volume postProcessor;

    private UniversalRenderPipelineAsset universalRenderPipelineAsset;
    private FullScreenPassRendererFeature targetFeature;

    void Start()
    {
        universalRenderPipelineAsset = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;

        if (universalRenderPipelineAsset == null) return;

        var rendererData = universalRenderPipelineAsset.rendererDataList[0];

        foreach (var feature in rendererData.rendererFeatures)
        {
            if (feature is FullScreenPassRendererFeature f)
            {
                targetFeature = f;
                feature.SetActive(true);
            }
        }

        if (postProcessor != null)
        {
            postProcessor.enabled = true;
        }

#if UNITY_EDITOR
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
#endif
    }

#if UNITY_EDITOR
    private void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            if (targetFeature != null)
            {
                targetFeature.SetActive(false);
            }

            if (postProcessor != null)
            {
                postProcessor.enabled = false;
            }
        }
    }

    private void OnDestroy()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeChanged;
    }
#endif
}