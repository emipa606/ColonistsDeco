﻿using UnityEngine;
using Verse;

namespace ColonistsDeco;

public class Dialog_Inspect : Window
{
    private const float TitleHeight = 42f;

    private const float DialogWidth = 500f;

    private const float DialogHeight = 600f;

    private readonly Texture2D image;
    private readonly TaggedString text;

    private readonly string title;

    private Vector2 scrollPosition = Vector2.zero;

    public Dialog_Inspect(string text, Texture2D image, string title = null)
    {
        this.text = text;
        this.image = image;
        this.title = title;
        if (text.NullOrEmpty())
        {
            this.text = "Null";
        }

        forcePause = false;
        draggable = true;
        resizeable = true;
        preventCameraMotion = false;
        absorbInputAroundWindow = false;
        doCloseX = true;
        closeOnClickedOutside = true;
    }

    public override Vector2 InitialSize
    {
        get
        {
            var num = DialogHeight;
            if (title != null)
            {
                num += TitleHeight;
            }

            return new Vector2(DialogWidth, num);
        }
    }

    public override void DoWindowContents(Rect inRect)
    {
        var num = inRect.y;
        if (!title.NullOrEmpty())
        {
            Text.Font = GameFont.Medium;
            Widgets.Label(new Rect(0f, num, inRect.width, TitleHeight), title);
            num += TitleHeight;
        }

        Text.Font = GameFont.Small;
        var outRect = new Rect(inRect.x, num, inRect.width, inRect.height - 42f - num);
        var width = outRect.width - 16f;
        var viewRect = new Rect(0f, 0f, width, Text.CalcHeight(text, width));
        Widgets.BeginScrollView(outRect, ref scrollPosition, viewRect);
        Widgets.Label(new Rect(0f, 0f, viewRect.width, viewRect.height), text);
        Widgets.EndScrollView();

        Widgets.DrawTextureFitted(outRect, image, 1f);
    }
}