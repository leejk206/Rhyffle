using UnityEngine;

public class ScoreManager
{
    public int totalScore;
    private int _baseScorePerNote;
    private int _remainderFirstNote;

    public Define.GameMode CurrentMode { get; set; } = Define.GameMode.Plain;
    
    public void Init(float totalNoteCount) // 점수 시스템 초기화
    {
        float rawScore = 1000000f / totalNoteCount;
        _baseScorePerNote = Mathf.RoundToInt(rawScore);
        _remainderFirstNote = Mathf.RoundToInt(1000000 - _baseScorePerNote * (totalNoteCount - 1));
        totalScore = 0;
    }
    
    public void ApplyNoteScore(int noteIndex, Define.JudgementType judgement, int cardBonus) // 노트 하나 점수 계산하여 totalScore에 반영 
    {
        int baseScore = (noteIndex == 0) ? _remainderFirstNote : _baseScorePerNote;
        float multiplier = GetJudgementMultiplier(judgement); // 판정 배율

        // 노트 점수 + 카드 점수) * 판정 배율
        int totalNoteScore = Mathf.RoundToInt((baseScore + (CurrentMode == Define.GameMode.Challenge ? cardBonus : 0)) * multiplier);
        totalScore += totalNoteScore;
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
