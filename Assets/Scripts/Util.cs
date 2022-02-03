using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static string LowerFirstLetter(this string input)
    {
        switch (input)
        {
            case null: throw new ArgumentNullException(nameof(input));
            case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
            default: return input[0].ToString().ToLower() + input.Substring(1);
        }
    }
}
