using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepMeth : MonoBehaviour
{
   public void PlayFootstep()
   {
        GetComponent<AudioSource>().Play();
   }

}
