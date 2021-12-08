using Project.Logic;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Project.Display
{

    //Used to display messages to the player in the "Message Log" panel
    public static class MessageLog
    {
        private static TextMeshProUGUI LogTextField
        {
            get
            {
                return _ltf ??= GameObject.Find("log").GetComponent<TextMeshProUGUI>();
            }
        }
        private static TextMeshProUGUI _ltf;
        private static StringBuilder StringBuilder { get; set; } = new StringBuilder(500);


        // Use a Queue to keep track of the lines of text
        // The first line added to the log will also be the first removed
        private static Queue<string> _lines = new Queue<string>();


        // Add a line to the MessageLog queue
        public static void Print(string message)
        {
            _lines.Enqueue(message);

            // When exceeding the maximum number of lines remove the oldest one.
            if (_lines.Count > GameSystem.c_MaxLines)
            {
                _lines.Dequeue();
            }

            StringBuilder.Clear();
            string[] lines = _lines.ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                StringBuilder.AppendLine(lines[i]);
            }

            LogTextField.text = StringBuilder.ToString();
        }
    }
}