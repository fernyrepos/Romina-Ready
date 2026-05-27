using System;
using UnityEngine;
using Verse;

namespace RominaReady
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class HotSwappableAttribute : Attribute
    {
    }
    [HotSwappable]
    public class Window_ReadyButton : Window
    {
        private bool fadingOut;
        private float fadeStartTime;
        private float fadeAlpha = 1f;
        private const float FadeDuration = 3f;
        public override Vector2 InitialSize => new Vector2(90f + Margin, 35f + Margin);
        public Window_ReadyButton()
        {
            draggable = true;
            closeOnClickedOutside = false;
            preventCameraMotion = false;
            doCloseButton = false;
            doCloseX = false;
            resizeable = false;
            forcePause = false;
            absorbInputAroundWindow = false;
            focusWhenOpened = false;
            closeOnCancel = false;
            doWindowBackground = false;
            drawShadow = false;
            layer = WindowLayer.GameUI;
        }

        public override void SetInitialSizeAndPosition()
        {
            windowRect = new Rect(
                UI.screenWidth - InitialSize.x - 20f,
                20f,
                InitialSize.x,
                InitialSize.y);
        }

        public override void DoWindowContents(Rect inRect)
        {
            if (fadingOut)
            {
                var elapsed = Time.realtimeSinceStartup - fadeStartTime;
                fadeAlpha = Mathf.Clamp01(1f - elapsed / FadeDuration);
            }

            var prev = GUI.color;
            GUI.color = fadingOut ? new Color(0.5f, 0.5f, 0.5f, fadeAlpha) : Color.white;

            var label = State.hasHadFirstThreat ? "RR_ReadyForMore".Translate() : "RR_ReadyForThreats".Translate();
            if (Widgets.ButtonText(inRect, label, active: fadingOut is false))
            {
                fadingOut = true;
                fadeStartTime = Time.realtimeSinceStartup;
            }

            GUI.color = prev;

            if (fadingOut && fadeAlpha <= 0f)
            {
                State.OnButtonClicked();
                Close();
            }
        }
    }
}
