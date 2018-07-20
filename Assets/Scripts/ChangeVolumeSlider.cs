using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolumeSlider : MonoBehaviour {

    public void ChangeBackgroundVolume()
    {
        MetaData.BackgroundVolume = GameObject.Find("BackgroundMusicSlider").GetComponent<Slider>().value;
    }

    public void ChangeEffectVolume()
    {
        MetaData.EffectVolume = GameObject.Find("EffectSlider").GetComponent<Slider>().value;
    }

}

