using UnityEngine;
using Cysharp.Threading.Tasks;

public class CardFlip : MonoBehaviour
{
    // public Sprite frontSprite;
    // public Sprite backSprite;
    // public SpriteRenderer spriteRenderer;

    private bool isFront = false;
    private bool isFlipping = false;
    
    [SerializeField] private GameObject cardScaleGuide;
    private Vector3 baseScale;

    private void Start()
    {
        baseScale = cardScaleGuide != null ? cardScaleGuide.transform.localScale : transform.localScale;
    }

    public async UniTaskVoid FlipCardAsync()
    {
        if (isFlipping) return;
        isFlipping = true;

        float duration = 0.25f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float scaleY = Mathf.Lerp(baseScale.y, 0f, t);
            transform.localScale = new Vector3(baseScale.x, scaleY, baseScale.z);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        // 뒷면 스프라이트로 변경 (추후)
        isFront = !isFront;

        
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float scaleY = Mathf.Lerp(0f, baseScale.y, t);
            transform.localScale = new Vector3(baseScale.x, scaleY, baseScale.z);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        isFlipping = false;
    }
    
    void Update() // (임시) f 키 누르면 카드 뒤집히는 애니메이션 시작
    {
        if (Input.GetKeyDown(KeyCode.F)) FlipCardAsync().Forget();
    }
}