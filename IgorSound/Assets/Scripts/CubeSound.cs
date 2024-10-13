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
        private float[] spectrumData = new float[256];
        private float[] samplesData = new float[256];
        private int dir = 1;

        public GameObject Twist;
        public MeshRenderer meshRenderer;

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

                // Obtém os dados do espectro
                audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);


                float lowFreq = GetAverage(spectrumData,0,80);
                float mediumFreq = GetAverage(spectrumData,80,160);
                float highFreq = GetAverage(spectrumData,160,256);


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

                var delta = maxFreq * dir * Time.deltaTime;

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

        // Função auxiliar para calcular a média de uma faixa de frequências
        float GetAverage(float[] data, int start, int end)
        {
            float sum = 0;
            for (int i = start; i < end; i++)
            {
                sum += data[i];
            }
            return sum / (end - start);
        }
    }
}
