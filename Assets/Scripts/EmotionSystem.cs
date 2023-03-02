using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionSystem : MonoBehaviour
{
    public enum EmotionKind
    {
        Default,
        Angry,
        Sad,
        Smile
    }

    public enum TransitionState
    {
        FadeOut,
        FadeIn,
        Completed,
    }

    public RawImage view;
    public Texture2D texDefault;
    public Texture2D texAngry;
    public Texture2D texSad;
    public Texture2D texSmile;
    public float transitionScale;

    Dictionary<EmotionKind, Texture2D> tex;
    Queue<EmotionKind> queue;
    TransitionState transitionState;
    EmotionKind fromEmotionKind;
    EmotionKind toEmotionKind;

    void Start()
    {
        tex = new Dictionary<EmotionKind, Texture2D>
        {
            { EmotionKind.Default, texDefault },
            { EmotionKind.Angry, texAngry },
            { EmotionKind.Sad, texSad },
            { EmotionKind.Smile, texSmile }
        };

        view.color = Color.clear;

        queue = new Queue<EmotionKind>();
    }

    void Update()
    {
        if (0 < queue.Count && transitionState == TransitionState.Completed)
        {
            transitionState = TransitionState.FadeOut;
            fromEmotionKind = toEmotionKind;
            toEmotionKind = queue.Dequeue();

            if (fromEmotionKind == toEmotionKind)
            {
                transitionState = TransitionState.Completed;
            }
        }

        if (transitionState == TransitionState.FadeOut)
        {
            var alpha = Mathf.Clamp01(view.color.a - Time.deltaTime * transitionScale);
            view.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            if (alpha == 0.0f)
            {
                view.texture = tex[toEmotionKind];
                transitionState = TransitionState.FadeIn;
            }
        }

        if (transitionState == TransitionState.FadeIn)
        {
            var alpha = Mathf.Clamp01(view.color.a + Time.deltaTime * transitionScale);
            view.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            if (alpha == 1.0f)
            {
                transitionState = TransitionState.Completed;
            }
        }
    }

    public void SetEmotion(EmotionKind emotion)
    {
        queue.Enqueue(emotion);
    }
}
