using MarvelousGameJamProject.Result;

namespace MarvelousGameJamProject
{
    /// <summary>
    /// このクラスは、プロジェクト全体で使用される静的な設定を保持するためのクラスです。
    /// </summary>
    public static class StaticSettings
    {
        // 操作方法を反転するかどうか
        public static bool IsInvertControl { get; set; } = false;

        public static int Score { get; set; } = 0;

        // 現在のランク
        public static Rank CurrentRank { get; set; } = Rank.C;
        // 音量設定
    }
}