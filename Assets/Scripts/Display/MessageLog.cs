using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Project.Display
{

    //Used to display messages to the player in the "Message Log" panel
    public static class MessageLog
    {
        private static TextMeshProUGUI _logTextField;
        private static TextMeshProUGUI _logField
        {
            get
            {
                return _logTextField ??= GameObject.Find("log").GetComponent<TextMeshProUGUI>();
            }
        }
        private static StringBuilder _stringBuilder { get; set; } = new StringBuilder(500);

        // Define the maximum number of lines to store
        private static readonly int _maxLines = 5;

        // Use a Queue to keep track of the lines of text
        // The first line added to the log will also be the first removed
        private static Queue<string> _lines = new Queue<string>();


        // Add a line to the MessageLog queue
        public static void Print(string message)
        {
            _lines.Enqueue(message);

            // When exceeding the maximum number of lines remove the oldest one.
            if (_lines.Count > _maxLines)
            {
                _lines.Dequeue();
            }

            _stringBuilder.Clear();
            string[] lines = _lines.ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                _stringBuilder.AppendLine(lines[i]);
            }

            _logField.text = _stringBuilder.ToString();
        }
    }
}