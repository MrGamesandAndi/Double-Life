namespace Yarn.Unity.Generated
{
    [System.CodeDom.Compiler.GeneratedCode("YarnActionAnalyzer", "1.0.0.0")]
    public class ActionRegistration
    {
#if UNITY_EDITOR
        [global::UnityEditor.InitializeOnLoadMethod]
#endif
        [global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AddRegisterFunction()
        {
            Actions.AddRegistrationMethod(RegisterActions);
        }

        [System.CodeDom.Compiler.GeneratedCode("YarnActionAnalyzer", "1.0.0.0")]
        public static void RegisterActions(global::Yarn.Unity.IActionRegistration target)
        {
            // Actions from file:
            // Assets\01_Scripts\RelationshipSystem\RelationshipSystem.cs
            target.AddCommandHandler("give_new_friend", typeof(global::Relationships.RelationshipSystem).GetMethod(nameof(global::Relationships.RelationshipSystem.StartNewFriendWithRandom), new System.Type[]{}));
            target.AddCommandHandler("break_up", typeof(global::Relationships.RelationshipSystem).GetMethod(nameof(global::Relationships.RelationshipSystem.BreakUp), new System.Type[]{}));
            target.AddCommandHandler("go_to_date", typeof(global::Relationships.RelationshipSystem).GetMethod(nameof(global::Relationships.RelationshipSystem.GoToDate), new System.Type[]{}));
            target.AddCommandHandler("reset_need", typeof(global::Relationships.RelationshipSystem).GetMethod(nameof(global::Relationships.RelationshipSystem.ResetNeed), new System.Type[]{typeof(int)}));
            target.AddCommandHandler("confess_love", typeof(global::Relationships.RelationshipSystem).GetMethod(nameof(global::Relationships.RelationshipSystem.TryConfessLove), new System.Type[]{}));
        }
    }
}