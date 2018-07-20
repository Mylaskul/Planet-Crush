using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MetaData {

    private static float bgvolume = 0.5f; 
    private static float evolume = 0.5f;

    public static float BackgroundVolume
    {
        get
        {
            return bgvolume;
        }
        set
        {
            bgvolume = value;
        }
    }

    public static float EffectVolume
    {
        get
        {
            return evolume;
        }
        set
        {
            evolume = value;
        }
    }

}
