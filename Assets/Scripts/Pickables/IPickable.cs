using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable 
{
    public void Pickup(Player player);
    public void SetAsPickable();

    public void SetAsNotPickable();
}
