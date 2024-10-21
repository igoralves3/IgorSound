using System;
using System.Collections;
using System.Collections.Generic;

using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

namespace Deform{
    public class CubeSound : MonoBehaviour
    {

        public AudioSource audioSource;
        

        public GameObject Twist;
        public MeshRenderer meshRenderer;

        public AudioClip sfx1;
        public AudioClip sfx2;
        public AudioClip sfx3;

        public float pitchIncrease = 0.5f;
        public float movementScale=0.5f;

        private float[] spectrumData = new float[256];
        private float[] samplesData = new float[256];
        private int dir = 1;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            meshRenderer = GetComponent<MeshRenderer>();
            
            dir = 1;
        }

        // Update is called once per frame
        void Update()
        {
            if (audioSource.isPlaying) {

                if (Input.GetKeyDown("up")) {
                    IncreasePitch();
                } else if (Input.GetKeyDown("down")) { 
                    DecreasePitch();
                }


                // Obtém os dados do espectro
                audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);


                float lowFreq = GetAverage(spectrumData,0,40);
                float mediumFreq = GetAverage(spectrumData,40,120);
                float highFreq = GetAverage(spectrumData,120,256);


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

                

                var delta = movementScale * maxFreq * dir * Time.deltaTime;

                Twist.GetComponent<TwistDeformer>().StartAngle += delta;
                this.transform.position += new Vector3(delta / 180, 0, 0);
                if (Twist.GetComponent<TwistDeformer>().StartAngle >= 180)
                {
                    dir = -1;
                }else if (Twist.GetComponent<TwistDeformer>().StartAngle <= -180)
                {

                    dir = 1;

                }
                
                if (lowFreq > mediumFreq && lowFreq > highFreq) {
                    meshRenderer.material.SetColor("_Color", Color.red);
                }else if (mediumFreq > lowFreq && mediumFreq > highFreq)
                {
                    meshRenderer.material.SetColor("_Color", Color.green);
                }
                else{
                    meshRenderer.material.SetColor("_Color", Color.blue);
                }

                Debug.Log("Low: " + lowFreq + " Medium: " + mediumFreq + " High: " + highFreq);
            }
        }

        /// <summary>
        /// Função auxiliar para calcular a média de uma faixa de frequências
        /// </summary>
        /// <param name="data">dados do espectro</param>
        /// <param name="start">início dos dados</param>
        /// <param name="end">fim dos dados</param>
        /// <returns></returns>
        float GetAverage(float[] data, int start, int end)
        {
            float sum = 0;
            for (int i = start; i < end; i++)
            {
                sum += data[i];
            }
            return sum / (end - start);
        }

        void IncreasePitch()
        {
            
                audioSource.pitch += pitchIncrease;
                Debug.Log("Pitch aumentado para: " + audioSource.pitch);
            
        }

        void DecreasePitch()
        {
            if (audioSource.pitch > 0)
            {
                audioSource.pitch -= pitchIncrease;
                Debug.Log("Pitch aumentado para: " + audioSource.pitch);
            }
        }

    }
}
