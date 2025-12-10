using UnityEngine;

namespace Core.ItemLogic
{


    public interface IItem
    {
        public void OnPickUp(PlayerData playerData);
        //OnCollisionEnter2D if we go the hitbox pick up route
        
    }
}
