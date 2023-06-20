using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    public Team GetTeam();
    public void DealDamage(float damage);
}
