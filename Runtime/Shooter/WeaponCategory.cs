using UnityEngine;

namespace BaseTool.Shooter
{
    [CreateAssetMenu(fileName = "New Weapon Category", menuName = "BaseTool/Shooter/Weapon Category")]
    public class WeaponCategory : ScriptableObject
    {
        /// <summary>
        /// Weapon category displaying name (for UI purpose e.g.).
        /// </summary>
        [Header("Informations")]
        public string Title;

        /// <summary>
        /// Weapon category description (for UI purpose e.g.).
        /// </summary>
        [TextArea(2, 5)]
        public string Description;

        /// <summary>
        /// Weapon category icon (for UI purpose e.g.).
        /// </summary>
        public Sprite Icon;
    }
}
