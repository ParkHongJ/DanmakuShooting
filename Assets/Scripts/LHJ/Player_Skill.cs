using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer_Skill
{
    GameObject getOwner();
    void setOwner(GameObject _owner);
    float getDamage();
    void setDamage(float _damage);
}
