using System;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

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
        public override Vector2 InitialSize => new Vector2(170f + Margin, 40f + Margin);
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
            soundClose = null;
            layer = WindowLayer.GameUI;
        }

        public override void SetInitialSizeAndPosition()
        {
            if (State.buttonWindowPosition != Vector2.zero)
            {
                windowRect = new Rect(State.buttonWindowPosition.x, State.buttonWindowPosition.y, InitialSize.x, InitialSize.y);
            }
            else
            {
                windowRect = new Rect(
                    UI.screenWidth - InitialSize.x - 20f,
                    20f,
                    InitialSize.x,
                    InitialSize.y);
            }
        }

        public override void DoWindowContents(Rect inRect)
        {
            State.buttonWindowPosition = windowRect.position;
            Text.Font = GameFont.Tiny;
            if (fadingOut)
            {
                var elapsed = Time.realtimeSinceStartup - fadeStartTime;
                fadeAlpha = Mathf.Clamp01(1f - elapsed / FadeDuration);
            }

            var prev = GUI.color;
            GUI.color = fadingOut ? new Color(0.5f, 0.5f, 0.5f, fadeAlpha) : Color.white;

            var label = State.hasHadFirstThreat ? "RR_ReadyForMore".Translate() : "RR_ReadyForThreats".Translate();
            if (fadingOut)
            {
                Widgets.DrawAtlas(inRect, Widgets.ButtonBGAtlas);
                Text.Anchor = TextAnchor.MiddleCenter;
                Widgets.Label(inRect, label);
                Text.Anchor = TextAnchor.UpperLeft;
            }
            else if (Widgets.ButtonText(inRect, label))
            {
                SoundDefOf.Click.PlayOneShotOnCamera();
                fadingOut = true;
                fadeStartTime = Time.realtimeSinceStartup;
            }

            GUI.color = prev;

            if (fadingOut && fadeAlpha <= 0f)
            {
                State.OnButtonClicked();
                Close();
            }
            Text.Font = GameFont.Small;
        }

        public override void PostClose()
        {
            State.ClearWindowReference(this);
        }
    }
}
