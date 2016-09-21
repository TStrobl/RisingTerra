using UnityEngine;
using System.Collections;

public interface IPlayerItem
{
    /// <summary>
    /// Das Item, das der Spieler gerade hält
    /// </summary>
    Enums.ItemType ItemType { get; }

    /// <summary>
    /// Der typ des Items, z.B. eine Spitzhacke
    /// </summary>
    Enums.ItemClass ItemClass { get; }

    /// <summary>
    /// Lädt den Animator für dieses Item
    /// </summary>
    void LoadAnimator();
}
