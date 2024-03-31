using System;
using UnityEngine;
using Verse;

namespace ColonistsDeco;

public class Dialog_Confirm : Dialog_MessageBox
{
    private const float TitleHeight = 42f;

    private const float DialogWidth = 500f;

    private const float DialogHeight = 300f;

    public Dialog_Confirm(string text, Action confirmedAct = null, bool destructive = false, string title = null)
        : base(text, "Confirm".Translate(), confirmedAct, "GoBack".Translate(), null, title, destructive)
    {
        closeOnCancel = false;
        closeOnAccept = false;
    }

    public override Vector2 InitialSize
    {
        get
        {
            var num = 300f;
            if (title != null)
            {
                num += 42f;
            }

            return new Vector2(500f, num);
        }
    }

    public override void DoWindowContents(Rect inRect)
    {
        base.DoWindowContents(inRect);
        if (Event.current.type != EventType.KeyDown)
        {
            return;
        }

        switch (Event.current.keyCode)
        {
            case KeyCode.Return:
            case KeyCode.KeypadEnter:
            {
                buttonAAction?.Invoke();

                Close();
                break;
            }
            case KeyCode.Escape:
            {
                buttonBAction?.Invoke();

                Close();
                break;
            }
        }
    }
}