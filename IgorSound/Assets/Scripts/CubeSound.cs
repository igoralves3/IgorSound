using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Deform{
    public class CubeSound : MonoBehaviour
    {

        public AudioSource audioSource;
        private float[] spectrumData = new float[256];
        private int dir = 1;

        public GameObject Twist;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            dir = 1;
        }

        // Update is called once per frame
        void Update()
        {
            if (audioSource.isPlaying) {

                // Obtém os dados do espectro
                audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

                // Obtém a frequência máxima
                float maxFreq = 0f;
                float maxAmp = 0f;

                for (int i = 0; i < spectrumData.Length; i++)
                {
                    if (spectrumData[i] > maxAmp)
                    {
                        maxAmp = spectrumData[i];
                        maxFreq = i * AudioSettings.outputSampleRate / 2 / spectrumData.Length;
                    }
                }


                Debug.Log(maxFreq);

                Twist.GetComponent<TwistDeformer>().StartAngle += maxFreq * dir * Time.deltaTime;
                if (Twist.GetComponent<TwistDeformer>().StartAngle >= 180)
                {
                    dir = -1;
                }else if (Twist.GetComponent<TwistDeformer>().StartAngle <= -180)
                {
                    dir = 1;
                }

            }
        }
    }
}
