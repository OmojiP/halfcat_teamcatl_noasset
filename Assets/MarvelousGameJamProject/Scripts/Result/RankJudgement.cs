namespace MarvelousGameJamProject.Result
{
    public class RankJudgement
    {
        /// <summary>
        /// スコアに基づいてランクを判定し、スタティック領域に保存します。
        /// </summary>
        /// <param name="score">判定するスコア</param>
        public static void JudgeRank(int score)
        {
            Rank rank;

            if (score >= 6)
            {
                rank = Rank.S;
            }
            else if (score >= 4)
            {
                rank = Rank.A;
            }
            else if (score >= 2)
            {
                rank = Rank.B;
            }
            else
            {
                rank = Rank.C;
            }

            // 判定されたランクを静的な設定に保存
            StaticSettings.CurrentRank = rank;
        }
    }
}