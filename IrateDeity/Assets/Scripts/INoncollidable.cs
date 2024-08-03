using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface INoncollidable
{
    //All Game Objects with this interface must have a box collider2D component
    //Also include System.Linq; when copying IgnoreCollision Methods
    public void IgnoreCollision();
}
