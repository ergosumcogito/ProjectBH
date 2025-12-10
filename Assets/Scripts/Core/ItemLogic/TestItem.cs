using Core.Enemy_Logic;
using Core.ItemLogic;
using UnityEngine;

public class TestItem : IItem , IDropable
{
    //Depending on how we want to implement Items i.e. if you pick them up with a hitbox like coins or directly give them to the player the implementation changes
    //Hitbox pickup will look similar to coins but requires to re-write the collision detection
    //Direct add requires an extra class that handles the pick-up trigger. This would allow to decouple Playerdata from the item itself
    //I'll leave it as it stands for now and the group should decide how we want it done. "Daniel"
    private int maxHealth = 100;

    public void OnPickUp(PlayerData playerData)
    {
        playerData.maxHealth += maxHealth;
        //Might need to trigger the HealthChanged event
        Debug.Log($"max health is now {playerData.maxHealth}");
    }
}
