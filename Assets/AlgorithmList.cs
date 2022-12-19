using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgorithmList : MonoBehaviour
{
    // 1 - A --> B
    // 2 - A --> C
    // 3 - B --> C
    // 4 - C --> A
    // 5 - C --> B
    // 6 - B --> A
    public static void AddTask(char a, char b)
    {
        int task = 1;

        if (a == 'A' && b == 'B')
        {
            task = 1;
        }

        if (a == 'A' && b == 'C')
        {
            task = 2;
        }

        if (a == 'B' && b == 'C')
        {
            task = 3;
        }

        if (a == 'C' && b == 'A')
        {
            task = 4;
        }

        if (a == 'C' && b == 'B')
        {
            task = 5;
        }

        if (a == 'B' && b == 'A')
        {
            task = 6;
        }

        SolveRings.tasksToExecute.Add(task);
    }

}
