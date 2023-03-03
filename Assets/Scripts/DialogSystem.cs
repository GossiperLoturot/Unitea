using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public Text text;
    public EmotionSystem emotionSystem;

    int state;

    void Start()
    {
        SetState(0);
    }

    public void Submit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (state)
            {
                case 0:
                    SetState(1);
                    break;

                case 1:
                    SetState(4);
                    break;

                case 2:
                    SetState(5);
                    break;

                case 3:
                    SetState(6);
                    break;

                case 4:
                    SetState(1);
                    break;

                case 5:
                    SetState(1);
                    break;

                case 6:
                    SetState(1);
                    break;
            }
        }
    }

    public void SelectNext(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (state)
            {
                case 1:
                    SetState(2);
                    break;

                case 2:
                    SetState(3);
                    break;

                case 3:
                    SetState(1);
                    break;
            }
        }
    }

    public void SelectPrev(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (state)
            {
                case 1:
                    SetState(3);
                    break;

                case 2:
                    SetState(1);
                    break;

                case 3:
                    SetState(2);
                    break;
            }
        }
    }

    private void SetState(int state)
    {
        switch (state)
        {
            case 0:
                text.text = "こんにちは！";
                emotionSystem.SetEmotion(EmotionSystem.EmotionKind.Default);
                break;

            case 1:
                text.text = "どうしたの？\n> 怒って！\n泣いて！\n笑って！";
                emotionSystem.SetEmotion(EmotionSystem.EmotionKind.Default);
                break;

            case 2:
                text.text = "どうしたの？\n怒って！\n> 泣いて！\n笑って！";
                emotionSystem.SetEmotion(EmotionSystem.EmotionKind.Default);
                break;

            case 3:
                text.text = "どうしたの？\n怒って！\n泣いて！\n> 笑って！";
                emotionSystem.SetEmotion(EmotionSystem.EmotionKind.Default);
                break;

            case 4:
                text.text = "は？";
                emotionSystem.SetEmotion(EmotionSystem.EmotionKind.Angry);
                break;

            case 5:
                text.text = "うぅ...";
                emotionSystem.SetEmotion(EmotionSystem.EmotionKind.Sad);
                break;

            case 6:
                text.text = "へへっ！";
                emotionSystem.SetEmotion(EmotionSystem.EmotionKind.Smile);
                break;
        }

        this.state = state;
    }
}
