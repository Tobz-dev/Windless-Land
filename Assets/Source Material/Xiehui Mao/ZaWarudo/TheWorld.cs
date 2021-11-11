using UnityEngine;
using UnityEngine.SocialPlatforms;
using DG.Tweening;
using System.Collections;

namespace UnityStandardAssets.ImageEffects
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]

    class TheWorld : PostEffectsBase { 

        public Vector2 center;
        [Range(0, 2)] public float radius;
        public Color impactColor;
        [Range(0, 2)] public float impactRadius;
        [Range(0, 2)] public float impactRadius1;
        public bool isGray;
        public float wave_intensity;
        public float wave_shape;
        public float twist_intensity;
        public float twist_speed;
        public float blur_intensity;
        public float blur_radius;
        public Shader colorShader = null;
        public Shader blurShader = null;
        private Material colorMaterial = null;
        private Material blurMaterial = null;
        public Texture2D tex;
        // you can set it to null to see how it works with the scene
        public Texture2D testImage = null;
       

        RenderTexture mySource;
        public override bool CheckResources()
        {
            CheckSupport(true);

            colorMaterial = CheckShaderAndCreateMaterial(colorShader, colorMaterial);
            blurMaterial = CheckShaderAndCreateMaterial(blurShader, blurMaterial);
            colorMaterial.SetTexture("_NoiseTex", tex);
            if (!isSupported)
                ReportAutoDisable();
            return isSupported;
        }

        IEnumerator TimeStop() {
            //long ugly brute animate
            DOTween.To(() => impactRadius, x => impactRadius = x, 2, 1f).SetEase(Ease.InCubic);
            yield return new WaitForSeconds(0.05f);
            DOTween.To(() => radius, x => radius = x, 2, 0.5f).SetEase(Ease.InCubic);
            yield return new WaitForSeconds(0.1f);
            DOTween.To(() => impactRadius1, x => impactRadius1 = x, 2, 0.5f).SetEase(Ease.InCubic);
            Tween t = DOTween.To(() => blur_intensity, x => blur_intensity = x, 1.5f, 0.2f).SetEase(Ease.InCubic);
            t.onComplete = delegate { DOTween.To(() => blur_intensity, x => blur_intensity = x, 0f, 0.2f).SetEase(Ease.InCubic); };
            yield return new WaitForSeconds(0.4f);
            Tween t1 = DOTween.To(() => blur_intensity, x => blur_intensity = x, 1.3f, 0.2f).SetEase(Ease.InCubic);
            t1.onComplete = delegate { DOTween.To(() => blur_intensity, x => blur_intensity = x, 0f, 0.2f).SetEase(Ease.InCubic); };
            yield return new WaitForSeconds(0.4f);
            isGray = true;
            Tween t2 = DOTween.To(() => blur_intensity, x => blur_intensity = x, 1.2f, 0.2f).SetEase(Ease.InCubic);
            t2.onComplete = delegate { DOTween.To(() => blur_intensity, x => blur_intensity = x, 0f, 0.2f).SetEase(Ease.InCubic); };
            yield return new WaitForSeconds(0.4f);
            Tween t3 = DOTween.To(() => blur_intensity, x => blur_intensity = x, 1.4f, 0.2f).SetEase(Ease.InCubic);
            t3.onComplete = delegate { DOTween.To(() => blur_intensity, x => blur_intensity = x, 0f, 0.2f).SetEase(Ease.InCubic); };
            DOTween.To(() => radius, x => radius = x, 0, 0.5f).SetEase(Ease.InCubic);
            DOTween.To(() => impactRadius, x => impactRadius = x, 0, 0.5f).SetEase(Ease.InCubic);
            DOTween.To(() => impactRadius, x => impactRadius = x, 0, 0.6f).SetEase(Ease.InCubic);
        }

        void Awake (){
            if (testImage) {
                mySource = new RenderTexture(testImage.width / 2, testImage.height / 2, 0);
                RenderTexture.active = mySource;
                Graphics.Blit(testImage, mySource);
            }        
            StartCoroutine(TimeStop());
           
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (CheckResources() == false)
            {
                Graphics.Blit(source, destination);
                return;
            }

            blurMaterial.SetFloat("_SampleStrength", blur_intensity);
            blurMaterial.SetFloat("_SampleDist", blur_radius);
            blurMaterial.SetFloat("_CenterX", center.x);
            blurMaterial.SetFloat("_CenterY", center.y);
            
           

            colorMaterial.SetFloat("_Radius", radius);
            colorMaterial.SetColor("_ImpactColor", impactColor);
            colorMaterial.SetFloat("_ImpactRadius", impactRadius);
            colorMaterial.SetFloat("_ImpactRadius1", impactRadius1);
            colorMaterial.SetFloat("_CenterX", center.x);       
            colorMaterial.SetFloat("_CenterY", center.y);
            colorMaterial.SetFloat("_TwistIntensity", twist_intensity);
            colorMaterial.SetFloat("_TwistSpeed", twist_speed);
            colorMaterial.SetFloat("_WaveIntensity", wave_intensity);
            colorMaterial.SetFloat("_WaveShape", wave_shape);
            float gv = isGray ? 1 : 0;
            colorMaterial.SetFloat("_Gray", gv);
            RenderTexture rt = RenderTexture.GetTemporary(source.width, source.height);
            if (testImage)
            {
                Graphics.Blit(mySource, rt, colorMaterial);
            }
            else {
                Graphics.Blit(source, rt, colorMaterial);
            }
            Graphics.Blit(rt, destination, blurMaterial);
            RenderTexture.ReleaseTemporary(rt);
        }
    }
}