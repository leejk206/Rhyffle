using UnityEngine;
using static Define;

public class ScoreManager
{
    public int totalScore;
    private int _baseScorePerNote;
    private int _remainderFirstNote;

    public float CurrentMultiflier; // 카드 효과에 의한 배율

    public bool isJudgementSetted;

    public Define.GameMode CurrentMode { get; set; } = Define.GameMode.Plain;
    
    public void Init(float totalNoteCount) // 점수 시스템 초기화
    {
        float rawScore = 1000000f / totalNoteCount;
        _baseScorePerNote = Mathf.RoundToInt(rawScore);
        _remainderFirstNote = Mathf.RoundToInt(1000000 - _baseScorePerNote * (totalNoteCount - 1));
        totalScore = 0;
        CurrentMultiflier = 1;
        isJudgementSetted = false;
    }
    
    public void ApplyNoteScore(bool first, Define.JudgementType judgement, int rank, float scale) // 노트 하나 점수 계산하여 totalScore에 반영 
    {
        int baseScore = (first) ? _remainderFirstNote : _baseScorePerNote;

        // 점수 계산 컨텍스트 생성
        ScoreContext ctx = new ScoreContext
        {
            NoteIndex = 0,
            Judgement = judgement,
            BaseScore = baseScore,
            CardBonus = 1,
            JudgementMultiplier = 1,
            CardMultiplier = CurrentMultiflier,
        };

        // 모든 Brand/Effect에 훅 호출 (낙인/효과가 점수에 개입)
        if (Managers.Effect.Brands != null)
        {
            foreach (var brand in Managers.Effect.Brands)
            {
                brand.OnBeforeScoreApply(ctx);
            }
        }

        if (Managers.Effect.Effects != null)
        {
            foreach (var effect in Managers.Effect.Effects)
            {
                effect.OnBeforeScoreApply(ctx);
            }
        }

        // 최종 점수 계산
        int totalNoteScore = Mathf.RoundToInt(
            /*(ctx.BaseScore + (CurrentMode == Define.GameMode.Challenge ? ctx.CardBonus : 0))
            * ctx.JudgementMultiplier
            * ctx.CardMultiplier*/
            (baseScore + (CurrentMode == Define.GameMode.Challenge ? 1 :0)  * (rank)) * scale
        );

        ctx.FinalScore = totalNoteScore;
        totalScore += ctx.FinalScore;
    }
    
    public void ApplyPresetNoteScore(int noteIndex, Define.JudgementType judgement, int cardBonus)
    {

    }

    public int GetFinalScore(float patternMultiplier) // 최종 점수를 반환 (*족보점수)
    {
        return Mathf.RoundToInt(totalScore * patternMultiplier);
    }
    
    public int GetTotalScore() // 현재 점수 반환
    {
        return totalScore;
    }
    
    private float GetJudgementMultiplier(Define.JudgementType judgement) // 판정에 따른 점수 배율
    {
        return judgement switch
        {
            Define.JudgementType.Perfect => 1.2f,
            Define.JudgementType.Great => 1.0f,
            Define.JudgementType.Good => 0.7f,
            Define.JudgementType.Miss => 0f,
            _ => 0f
        };
    }
    
    public void ResetScore() // 점수 초기화
    {
        totalScore = 0;
    }
}

public class ScoreContext
{
    public bool First;
    public int NoteIndex;
    public JudgementType Judgement;
    public int BaseScore;
    public float CardBonus;
    public float JudgementMultiplier;
    public float CardMultiplier;

    public int FinalScore;
}