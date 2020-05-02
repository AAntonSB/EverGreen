using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace NameGenerator
{
    public class GenerateNames : MonoBehaviour
    {

        List<string> words = new List<string>();
        string latinPath;
        string celticPath;
        string[] readText;

        void Start()
        {
            latinPath = @"c:\dev\EverGreen\EverGreen\LatinWordBook.txt";
            celticPath = @"c:\dev\EverGreen\EverGreen\Celtic.txt";
            readText = File.ReadAllLines(latinPath);
            foreach (string s in readText)
            {
                words.Add(s);
            }
            readText = File.ReadAllLines(celticPath);
            foreach (string s in readText)
            {
                words.Add(s);
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log(CreateName());
            }
        }

        string CreateName()
        {
            string firstWord = words[UnityEngine.Random.Range(0,words.Count-1)];
            string secondWord = words[UnityEngine.Random.Range(0,words.Count-1)];
            string randomWord;


            return randomWord = String.Concat(firstWord, secondWord);
        }
    }
}
