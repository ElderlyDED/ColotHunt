using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace YG 
{
    public class BannerYG : MonoBehaviour
    {
        public enum RTBNumber { One, Two, Three, Four };
        [Tooltip("Всего доступно четыре баннера. Выберите номер данного баннера")]
        public RTBNumber RTB_Number;
        public enum Device { desktopAndMobile, onlyDesktop, onlyMobile };
        public Device device;
        public float minWidth = 20f, minHeight = 20f;

        RectTransform rt;

        static float timerRBT1 = 31f;
        static float timerRBT2 = 31f;
        static float timerRBT3 = 31f;
        static float timerRBT4 = 31f;

        private void Awake()
        {
#if !UNITY_EDITOR
            rt = transform.GetChild(0).GetComponent<RectTransform>();
            rt.GetComponent<RawImage>().color = Color.clear;
#endif
        }

#if !UNITY_EDITOR
        Vector2 ScreenSize;

        private void OnEnable()
        {
            YandexGame.GetDataEvent += ActivateRTB;
            YandexGame.OpenFullAdEvent += DeactivateRTB;
            YandexGame.CloseFullAdEvent += ActivateRTB;
            YandexGame.OpenVideoEvent += DeactivateRTB;
            YandexGame.CloseVideoEvent += ActivateRTB;
            YandexGame.CheaterVideoEvent += ActivateRTB;

            if (YandexGame.SDKEnabled)
                ActivateRTB();
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= ActivateRTB;
            YandexGame.OpenFullAdEvent -= DeactivateRTB;
            YandexGame.CloseFullAdEvent -= ActivateRTB;
            YandexGame.OpenVideoEvent -= DeactivateRTB;
            YandexGame.CloseVideoEvent -= ActivateRTB;
            YandexGame.CheaterVideoEvent -= ActivateRTB;

            DeactivateRTB();
        }

        private void Update()
        {
            if (CheckDevice())
            {
                if (ScreenSize.x != Screen.width || ScreenSize.y != Screen.height)
                {
                    RecalculateRect();
                }

                ScreenSize.x = Screen.width;
                ScreenSize.y = Screen.height;

                // Обновление RTB-блоков
                if (YandexGame.SDKEnabled)
                {
                    if (RTB_Number == RTBNumber.One)
                    {
                        timerRBT1 += Time.unscaledDeltaTime;

                        if (timerRBT1 >= 31)
                        {
                            timerRBT1 = 0;
                            Invoke("RenderRTB1", 0.01f);
                        }
                    }
                    else if (RTB_Number == RTBNumber.Two)
                    {
                        timerRBT2 += Time.unscaledDeltaTime;

                        if (timerRBT2 >= 31)
                        {
                            timerRBT2 = 0;
                            Invoke("RenderRTB2", 0.01f);
                        }
                    }
                    else if (RTB_Number == RTBNumber.Three)
                    {
                        timerRBT3 += Time.unscaledDeltaTime;

                        if (timerRBT3 >= 31)
                        {
                            timerRBT3 = 0;
                            Invoke("RenderRTB3", 0.01f);
                        }
                    }
                    else if (RTB_Number == RTBNumber.Four)
                    {
                        timerRBT4 += Time.unscaledDeltaTime;

                        if (timerRBT4 >= 31)
                        {
                            timerRBT4 = 0;
                            Invoke("RenderRTB4", 0.01f);
                        }
                    }
                }
            }
        }
#endif

        public void RecalculateRect()
        {
            if (CheckDevice())
            {
                if (!rt)
                    rt = transform.GetChild(0).GetComponent<RectTransform>();

                float width = rt.rect.width;
                float height = rt.rect.height;

                if (width < minWidth) width = minWidth;
                if (height < minHeight) height = minHeight;

                string _width = width.ToString() + "px";
                string _height = height.ToString() + "px";

                string _left = "50%";
                string _right = "0px";
                string _top = "50%";
                string _bottom = "0px";

                string _offsetX = rt.localPosition.x.ToString();
                string _offsetY = (-rt.localPosition.y).ToString();
                string _offset = $"translate({_offsetX.Replace(",", ".")}px, {_offsetY.Replace(",", ".")}px)";

                if (RTB_Number == RTBNumber.One)
                {
                    RecalculateRTB1(
                        _width.Replace(",", "."),
                        _height.Replace(",", "."),
                        _left.Replace(",", "."),
                        _right.Replace(",", "."),
                        _top.Replace(",", "."),
                        _bottom.Replace(",", "."),
                        _offset);
                }
                else if (RTB_Number == RTBNumber.Two)
                {
                    RecalculateRTB2(
                        _width.Replace(",", "."),
                        _height.Replace(",", "."),
                        _left.Replace(",", "."),
                        _right.Replace(",", "."),
                        _top.Replace(",", "."),
                        _bottom.Replace(",", "."),
                        _offset);
                }
                else if (RTB_Number == RTBNumber.Three)
                {
                    RecalculateRTB3(
                        _width.Replace(",", "."),
                        _height.Replace(",", "."),
                        _left.Replace(",", "."),
                        _right.Replace(",", "."),
                        _top.Replace(",", "."),
                        _bottom.Replace(",", "."),
                        _offset);
                }
                else if (RTB_Number == RTBNumber.Four)
                {
                    RecalculateRTB4(
                        _width.Replace(",", "."),
                        _height.Replace(",", "."),
                        _left.Replace(",", "."),
                        _right.Replace(",", "."),
                        _top.Replace(",", "."),
                        _bottom.Replace(",", "."),
                        _offset);
                }
            }
        }

        public void ActivityRTB(bool state)
        {
            if (CheckDevice())
            {
                if (RTB_Number == RTBNumber.One)
                {
                    ActivityRTB1(state);
                }
                else if (RTB_Number == RTBNumber.Two)
                {
                    ActivityRTB2(state);
                }
                else if (RTB_Number == RTBNumber.Three)
                {
                    ActivityRTB3(state);
                }
                else if (RTB_Number == RTBNumber.Four)
                {
                    ActivityRTB4(state);
                }

                if (state)
                    RecalculateRect();
            }
        }

        void ActivateRTB() 
        { 
            if (!YandexGame.nowFullAd && !YandexGame.nowVideoAd)
                ActivityRTB(true);
        }
        void ActivateRTB(int id) => ActivateRTB();
        void DeactivateRTB() => ActivityRTB(false);
        void DeactivateRTB(int id) => DeactivateRTB();

        [DllImport("__Internal")]
        private static extern void RecalculateRTB1(
            string width,
            string height,
            string left,
            string right,
            string top,
            string bottom,
            string offset);

        [DllImport("__Internal")]
        private static extern void RecalculateRTB2(
            string width,
            string height,
            string left,
            string right,
            string top,
            string bottom,
            string offset);

        [DllImport("__Internal")]
        private static extern void RecalculateRTB3(
            string width,
            string height,
            string left,
            string right,
            string top,
            string bottom,
            string offset);

        [DllImport("__Internal")]
        private static extern void RecalculateRTB4(
            string width,
            string height,
            string left,
            string right,
            string top,
            string bottom,
            string offset);


        [DllImport("__Internal")]
        private static extern void ActivityRTB1(bool state);

        [DllImport("__Internal")]
        private static extern void ActivityRTB2(bool state);

        [DllImport("__Internal")]
        private static extern void ActivityRTB3(bool state);

        [DllImport("__Internal")]
        private static extern void ActivityRTB4(bool state);


        [DllImport("__Internal")]
        private static extern void RenderRTB1();

        [DllImport("__Internal")]
        private static extern void RenderRTB2();

        [DllImport("__Internal")]
        private static extern void RenderRTB3();

        [DllImport("__Internal")]
        private static extern void RenderRTB4();

        bool CheckDevice()
        {
            bool result = true;

            if (device == Device.onlyDesktop)
            {
                if (YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet)
                    result = false;
            }
            else if (device == Device.onlyMobile)
            {
                if (!YandexGame.EnvironmentData.isMobile && !YandexGame.EnvironmentData.isTablet)
                    result = false;
            }

            return result;
        }
    }
}
