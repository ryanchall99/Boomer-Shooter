using UnityEngine;

public class PlayerHealth : BaseHealth
{
    public override void Death()
    {
        Destroy(this.gameObject);
    }

}
