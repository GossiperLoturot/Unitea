using System.Collections.Generic;
using UnityEngine;

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

    public SpriteRenderer spriteDefault;
    public SpriteRenderer spriteAngry;
    public SpriteRenderer spriteSad;
    public SpriteRenderer spriteSmile;
    public float transitionScale;

    Dictionary<EmotionKind, SpriteRenderer> sprites;
    Queue<EmotionKind> queue;
    TransitionState transitionState;
    EmotionKind fromEmotionKind;
    EmotionKind toEmotionKind;

    void Start()
    {
        sprites = new Dictionary<EmotionKind, SpriteRenderer>
        {
            { EmotionKind.Default, spriteDefault },
            { EmotionKind.Angry, spriteAngry },
            { EmotionKind.Sad, spriteSad },
            { EmotionKind.Smile, spriteSmile }
        };

        foreach (var sprite in sprites.Values)
        {
            sprite.color = Color.clear;
        }

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
            var alpha = Mathf.Clamp01(sprites[fromEmotionKind].color.a - Time.deltaTime * transitionScale);
            sprites[fromEmotionKind].color = new Color(1.0f, 1.0f, 1.0f, alpha);
            if (alpha == 0.0f)
            {
                transitionState = TransitionState.FadeIn;
            }
        }

        if (transitionState == TransitionState.FadeIn)
        {
            var alpha = Mathf.Clamp01(sprites[toEmotionKind].color.a + Time.deltaTime * transitionScale);
            sprites[toEmotionKind].color = new Color(1.0f, 1.0f, 1.0f, alpha);
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
